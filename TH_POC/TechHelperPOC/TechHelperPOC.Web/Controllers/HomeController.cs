using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TechHelperPOC.Web.Models;
using TechHelperPOC.Web.Repository;
using TechHelperPOC.Web.ViewModel;


namespace TechHelperPOC.Web.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        //List<SelectListItem> pcName;
        private ApplicationDbContext db = new ApplicationDbContext();
        private void SetViewContent(string computer)
        {
            var pcName = new List<SelectListItem>();
            var pc = db.SystemInfoes.Select(m => new SelectListItem
            {
                Value = m.MachineName,
                Text = m.MachineName
            }).Distinct();
            
            pcName.Add(new SelectListItem { Value = "",Text = "Select Machine" });
            pcName.InsertRange(1, pc);
            ViewBag.machineName = pcName;
            ViewBag.Machine = computer;
            
        }
        private void SetDropdownList(string computer)
        {
            SetViewContent(computer);
            if (computer == null || computer == "")
            {
                ViewBag.Message = "Search Computer!";
            }
            else
            {}
        }

        private void SetScannedDate(string computer, string scanDate, string macAddress)
        {
            
            var sDate = unit.SystemInfoes.Get()
               .Where(m => m.MachineName == computer && m.MacAddress == macAddress)
               .OrderByDescending(s => s.ScannedDate).Distinct();
            var d = new List<SelectListItem>();
            var sd = string.Empty;
            d.Add(new SelectListItem { Value = "", Text = "Select Date" });
            foreach (var s in sDate)
            {
                if (sd != s.ScannedDate)
                    d.Add(new SelectListItem { Value = s.ScannedDate, Text = s.ScannedDate });
                    sd = s.ScannedDate;

            }
            
            ViewBag.scannedDate = d;
        }

        private void ShowTechnicianInfo(string machinename, string scandate)
        {
            var tech = new ApplicationDbContext();
            var t =  tech.Auditors.Where(a=>a.Workstation==machinename && a.ScannedDate==scandate);
           
        }

        private void SetMacAddress(string computer, string macAddress)
        {

            var macAddr = unit.SystemInfoes.Get()
               .Where(m => m.MachineName == computer).Distinct();
            var d = new List<SelectListItem>();
            var sd = string.Empty;
            d.Add(new SelectListItem { Value = "", Text = "Select MacAddress" });
            foreach (var s in macAddr)
            {
                if (sd != s.MacAddress)
                    d.Add(new SelectListItem { Value = s.MacAddress, Text = s.MacAddress });
                sd = s.MacAddress;

            }

            ViewBag.macAddress = d;
        }

        public ActionResult GetScannedDate(string macAddress)
        {
            var sDate = unit.SystemInfoes.Get()
                .Where(m => m.MacAddress == macAddress).OrderByDescending(s=>s.ScannedDate).Distinct();
            var d = new List<SelectListItem>();
            var sd = string.Empty;
            foreach (var s in sDate)
            {
                if (sd != s.ScannedDate)
                    d.Add(new SelectListItem { Value = s.ScannedDate.Replace(" ", string.Empty), Text = s.ScannedDate });
                    sd = s.ScannedDate;
                
            }
 
            return Json(d);
        }

        public ActionResult GetMacAddress(string machineName)
        {
            var macAddress = unit.SystemInfoes.Get()
                .Where(m => m.MachineName == machineName).Distinct();
            var d = new List<SelectListItem>();
            var sd = string.Empty;
            foreach (var s in macAddress)
            {
                if (sd != s.MacAddress)
                    d.Add(new SelectListItem { Value = s.MacAddress, Text = s.MacAddress });
                sd = s.MacAddress;

            }

            return Json(d);
        }

        
        public ActionResult Index()
        {
            var computer = System.Web.HttpContext.Current.Session["ComputerName"] as String;
            var scanDate = System.Web.HttpContext.Current.Session["ScannedDate"] as String;
            var macAddress = System.Web.HttpContext.Current.Session["MacAddress"] as String;
            SetDropdownList(computer);
            SetScannedDate(computer,scanDate, macAddress);
            SetMacAddress(computer, macAddress);
            var PcInfo = unit.SystemInfoes.Get().Where(m => m.MachineName == computer && 
                m.ScannedDate.Replace(" ", string.Empty) == scanDate.Replace(" ", string.Empty) &&
                m.MacAddress==macAddress);
            ViewBag.Header = string.Empty;
            return View(PcInfo.ToList());
        }

        [HttpPost]
        public ActionResult Index(string machineName, string scannedDate, string macAddress)
        {
            if ((machineName == null || machineName == "") && 
                (scannedDate == null || scannedDate == "" && 
                (macAddress == null || macAddress == "")))
            {
                SetDropdownList(machineName);
                SetScannedDate(machineName,scannedDate, macAddress);
                SetMacAddress(machineName, macAddress);
                return RedirectToAction("Index");
            }
           
            System.Web.HttpContext.Current.Session["ComputerName"] = machineName;
            System.Web.HttpContext.Current.Session["ScannedDate"] = scannedDate;
            System.Web.HttpContext.Current.Session["MacAddress"] = macAddress;
            ViewBag.Machine = machineName;
            SetDropdownList(machineName);
            SetScannedDate(machineName, scannedDate, macAddress);
            SetMacAddress(machineName, macAddress);
            var PcInfo = unit.SystemInfoes.Get().Where(m => m.MachineName == machineName && 
                m.ScannedDate.Replace(" ", string.Empty) == scannedDate.Replace(" ", string.Empty) &&
                 m.MacAddress == macAddress);
            return View(PcInfo.ToList());
        }


        public ActionResult Audit()
        {
            var computer = System.Web.HttpContext.Current.Session["ComputerName"] as String;
            var scanDate = System.Web.HttpContext.Current.Session["ScannedDate"] as String;
            var macAddress = System.Web.HttpContext.Current.Session["MacAddress"] as String;
            SetDropdownList(computer);
            SetScannedDate(computer, scanDate, macAddress);
            SetMacAddress(computer, macAddress);
            var systemaudit = new SystemAuditViewModel();
            var Info = db.Auditors.Join(db.SystemInfoes,
                a=>a.ScannedDate, s=>s.ScannedDate,
                (a, s) => new
                {
                    Technician = a.Technician,
                    Client = a.Client,
                    Site = a.Site,
                    Workstation = a.Workstation,
                    OS = a.OS,
                    MachineName = s.MachineName,
                    ManagementGroup = s.ManagementGroup,
                    DisplayGroup = s.DisplayGroup,
                    PropertyName = s.PropertyName,
                    PropertyValue = s.PropertyValue,
                    ScannedDate = a.ScannedDate,
                    MacAddress = s.MacAddress
                }).ToList();

            foreach (var i in Info)
            {
                var pc = new SystemAudit();
                pc.Technician = i.Technician;
                pc.Client = i.Client;
                pc.Site = i.Site;
                pc.Workstation = i.Workstation;
                pc.OS = i.OS;
                pc.MachineName = i.MachineName;
                pc.ManagementGroup = i.ManagementGroup;
                pc.DisplayGroup = i.DisplayGroup;
                pc.PropertyName = i.PropertyName;
                pc.PropertyValue = i.PropertyValue;
                pc.ScannedDate = i.ScannedDate;
                pc.MacAddress = i.MacAddress;
                systemaudit.SystemAudits.Add(pc);
            }
            //var PcInfo = unit.SystemInfoes.Get().Where(m => m.MachineName == computer &&
            //    m.ScannedDate.Replace(" ", string.Empty) == scanDate.Replace(" ", string.Empty) &&
            //    m.MacAddress == macAddress);
            ViewBag.Header = string.Empty;
            return View(systemaudit);
        }

        [HttpPost]
        public ActionResult Audit(string machineName, string scannedDate, string macAddress)
        {
            if ((machineName == null || machineName == "") &&
                (scannedDate == null || scannedDate == "" &&
                (macAddress == null || macAddress == "")))
            {
                SetDropdownList(machineName);
                SetScannedDate(machineName, scannedDate, macAddress);
                SetMacAddress(machineName, macAddress);
                return RedirectToAction("Index");
            }

            System.Web.HttpContext.Current.Session["ComputerName"] = machineName;
            System.Web.HttpContext.Current.Session["ScannedDate"] = scannedDate;
            System.Web.HttpContext.Current.Session["MacAddress"] = macAddress;
            ViewBag.Machine = machineName;
            SetDropdownList(machineName);
            SetScannedDate(machineName, scannedDate, macAddress);
            SetMacAddress(machineName, macAddress);
            var Info = db.Auditors.Join(db.SystemInfoes,
                a => a.ScannedDate, s => s.ScannedDate,
                (a, s) => new
                {
                    Technician = a.Technician,
                    Client = a.Client,
                    Site = a.Site,
                    Workstation = a.Workstation,
                    OS = a.OS,
                    MachineName = s.MachineName,
                    ManagementGroup = s.ManagementGroup,
                    DisplayGroup = s.DisplayGroup,
                    PropertyName = s.PropertyName,
                    PropertyValue = s.PropertyValue,
                    ScannedDate = a.ScannedDate,
                    MacAddress = s.MacAddress
                }).Where(a => a.MachineName == machineName && a.ScannedDate.Replace(" ", string.Empty) == scannedDate.Replace(" ", string.Empty)
                    && a.MacAddress == macAddress).ToList();

            var systemaudit = new SystemAuditViewModel();
            foreach (var i in Info)
            {
                var pc = new SystemAudit();
                pc.Technician = i.Technician;
                pc.Client = i.Client;
                pc.Site = i.Site;
                pc.Workstation = i.Workstation;
                pc.OS = i.OS;
                pc.MachineName = i.MachineName;
                pc.ManagementGroup = i.ManagementGroup;
                pc.DisplayGroup = i.DisplayGroup;
                pc.PropertyName = i.PropertyName;
                pc.PropertyValue = i.PropertyValue;
                pc.ScannedDate = i.ScannedDate;
                pc.MacAddress = i.MacAddress;
                systemaudit.SystemAudits.Add(pc);
            }

            return View(systemaudit);
        }
        
        public ActionResult Show()
        {
            var computer = System.Web.HttpContext.Current.Session["ComputerName"] as String;
            var scanDate = System.Web.HttpContext.Current.Session["ScannedDate"] as String;
            var macAddress = System.Web.HttpContext.Current.Session["MacAddress"] as String;
            SetDropdownList(computer);
            SetScannedDate(computer,scanDate, macAddress);
            SetMacAddress(computer, macAddress);
            var PcInfo = unit.SystemInfoes.Get().Where(m => m.MachineName == computer);
            ViewBag.Header = string.Empty;
            return View(PcInfo.ToList());
        }
        
        [HttpPost]
        public ActionResult Show(string machineName, string scannedDate, string macAddress)
        {
            if (machineName == null || machineName == "" && (scannedDate == null || scannedDate == ""))
            {
                SetDropdownList(machineName);
                SetScannedDate(machineName, scannedDate, macAddress);
                SetMacAddress(machineName, macAddress);
                return RedirectToAction("Show");
            }
            System.Web.HttpContext.Current.Session["ComputerName"] = machineName;
            ViewBag.Machine = machineName;
            SetDropdownList(machineName);
            SetScannedDate(machineName, scannedDate, macAddress);
            SetMacAddress(machineName, macAddress);
            ShowTechnicianInfo(machineName, scannedDate);
            var PcInfo = unit.SystemInfoes.Get().Where(m => m.MachineName == machineName);
            return View(PcInfo.ToList());
        }

        // GET: System Overview checked
        public ActionResult SystemOverView()
        {
            var computer = System.Web.HttpContext.Current.Session["ComputerName"] as String;
            SetDropdownList(computer);
            var items = unit.SystemInfoes.Get().Where(i => i.MachineName == computer && (i.DisplayGroup == "System Overview"
                || (i.DisplayGroup == "Processor" && (i.PropertyName == "NumberOfCores"
                || i.PropertyName == "NumberOfLogicalProcessors"))));


            return View(items.ToList());
        }

        [HttpPost]
        public ActionResult SystemOverView(string machineName)
        {
            if (machineName == null || machineName == "")
            {
                return RedirectToAction("SystemOverView");
            }
            
            System.Web.HttpContext.Current.Session["ComputerName"] = machineName;
            var computer = System.Web.HttpContext.Current.Session["ComputerName"] as String;
            SetDropdownList(machineName);
            var items = unit.SystemInfoes.Get().Where(i => i.MachineName == computer && (i.DisplayGroup == "System Overview"
                || (i.DisplayGroup == "Processor" && (i.PropertyName == "NumberOfCores"
                || i.PropertyName == "NumberOfLogicalProcessors" || i.PropertyName == "Name"))));

            return View(items.ToList());
        }

        //get Operating system checked
        public ActionResult OperatingSystem()
        {
            var computer = System.Web.HttpContext.Current.Session["ComputerName"] as String;
            SetDropdownList(computer);
            var items = unit.SystemInfoes.Get().Where(i => i.MachineName == computer && i.DisplayGroup == "Operating System");
            return View(items.ToList());
        }

        [HttpPost]
        public ActionResult OperatingSystem(string machineName)
        {
            if (machineName == null || machineName == "")
            {
                return RedirectToAction("OperatingSystem");
            }
            System.Web.HttpContext.Current.Session["ComputerName"] = machineName;
            var computer = System.Web.HttpContext.Current.Session["ComputerName"] as String;
            SetDropdownList(machineName);
            var items = unit.SystemInfoes.Get().Where(i => i.MachineName == computer && i.DisplayGroup == "Operating System");
            return View(items.ToList());
        }

        //get User Account checked
        public ActionResult UserAccount()
        {
            var computer = System.Web.HttpContext.Current.Session["ComputerName"] as String;
            SetDropdownList(computer);
            var items = unit.SystemInfoes.Get().Where(i => i.MachineName == computer && i.DisplayGroup == "Users");
            return View(items.ToList());
        }

        [HttpPost]
        public ActionResult UserAccount(string machineName)
        {
            if (machineName == null || machineName == "")
            {
                return RedirectToAction("UserAccount");
            }
            System.Web.HttpContext.Current.Session["ComputerName"] = machineName;
            var computer = System.Web.HttpContext.Current.Session["ComputerName"] as String;
            SetDropdownList(machineName);
            var items = db.SystemInfoes.Where(i => i.MachineName == computer && i.DisplayGroup == "Users");
            return View(items.ToList());
        }

        //Group Users
        public ActionResult GroupUsers()
        {
            var computer = System.Web.HttpContext.Current.Session["ComputerName"] as String;
            SetDropdownList(computer);
            var items = unit.SystemInfoes.Get().Where(i => i.MachineName == computer && i.DisplayGroup == "Group Users");
            return View(items.ToList());
        }
        [HttpPost]
        public ActionResult GroupUsers(string machineName)
        {
            if (machineName == null || machineName == "")
            {
                return RedirectToAction("GroupUsers");
            }
            System.Web.HttpContext.Current.Session["ComputerName"] = machineName;
            var computer = System.Web.HttpContext.Current.Session["ComputerName"] as String;
            SetDropdownList(machineName);
            var items = unit.SystemInfoes.Get().Where(i => i.MachineName == computer && i.DisplayGroup == "Group Users");
            return View(items.ToList());
        }

        public ActionResult Peripherals()
        {
            var computer = System.Web.HttpContext.Current.Session["ComputerName"] as String;
            SetDropdownList(computer);
            var items = unit.SystemInfoes.Get().Where(e => e.MachineName == computer && (e.Sequence == 1 && e.PropertyName == "Name"
                && (e.DisplayGroup == "Printer" || e.DisplayGroup == "Keyboard")));
            return View(items.ToList());
        }

        [HttpPost]
        public ActionResult Peripherals(string machineName)
        {
            if (machineName == null || machineName == "")
            {
                return RedirectToAction("Peripherals");
            }
            System.Web.HttpContext.Current.Session["ComputerName"] = machineName;
            var computer = System.Web.HttpContext.Current.Session["ComputerName"] as String;
            SetDropdownList(machineName);
            var items = unit.SystemInfoes.Get().Where(e => e.MachineName == computer && (e.Sequence == 1 && e.PropertyName == "Name"
                && (e.DisplayGroup == "Printer" || e.DisplayGroup == "Keyboard")));
            return View(items.ToList());
        }

        //get display adapter checked
        public ActionResult DisplayAdapter()
        {
            var computer = System.Web.HttpContext.Current.Session["ComputerName"] as String;
            SetDropdownList(computer);
            var items = unit.SystemInfoes.Get().Where(i => i.MachineName == computer && i.DisplayGroup == "Display Adapters");
            return View(items.ToList());
        }

        [HttpPost]
        public ActionResult DisplayAdapter(string machineName)
        {
            if (machineName == null || machineName == "")
            {
                return RedirectToAction("DisplayAdapter");
            }
            System.Web.HttpContext.Current.Session["ComputerName"] = machineName;
            var computer = System.Web.HttpContext.Current.Session["ComputerName"] as String;
            SetDropdownList(machineName);
            var items = unit.SystemInfoes.Get().Where(i => i.MachineName == computer && i.DisplayGroup == "Display Adapters");
            return View(items.ToList());
        }

        //get Processor checked
        public ActionResult Processor()
        {
            var computer = System.Web.HttpContext.Current.Session["ComputerName"] as String;
            SetDropdownList(computer);
            var items = unit.SystemInfoes.Get().Where(i => i.MachineName == computer && i.DisplayGroup == "Processor");
            return View(items.ToList());
        }

        [HttpPost]
        public ActionResult Processor(string machineName)
        {
            if (machineName == null || machineName == "")
            {
                return RedirectToAction("Processor");
            }
            System.Web.HttpContext.Current.Session["ComputerName"] = machineName;
            var computer = System.Web.HttpContext.Current.Session["ComputerName"] as String;
            SetDropdownList(machineName);
            var items = unit.SystemInfoes.Get().Where(i => i.MachineName == computer && i.DisplayGroup == "Processor");
            return View(items.ToList());
        }
        //get Local time checked
        public ActionResult LocalTime()
        {
            var computer = System.Web.HttpContext.Current.Session["ComputerName"] as String;
            SetDropdownList(computer);
            var items = unit.SystemInfoes.Get().Where(i => i.MachineName == computer && i.DisplayGroup == "Local Time");
            return View(items.ToList());
        }

        [HttpPost]
        public ActionResult LocalTime(string machineName)
        {
            if (machineName == null || machineName == "")
            {
                return RedirectToAction("LocalTime");
            }
            System.Web.HttpContext.Current.Session["ComputerName"] = machineName;
            var computer = System.Web.HttpContext.Current.Session["ComputerName"] as String;
            SetDropdownList(machineName);
            var items = unit.SystemInfoes.Get().Where(i => i.MachineName == computer && i.DisplayGroup == "Local Time");
            return View(items.ToList());
        }

        //get Printer checked
        public ActionResult Printers()
        {
            var computer = System.Web.HttpContext.Current.Session["ComputerName"] as String;
            SetDropdownList(computer);
            var items = unit.SystemInfoes.Get().Where(i => i.MachineName == computer && i.DisplayGroup == "Printer");
            return View(items.ToList());
        }

        [HttpPost]
        public ActionResult Printers(string machineName)
        {
            if (machineName == null || machineName == "")
            {
                return RedirectToAction("Printers");
            }
            System.Web.HttpContext.Current.Session["ComputerName"] = machineName;
            var computer = System.Web.HttpContext.Current.Session["ComputerName"] as String;
            SetDropdownList(machineName);
            var items = unit.SystemInfoes.Get().Where(i => i.MachineName == computer && i.DisplayGroup == "Printer");
            return View(items.ToList());
        }

        //get Printer configuration
        public ActionResult PrinterConfiguration()
        {
            var computer = System.Web.HttpContext.Current.Session["ComputerName"] as String;
            SetDropdownList(computer);
            var items = unit.SystemInfoes.Get().Where(i => i.MachineName == computer && i.DisplayGroup == "Printer Configuration");
            return View(items.ToList());
        }
        [HttpPost]
        public ActionResult PrinterConfiguration(string machineName)
        {
            if (machineName == null || machineName == "")
            {
                return RedirectToAction("PrinterConfiguration");
            }
            System.Web.HttpContext.Current.Session["ComputerName"] = machineName;
            var computer = System.Web.HttpContext.Current.Session["ComputerName"] as String;
            SetDropdownList(machineName);
            var items = unit.SystemInfoes.Get().Where(i => i.MachineName == computer && i.DisplayGroup == "Printer Configuration");
            return View(items.ToList());
        }

        //get BIOS version checked
        public ActionResult BiosVersion()
        {
            var computer = System.Web.HttpContext.Current.Session["ComputerName"] as String;
            SetViewContent(computer);
            var items = unit.SystemInfoes.Get().Where(i => i.MachineName == computer && i.DisplayGroup == "BIOS Version");
            return View(items.ToList());
        }

        [HttpPost]
        public ActionResult BiosVersion(string machineName)
        {
            if (machineName == null || machineName == "")
            {
                return RedirectToAction("BiosVersion");
            }
            System.Web.HttpContext.Current.Session["ComputerName"] = machineName;
            var computer = System.Web.HttpContext.Current.Session["ComputerName"] as String;
            SetDropdownList(machineName);
            var items = unit.SystemInfoes.Get().Where(i => i.MachineName == computer && i.DisplayGroup == "BIOS Version");
            return View(items.ToList());
        }

        //Physical drive checked
        public ActionResult PhysicalDisk()
        {
            var computer = System.Web.HttpContext.Current.Session["ComputerName"] as String;
            SetDropdownList(computer);
            var items = unit.SystemInfoes.Get().Where(i => i.MachineName == computer && i.DisplayGroup == "Physical Disk");
            return View(items.ToList());
        }

        [HttpPost]
        public ActionResult PhysicalDisk(string machineName)
        {
            if (machineName == null || machineName == "")
            {
                return RedirectToAction("PhysicalDisk");
            }
            System.Web.HttpContext.Current.Session["ComputerName"] = machineName;
            var computer = System.Web.HttpContext.Current.Session["ComputerName"] as String;
            SetDropdownList(machineName);
            var items = unit.SystemInfoes.Get().Where(i => i.MachineName == computer && i.DisplayGroup == "Physical Disk");
            return View(items.ToList());
        }

        //get "Logical Disk" checked
        public ActionResult LogicalDisk()
        {
            var computer = System.Web.HttpContext.Current.Session["ComputerName"] as String;
            SetDropdownList(computer);
            var items = unit.SystemInfoes.Get().Where(i => i.MachineName == computer && i.DisplayGroup == "Logical Disk");
            return View(items.ToList());
        }

        [HttpPost]
        public ActionResult LogicalDisk(string machineName)
        {
            if (machineName == null || machineName == "")
            {
                return RedirectToAction("LogicalDisk");
            }
            System.Web.HttpContext.Current.Session["ComputerName"] = machineName;
            var computer = System.Web.HttpContext.Current.Session["ComputerName"] as String;
            SetDropdownList(machineName);
            var items = unit.SystemInfoes.Get().Where(i => i.MachineName == computer && i.DisplayGroup == "Logical Disk");
            return View(items.ToList());
        }

        //"Installed Software" checked
        public ActionResult InstalledSoftware()
        {
            var computer = System.Web.HttpContext.Current.Session["ComputerName"] as String;
            SetDropdownList(computer);
            var items = unit.SystemInfoes.Get().Where(i => i.MachineName == computer && i.DisplayGroup == "Installed Software");
            return View(items.ToList());
        }

        [HttpPost]
        public ActionResult InstalledSoftware(string machineName)
        {
            if (machineName == null || machineName == "")
            {
                return RedirectToAction("InstalledSoftware");
            }
            System.Web.HttpContext.Current.Session["ComputerName"] = machineName;
            var computer = System.Web.HttpContext.Current.Session["ComputerName"] as String;
            SetDropdownList(machineName);
            var items = unit.SystemInfoes.Get().Where(i => i.MachineName == computer && i.DisplayGroup == "Installed Software");
            return View(items.ToList());
        }

        //"Startup Programs" checked
        public ActionResult StartupProgram()
        {
            var computer = System.Web.HttpContext.Current.Session["ComputerName"] as String;
            SetDropdownList(computer);
            var items = unit.SystemInfoes.Get().Where(i => i.MachineName == computer && i.DisplayGroup == "Startup Programs");
            return View(items.ToList());
        }

        [HttpPost]
        public ActionResult StartupProgram(string machineName)
        {
            if (machineName == null || machineName == "")
            {
                return RedirectToAction("StartupProgram");
            }
            System.Web.HttpContext.Current.Session["ComputerName"] = machineName;
            var computer = System.Web.HttpContext.Current.Session["ComputerName"] as String;
            SetDropdownList(machineName);
            var items = unit.SystemInfoes.Get().Where(i => i.MachineName == computer && i.DisplayGroup == "Startup Programs");
            return View(items.ToList());
        }

        //Network Adapter
        public ActionResult NetworkTCPIP()
        {
            var computer = System.Web.HttpContext.Current.Session["ComputerName"] as String;
            SetDropdownList(computer);
            var items = unit.SystemInfoes.Get().Where(i => i.MachineName == computer && i.DisplayGroup == "Network Adapter");
            return View(items.ToList());
        }

        [HttpPost]
        public ActionResult NetworkTCPIP(string machineName)
        {
            if (machineName == null || machineName == "")
            {
                return RedirectToAction("NetworkTCPIP");
            }
            System.Web.HttpContext.Current.Session["ComputerName"] = machineName;
            var computer = System.Web.HttpContext.Current.Session["ComputerName"] as String;
            SetDropdownList(machineName);
            var items = unit.SystemInfoes.Get().Where(i => i.MachineName == computer && i.DisplayGroup == "Network Adapter");
            return View(items.ToList());
        }

        //"Routing table" checked
        public ActionResult RoutingTable()
        {
            var computer = System.Web.HttpContext.Current.Session["ComputerName"] as String;
            SetDropdownList(computer);
            var items = unit.SystemInfoes.Get().Where(i => i.MachineName == computer && i.DisplayGroup == "Routing table");
            return View(items.ToList());
        }

        [HttpPost]
        public ActionResult RoutingTable(string machineName)
        {
            if (machineName == null || machineName == "")
            {
                return RedirectToAction("RoutingTable");
            }
            System.Web.HttpContext.Current.Session["ComputerName"] = machineName;
            var computer = System.Web.HttpContext.Current.Session["ComputerName"] as String;
            SetDropdownList(machineName);
            var items = unit.SystemInfoes.Get().Where(i => i.MachineName == computer && i.DisplayGroup == "Routing table");
            return View(items.ToList());
        }

        //Memory
        public ActionResult Memory()
        {
            var computer = System.Web.HttpContext.Current.Session["ComputerName"] as String;
            SetDropdownList(computer);
            var items = unit.SystemInfoes.Get().Where(i => i.MachineName == computer && i.DisplayGroup == "Memory");

            return View(items.ToList());
        }
        [HttpPost]
        public ActionResult Memory(string machineName)
        {
            if (machineName == null || machineName == "")
            {
                return RedirectToAction("Memory");
            }
            System.Web.HttpContext.Current.Session["ComputerName"] = machineName;
            var computer = System.Web.HttpContext.Current.Session["ComputerName"] as String;
            SetDropdownList(machineName);
            var items = unit.SystemInfoes.Get().Where(i => i.MachineName == computer && i.DisplayGroup == "Memory");

            return View(items.ToList());
        }

        //cache memory
        public ActionResult CacheMemory()
        {
            var computer = System.Web.HttpContext.Current.Session["ComputerName"] as String;
            SetDropdownList(computer);
            var items = unit.SystemInfoes.Get().Where(i => i.MachineName == computer && i.DisplayGroup == "Cache");

            return View(items.ToList());
        }
        [HttpPost]
        public ActionResult CacheMemory(string machineName)
        {
            if (machineName == null || machineName == "")
            {
                return RedirectToAction("CacheMemory");
            }
            System.Web.HttpContext.Current.Session["ComputerName"] = machineName;
            var computer = System.Web.HttpContext.Current.Session["ComputerName"] as String;
            SetDropdownList(machineName);
            var items = unit.SystemInfoes.Get().Where(i => i.MachineName == computer && i.DisplayGroup == "Cache");

            return View(items.ToList());
        }
        //"Port Connector"
        public ActionResult PortConnector()
        {
            var computer = System.Web.HttpContext.Current.Session["ComputerName"] as String;
            SetDropdownList(computer);
            var items = unit.SystemInfoes.Get().Where(i => i.MachineName == computer && i.DisplayGroup == "Port Connector");

            return View(items.ToList());
        }
        [HttpPost]
        public ActionResult PortConnector(string machineName)
        {
            if (machineName == null || machineName == "")
            {
                return RedirectToAction("PortConnector");
            }
            System.Web.HttpContext.Current.Session["ComputerName"] = machineName;
            var computer = System.Web.HttpContext.Current.Session["ComputerName"] as String;
            SetDropdownList(machineName);
            var items = unit.SystemInfoes.Get().Where(i => i.MachineName == computer && i.DisplayGroup == "Port Connector");

            return View(items.ToList());
        }

        public ActionResult SystemInfo()
        {
            var sb = new StringBuilder(string.Empty);
            try
            {
                sb.AppendFormat("Operation System: {0}<br/>", Environment.OSVersion);
                if (Environment.Is64BitOperatingSystem)
                    sb.AppendFormat("64 Bit Operating System<br/>");
                else
                    sb.AppendFormat("32 Bit Operating System<br/>");
                sb.AppendFormat("SystemDirectory: {0}<br/>", Environment.SystemDirectory);
                sb.AppendFormat("ProcessorCount: {0}<br/>", Environment.ProcessorCount);
                sb.AppendFormat("UserDomainName: {0}<br/>", Environment.UserDomainName);
                sb.AppendFormat("UserName: {0}<br>", Environment.UserName);
                sb.AppendFormat("MachineName: {0}<br>", Environment.MachineName);

                //Drives
                sb.AppendFormat("LogicalDrives:<br>");
                foreach (System.IO.DriveInfo DriveInfo1 in System.IO.DriveInfo.GetDrives())
                {
                    try
                    {
                        sb.AppendFormat("Drive: {0}<br/>VolumeLabel: {1}<br/>DriveType: {2}<br/>DriveFormat: " +
                            "{3}<br/>TotalSize: {4}<br/>AvailableFreeSpace: {5}<br/>",
                            DriveInfo1.Name, DriveInfo1.VolumeLabel, DriveInfo1.DriveType,
                            DriveInfo1.DriveFormat, DriveInfo1.TotalSize, DriveInfo1.AvailableFreeSpace);
                    }
                    catch
                    {
                    }
                }
                sb.AppendFormat("SystemPageSize: {0}<br/>", Environment.SystemPageSize);
                sb.AppendFormat("Version: {0}", Environment.Version);
            }
            catch
            {
            }
            return Content(sb.ToString());
        }

        public ActionResult DeviceInfo(string id)
        {
            var StringBuilder1 = new StringBuilder(string.Empty);

            try
            {
                var ManagementClass1 = new ManagementClass(id);
                var ManagemenobjCol = ManagementClass1.GetInstances();
                var properties = ManagementClass1.Properties;

                foreach (var obj in ManagemenobjCol)
                {
                    foreach (var property in properties)
                    {
                        try
                        {
                            StringBuilder1.AppendLine(property.Name + ":  " +
                            obj.Properties[property.Name].Value.ToString() + "<br/>");
                        }
                        catch
                        {
                        }
                    }
                    StringBuilder1.AppendLine();
                }
            }
            catch
            {
            }
            return Content(StringBuilder1.ToString());
        }

        public ActionResult InstalledProgram()
        {
            var sb = new StringBuilder(string.Empty);

            return Content(sb.ToString());
        }

        [HttpGet]
        public ActionResult SaveInfo()
        {
            return PartialView("SaveInfo");
        }
        [HttpPost]
        public ActionResult SaveInfo(FormCollection form)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Clear records
                    unit.SystemInfoes.DeleteByMachineName(Environment.MachineName);
                    var count = Request.Form.AllKeys.Where(x => x.StartsWith("machinename")).ToList().Count();
                    System.Web.HttpContext.Current.Session["ComputerName"] = form["machinename1"];
                    ViewBag.Machine = form["machinename1"];

                    for (int i = 1; i <= count; i++)
                    {

                        //SaveInfo(form["machinename" + i], form["group" + i], form["dgroup" + i], int.Parse(form["sequence" + i]), form["property" + i], form["value" + i]);
                    }

                    
                    unit.Save();

                }
                catch (Exception ex)
                {
                    throw;
                }
                return Json(new { success = true });
            }

            return RedirectToAction("Index");
        }

        public string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return null;
        }
        [HttpGet]
        public ActionResult GetInformation()
        {
            return PartialView("GetInformation");
        }
        
        [HttpPost]
        public ActionResult GetInformation(FormCollection form)
        {

            Auditor auditor = new Auditor();
            auditor.Technician = form["techname"];
            auditor.Client = form["client"];
            auditor.Site = form["site"];
            auditor.Workstation = form["pc"];
            auditor.OS = form["os"];
            auditor.ScannedDate = form["scandate"];
            db.Auditors.Add(auditor);
            db.SaveChanges();
            //unit.SystemInfoes.DeleteByMachineName(form["machinename1"]);
            var count = Request.Form.AllKeys.Where(x=> x.StartsWith("machinename")).ToList().Count();
            System.Web.HttpContext.Current.Session["ComputerName"] = form["machinename1"];
            ViewBag.Machine = form["machinename1"];
            
            for (int i = 1; i <= count; i++)
            {
                SaveInfo(form["machinename" + i], form["group" + i], form["dgroup" + i], int.Parse(form["sequence" + i]), form["property" + i], form["value" + i], form["scanDate" + i], form["macAddress" + i]);
            }

            unit.Save();
           return RedirectToAction("Show", "Home");
         } 

        private void SaveInfo(string machine, string group, string dgroup, int sequence, string property, string value, string scandate, string mcAddress)
        {
            unit.SystemInfoes.Create(new SystemInfo()
            {
                MachineName = machine,
                ManagementGroup = group,
                DisplayGroup = dgroup,
                Sequence = sequence,
                PropertyName = property,
                PropertyValue = value,
                ScannedDate = scandate,
                MacAddress = mcAddress
            });
        }



        //public void SaveDeviceInfo(string machine, string group, string dgroup, int i, string property)
        //{
        //    var value = string.Empty;

        //    try
        //    {
        //        var ManagementClass1 = new ManagementClass(group);
        //        var ManagemenobjCol = ManagementClass1.GetInstances();
        //        var properties = ManagementClass1.Properties;

        //        foreach (var obj in ManagemenobjCol)
        //        {
        //            foreach (var prop in properties)
        //            {
        //                try
        //                {
        //                    value = obj.Properties[property].Value.ToString();
        //                    SaveInfo(Environment.MachineName, group, dgroup, i, property, value);
        //                    return;
        //                }
        //                catch
        //                {
        //                }
        //            }
        //        }
        //    }
        //    catch
        //    {
        //    }
        //}

        //public void SaveAllDeviceInfo(string machine, string group, string dgroup, string[] property)
        //{
        //    var value = string.Empty;

        //    try
        //    {
        //        var i = 0;
        //        var ManagementClass1 = new ManagementClass(group);
        //        var ManagemenobjCol = ManagementClass1.GetInstances();
        //        var properties = ManagementClass1.Properties;

        //        foreach (var obj in ManagemenobjCol)
        //        {
        //            foreach (var prop in properties)
        //            {
        //                try
        //                {
        //                    for (int x = 0; x < property.Length; x++)
        //                    {
        //                        value = obj.Properties[property[x]].Value.ToString();
        //                        SaveInfo(Environment.MachineName, group, dgroup, ++i, property[x], value);
        //                    }
        //                    i = 0;
        //                    break;
        //                }
        //                catch
        //                {
        //                }
        //            }

        //        }
        //    }
        //    catch
        //    {
        //    }
        //}

    }
}