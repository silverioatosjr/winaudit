using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechHelperPOC.Web.Models;

namespace TechHelperPOC.Web.ViewModel
{
    public class SystemAuditViewModel
    {
        public SystemAuditViewModel()
        {
            SystemAudits = new List<SystemAudit>();
        }
        public List<SystemAudit> SystemAudits { get; set; }
    }

    public class SystemAudit
    {
        public string Technician { get; set; }

        public string Client { get; set; }
        
        public string Site { get; set; }
        
        public string Workstation { get; set; }
        
        public string OS { get; set; }
        
        public string MachineName { get; set; }

        public string ManagementGroup { get; set; }

        public string DisplayGroup { get; set; }
               
        public string PropertyName { get; set; }

        public string PropertyValue { get; set; }

        public string ScannedDate { get; set; }

        public string MacAddress { get; set; }
    }
}