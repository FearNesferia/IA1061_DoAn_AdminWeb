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
        public bool IsDetecMode { get; set; }



        //relationship
        public IList<TrafficPackage> TrafficPackages{ get; set; }
    }
}