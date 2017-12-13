using AdminApp.Models.DataHandler;
using IISServerModules.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminApp.Controllers.WebControllers
{
    public class HomeController : Controller
    {
        PakageDBContext db = new PakageDBContext();
        // GET: Home
        public ActionResult Index()
        {
            var result = db.TrafficPackages.ToList();

            return View();

            //TIEN
            //var result = db.TrafficPackages.Where(x => x.WebsiteId == 6).ToList();
            //var result2 = new List<TrafficPackage>();
            //foreach (var item in result)
            //{
            //    if(!result2.Any(x => x.NumberOfDigitsInArguments == item.NumberOfDigitsInArguments)
            //        ||!result2.Any(x => x.LengthOfArguments == item.LengthOfArguments))
            //    {
            //        result2.Add(item);
            //    }
            //}
            //string listRed = "[";
            //string listBlue = "[";
            //result2.Where(x => x.IsAttack).ToList().ForEach(x => listRed += $"{{x:{x.NumberOfDigitsInArguments},y:{x.LengthOfArguments}}},");
            //listRed += "]";
            //result2.Where(x => !x.IsAttack).ToList().ForEach(x => listBlue += $"{{x:{x.NumberOfDigitsInArguments},y:{x.LengthOfArguments}}},");
            //listBlue += "]";

            //ViewBag.maxX = result.Max(x => x.NumberOfDigitsInArguments) + 10;
            //ViewBag.maxY = result.Max(x => x.LengthOfArguments) +10;

            //ViewBag.listRed = listRed;
            //ViewBag.listBlue = listBlue;
            //return View(result);
        }

        public ActionResult DataMiningIndex()
        {
            return View();
        }
    }
}