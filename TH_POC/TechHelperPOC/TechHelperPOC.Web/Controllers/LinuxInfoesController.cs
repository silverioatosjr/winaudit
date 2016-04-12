using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TechHelperPOC.Web.Models;

namespace TechHelperPOC.Web.Controllers
{
    public class LinuxInfoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        private void SetViewContent()
        {
            var pcName = new List<SelectListItem>();
            var pc = db.LinuxInfoes.Select(m => new SelectListItem
            {
                Value = m.MachineName,
                Text = m.MachineName
            }).Distinct();

            pcName.Add(new SelectListItem { Value = "", Text = "All" });
            pcName.InsertRange(1, pc);
            ViewBag.machineName = pcName;
         }

        // GET: LinuxInfoes
        public ActionResult Index()
        {
            SetViewContent();
            var linux = db.LinuxInfoes.OrderBy(m => m.MachineName).ThenByDescending(s => s.ScannedDate);
            return View(linux.ToList());
        }

        [HttpPost]
        public ActionResult Index(string machineName)
        {
            SetViewContent();
            var pc = db.LinuxInfoes.Where(m => m.MachineName == machineName)
                .OrderBy(m => m.MachineName).ThenByDescending(s => s.ScannedDate);
            return View(pc.ToList());
        }

        public ActionResult Report()
        {
            SetViewContent();
            var pc = db.LinuxInfoes.GroupBy(m => m.MachineName)
                .Select(g => g.OrderByDescending(s => s.ScannedDate).FirstOrDefault());
            return View(pc.ToList());
        }

        [HttpPost]
        public ActionResult Report(string machineName)
        {
            SetViewContent();
            if (machineName == null || machineName == string.Empty)
            {
              var  pc = db.LinuxInfoes.GroupBy(m => m.MachineName)
                    .Select(g => g.OrderByDescending(s => s.ScannedDate).FirstOrDefault());
              return View(pc.ToList());
            }
            else
            {
               var pc = db.LinuxInfoes.Where(m => m.MachineName == machineName).GroupBy(m => m.MachineName)
                    .Select(g => g.OrderByDescending(s => s.ScannedDate).FirstOrDefault());
            return View(pc.ToList());
            }

        }
        // GET: LinuxInfoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LinuxInfo linuxInfo = db.LinuxInfoes.Find(id);
            if (linuxInfo == null)
            {
                return HttpNotFound();
            }
            return View(linuxInfo);
        }

        // GET: LinuxInfoes/Create
        public ActionResult Create()
        {
            return PartialView("Create");
        }

        // POST: LinuxInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Technician,Client,Site,OS,MachineName,ScannedDate,FileName,HtmlFile")] LinuxInfo linuxInfo, HttpPostedFileBase xmlUploader)
        {
            if (ModelState.IsValid)
            {
                if (xmlUploader != null && xmlUploader.ContentLength > 0)
                {

                    string filename = xmlUploader.FileName;
                    string contentType = xmlUploader.ContentType;
                    byte[] data = new byte[xmlUploader.ContentLength];
                    xmlUploader.InputStream.Read(data, 0, xmlUploader.ContentLength);
                    string TextFile = System.Text.Encoding.UTF8.GetString(data);
                    linuxInfo.HtmlFile = TextFile;
                    linuxInfo.FileName = filename;
                    db.LinuxInfoes.Add(linuxInfo);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(linuxInfo);
        }

        // GET: LinuxInfoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LinuxInfo linuxInfo = db.LinuxInfoes.Find(id);
            if (linuxInfo == null)
            {
                return HttpNotFound();
            }
            return View(linuxInfo);
        }

        // POST: LinuxInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Technician,Client,Site,OS,MachineName,ScannedDate,FileName,HtmlFile")] LinuxInfo linuxInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(linuxInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(linuxInfo);
        }

        // GET: LinuxInfoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LinuxInfo linuxInfo = db.LinuxInfoes.Find(id);
            if (linuxInfo == null)
            {
                return HttpNotFound();
            }
            return View(linuxInfo);
        }

        // POST: LinuxInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LinuxInfo linuxInfo = db.LinuxInfoes.Find(id);
            db.LinuxInfoes.Remove(linuxInfo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
