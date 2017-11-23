using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminApp.Models
{
    public class TrafficPackageModel
    {

        public string WebsiteUrl { get; set; }
        public string Path { get; set; }
        public string QueryString { get; set; }
        public string Payload { get; set; }
    }
}