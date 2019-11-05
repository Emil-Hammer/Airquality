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
    public class StationsController : Controller
    {
        private Entities db = new Entities();

        // GET: Stations
        public ActionResult Index()
        {
            var stations = db.Stations.Include(s => s.UTM);
            return View(stations.ToList());
        }

        // GET: Stations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stations stations = db.Stations.Find(id);
            if (stations == null)
            {
                return HttpNotFound();
            }
            return View(stations);
        }

        // GET: Stations/Create
        public ActionResult Create()
        {
            ViewBag.GeometriId = new SelectList(db.UTM, "GeometriId", "GeometriId");
            return View();
        }

        // POST: Stations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaaleStedId,Navn,Akronym,GeometriId")] Stations stations)
        {
            if (ModelState.IsValid)
            {
                db.Stations.Add(stations);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GeometriId = new SelectList(db.UTM, "GeometriId", "GeometriId", stations.GeometriId);
            return View(stations);
        }

        // GET: Stations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stations stations = db.Stations.Find(id);
            if (stations == null)
            {
                return HttpNotFound();
            }
            ViewBag.GeometriId = new SelectList(db.UTM, "GeometriId", "GeometriId", stations.GeometriId);
            return View(stations);
        }

        // POST: Stations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaaleStedId,Navn,Akronym,GeometriId")] Stations stations)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stations).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GeometriId = new SelectList(db.UTM, "GeometriId", "GeometriId", stations.GeometriId);
            return View(stations);
        }

        // GET: Stations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stations stations = db.Stations.Find(id);
            if (stations == null)
            {
                return HttpNotFound();
            }
            return View(stations);
        }

        // POST: Stations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Stations stations = db.Stations.Find(id);
            db.Stations.Remove(stations);
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
