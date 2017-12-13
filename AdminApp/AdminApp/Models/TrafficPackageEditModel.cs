using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminApp.Models
{
    public class TrafficPackageEditModel
    {
        public int TrafficPackageId { get; set; }
        public string WebsiteUrl { get; set; }
        public string Path { get; set; }
        public string QueryString { get; set; }
        public string Payload { get; set; }
        public bool IsAttack{ get; set; }
        public bool IsChecked { get; set; }
    }
}