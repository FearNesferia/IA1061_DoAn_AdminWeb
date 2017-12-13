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
using PagedList;
using AdminApp.Models;
using AdminApp.Models.DataHandler.R;

namespace AdminApp.Controllers.WebControllers
{
    public class TrafficPackagesController : Controller
    {
        private PakageDBContext db = new PakageDBContext();

        // GET: TrafficPackages
        public ActionResult Index(int? PageNumber, int? WebsiteChoice, string Search, string SortField)
        {
            IQueryable<TrafficPackage> query = db.TrafficPackages.Include(t => t.Website)
                                                                           .Where(x => Search == null
                                                                                  || x.Path.Contains(Search)
                                                                                  || x.QueryString.Contains(Search)
                                                                                  || x.Payload.Contains(Search));
            ViewBag.SortByUrl = string.IsNullOrEmpty(SortField) ? "Url_Desc" : string.Empty;
            ViewBag.SortByCreatedDate = SortField == "CreatedDate_Asc" ? "CreatedDate_Desc" : "CreatedDate_Asc";
            ViewBag.SortByPath = SortField == "Path_Asc" ? "Path_Desc" : "Path_Asc";
            ViewBag.SortByQueryString = SortField == "QueryString_Asc" ? "QueryString_Desc" : "QueryString_Asc";
            ViewBag.SortByPayLoad = SortField == "PayLoad_Asc" ? "PayLoad_Desc" : "PayLoad_Asc";
            ViewBag.SortByIsAttack = SortField == "IsAttack_Asc" ? "IsAttack_Desc" : "IsAttack_Asc";
            ViewBag.SortByIsChecked = SortField == "IsChecked_Asc" ? "IsChecked_Desc" : "IsChecked_Asc";

            switch (SortField)
            {
                case "Url_Desc": query = query.OrderByDescending(x => x.Website.Url); break;
                case "CreatedDate_Asc": query = query.OrderBy(x => x.CreatedDate); break;
                case "CreatedDate_Desc": query = query.OrderByDescending(x => x.CreatedDate); break;
                case "Path_Asc": query = query.OrderBy(x => x.Path); break;
                case "Path_Desc": query = query.OrderByDescending(x => x.Path); break;
                case "QueryString_Asc": query = query.OrderBy(x => x.QueryString); break;
                case "QueryString_Desc": query = query.OrderByDescending(x => x.QueryString); break;
                case "PayLoad_Asc": query = query.OrderBy(x => x.Payload); break;
                case "PayLoad_Desc": query = query.OrderByDescending(x => x.Payload); break;
                case "IsAttack_Asc": query = query.OrderBy(x => x.IsAttack); break;
                case "IsAttack_Desc": query = query.OrderByDescending(x => x.IsAttack); break;
                case "IsChecked_Asc": query = query.OrderBy(x => x.IsChecked); break;
                case "IsChecked_Desc": query = query.OrderByDescending(x => x.IsChecked); break;
                default: query = query.OrderBy(x => x.Website.Url); break; 
            }

            IPagedList<TrafficPackage> list = query.Where(x => !WebsiteChoice.HasValue || x.Website.WebsiteId == WebsiteChoice).ToPagedList((PageNumber ?? 1), 10);
            ViewBag.PageCount = list.PageCount;
            ViewBag.Websites = db.Websites.Distinct().ToList();
            return View(list);
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
            ViewBag.CurrentWebsite = db.Websites.Find(trafficPackage.WebsiteId).Url;
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
            //ViewBag.WebsiteId = new SelectList(db.Websites, "WebsiteId", "Url", trafficPackage.WebsiteId);
            ViewBag.CurrentWebsite = db.Websites.Find(trafficPackage.WebsiteId);
            TrafficPackageEditModel tp = new TrafficPackageEditModel()
            {
                TrafficPackageId = trafficPackage.TrafficPackageId,
                WebsiteUrl = db.Websites.Find(trafficPackage.WebsiteId).Url,
                Path = trafficPackage.Path,
                QueryString = trafficPackage.QueryString,
                Payload = trafficPackage.Payload,
                IsAttack = trafficPackage.IsAttack,
                IsChecked = trafficPackage.IsChecked
            };

            return View(tp);
        }

        // POST: TrafficPackages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TrafficPackageId,CreatedDate,Path,QueryString,Payload,IsChecked,IsAttack,LengthOfArguments,NumberOfArguments,NumberOfDigitsInArguments,NumberOfOtherCharInArguments,NumberOfDigitsInPath,NumberOfSpecialCharInArguments,LengthOfPath,LengthOfRequest,NumberOfLettersInArguments,NumberOfLettersCharInPath,NumberOfSepicalCharInPath,WebsiteId")] TrafficPackage trafficPackage)
        {
            TrafficPackage tp = db.TrafficPackages.Find(trafficPackage.TrafficPackageId);
            tp.IsChecked = trafficPackage.IsChecked;
            tp.IsAttack = trafficPackage.IsAttack;
            if (ModelState.IsValid)
            {
                db.Entry(tp).State = EntityState.Modified;
                //Call R To Update Tree
                int newRow = UpdateData.UpdateDataFunc(tp.WebsiteId);
                db.Websites.FirstOrDefault(x => x.WebsiteId == tp.WebsiteId).NumberOfPackageInTree = newRow;
                db.SaveChanges();
                return RedirectToAction("Index", "TrafficPackages", new {Search = Request.QueryString["Search"], WebsiteChoice = Request.QueryString["WebsiteChoice"] , SortField = Request.QueryString["SortField"] });
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


        [HttpGet]
        public ActionResult GetList()
        {
            return Json( new { list = db.TrafficPackages.Select(x => new { x = x.NumberOfDigitsInPath, y = x.LengthOfRequest }).ToList() }, JsonRequestBehavior.AllowGet);
            //return Json("zxc", JsonRequestBehavior.AllowGet);
        }
    }
}
