using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechHelperPOC.Web.Models
{
    public class Auditor : IEntity
    {
        public string Technician { get; set; }
        public string Client { get; set; }
        public string Site { get; set; }
        public string Workstation { get; set; }
        public string OS { get; set; }
        public string ScannedDate { get; set; }
    }
}