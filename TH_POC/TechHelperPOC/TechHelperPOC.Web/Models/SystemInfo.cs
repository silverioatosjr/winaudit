using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TechHelperPOC.Web.Models
{
    public class SystemInfo : IEntity
    {
        public string MachineName { get; set; }

        public string ManagementGroup { get; set; }

        public string DisplayGroup { get; set;}

        public int Sequence { get; set; }

        [Display(Name="Property")]
        public string PropertyName { get; set; }

        [Display(Name = "Value")]
        public string PropertyValue { get; set; }

        public string ScannedDate { get; set; }

        public string MacAddress { get; set; }
    }

    
}