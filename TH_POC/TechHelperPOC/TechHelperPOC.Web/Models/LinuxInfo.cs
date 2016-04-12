using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TechHelperPOC.Web.Models
{
    public class LinuxInfo : IEntity
    {
        [Required]
        [Display(Name = "Technician Name")]
        public string Technician { get; set; }

        [Required]
        public string Client { get; set; }
        
        [Required]
        public string Site { get; set; }
        
        [Required]
        [Display(Name="Operating System")]
        public string OS { get; set; }

        [Required]
        [Display(Name = "Machine Name")]
        public string MachineName { get; set; }

        [Required]
        [Display(Name = "Date")]
        public string ScannedDate { get; set; }

        public string FileName { get; set; }
       
        public string HtmlFile { get; set; }

    }
}