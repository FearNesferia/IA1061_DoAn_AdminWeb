using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdminApp.Models.DataHandler;
using IISServerModules.Models;

namespace AdminApp.Controllers.WebControllers
{
    public class TrafficPackagesController : Controller
    {
        private PakageDBContext db = new PakageDBContext();

        // GET: TrafficPackages
        public ActionResult Index()
        {
            var trafficPackages = db.TrafficPackages.Include(t => t.Website);
            return View(trafficPackages.ToList());
        }

        // GET: TrafficPackages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrafficPackage trafficPackage = db.TrafficPackages.Find(id);
            if (trafficPackage == null)
            {
                return HttpNotFound();
            }
            return View(trafficPackage);
        }

        // GET: TrafficPackages/Create
        public ActionResult Create()
        {
            ViewBag.WebsiteId = new SelectList(db.Websites, "WebsiteId", "Url");
            return View();
        }

        // POST: TrafficPackages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TrafficPackageId,CreatedDate,Path,QueryString,Payload,IsChecked,IsAttack,LengthOfArguments,NumberOfArguments,NumberOfDigitsInArguments,NumberOfOtherCharInArguments,NumberOfDigitsInPath,NumberOfSpecialCharInArguments,LengthOfPath,LengthOfRequest,NumberOfLettersInArguments,NumberOfLettersCharInPath,NumberOfSepicalCharInPath,WebsiteId")] TrafficPackage trafficPackage)
        {
            if (ModelState.IsValid)
            {
                db.TrafficPackages.Add(trafficPackage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.WebsiteId = new SelectList(db.Websites, "WebsiteId", "Url", trafficPackage.WebsiteId);
            return View(trafficPackage);
        }

        // GET: TrafficPackages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrafficPackage trafficPackage = db.TrafficPackages.Find(id);
            if (trafficPackage == null)
            {
                return HttpNotFound();
            }
            ViewBag.WebsiteId = new SelectList(db.Websites, "WebsiteId", "Url", trafficPackage.WebsiteId);
            return View(trafficPackage);
        }

        // POST: TrafficPackages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TrafficPackageId,CreatedDate,Path,QueryString,Payload,IsChecked,IsAttack,LengthOfArguments,NumberOfArguments,NumberOfDigitsInArguments,NumberOfOtherCharInArguments,NumberOfDigitsInPath,NumberOfSpecialCharInArguments,LengthOfPath,LengthOfRequest,NumberOfLettersInArguments,NumberOfLettersCharInPath,NumberOfSepicalCharInPath,WebsiteId")] TrafficPackage trafficPackage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trafficPackage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.WebsiteId = new SelectList(db.Websites, "WebsiteId", "Url", trafficPackage.WebsiteId);
            return View(trafficPackage);
        }

        // GET: TrafficPackages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrafficPackage trafficPackage = db.TrafficPackages.Find(id);
            if (trafficPackage == null)
            {
                return HttpNotFound();
            }
            return View(trafficPackage);
        }

        // POST: TrafficPackages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TrafficPackage trafficPackage = db.TrafficPackages.Find(id);
            db.TrafficPackages.Remove(trafficPackage);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
