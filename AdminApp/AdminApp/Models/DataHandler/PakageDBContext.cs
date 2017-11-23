using IISServerModules.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AdminApp.Models.DataHandler
{
    public class PakageDBContext:DbContext
    {
        public DbSet<TrafficPackage> TrafficPackages { get; set; }
        public DbSet<Website> Websites { get; set; }
    }
}