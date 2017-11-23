using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdminApp.Models;
using AdminApp.Models.DataHandler;

namespace AdminApp.Controllers.WebControllers
{
    public class WebsitesController : Controller
    {
        private PakageDBContext db = new PakageDBContext();

        // GET: Websites
        public ActionResult Index()
        {
            return View(db.Websites.ToList());
        }

        // GET: Websites/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Website website = db.Websites.Find(id);
            if (website == null)
            {
                return HttpNotFound();
            }
            return View(website);
        }

        // GET: Websites/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Websites/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WebsiteId,Url,IsDetecMode")] Website website)
        {
            if (ModelState.IsValid)
            {
                db.Websites.Add(website);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(website);
        }

        // GET: Websites/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Website website = db.Websites.Find(id);
            if (website == null)
            {
                return HttpNotFound();
            }
            return View(website);
        }

        // POST: Websites/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WebsiteId,Url,IsDetecMode")] Website website)
        {
            if (ModelState.IsValid)
            {
                db.Entry(website).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(website);
        }

        // GET: Websites/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Website website = db.Websites.Find(id);
            if (website == null)
            {
                return HttpNotFound();
            }
            return View(website);
        }

        // POST: Websites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Website website = db.Websites.Find(id);
            db.Websites.Remove(website);
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
