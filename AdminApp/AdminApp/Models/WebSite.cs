using IISServerModules.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdminApp.Models
{
    [Table("Websites")]
    public class Website
    {
        [Key]
        public int WebsiteId { get; set; }

        [Required]
        public string Url { get; set; }

        [Required]
        [Display(Name = "Is Detected Mode")]
        public bool IsDetecMode { get; set; }

        [Display(Name ="Checked Packages")]
        public int NumberOfPackageInTree { get; set; } = 0;

        //relationship
        public IList<TrafficPackage> TrafficPackages { get; set; }
    }
}