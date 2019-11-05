using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Airquality;
using Airquality.Models;

namespace Airquality.Controllers
{
    public class UTMsController : Controller
    {
        private Entities db = new Entities();

        // GET: UTMs
        public ActionResult Index()
        {
            return View(db.UTM.ToList());
        }

        // GET: UTMs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UTM uTM = db.UTM.Find(id);
            if (uTM == null)
            {
                return HttpNotFound();
            }
            return View(uTM);
        }

        // GET: UTMs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UTMs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GeometriId,UTMX,UTMY,UTMZone")] UTM uTM)
        {
            if (ModelState.IsValid)
            {
                db.UTM.Add(uTM);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(uTM);
        }

        // GET: UTMs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UTM uTM = db.UTM.Find(id);
            if (uTM == null)
            {
                return HttpNotFound();
            }
            return View(uTM);
        }

        // POST: UTMs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GeometriId,UTMX,UTMY,UTMZone")] UTM uTM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uTM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(uTM);
        }

        // GET: UTMs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UTM uTM = db.UTM.Find(id);
            if (uTM == null)
            {
                return HttpNotFound();
            }
            return View(uTM);
        }

        // POST: UTMs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UTM uTM = db.UTM.Find(id);
            db.UTM.Remove(uTM);
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
