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
    public class UnitsController : Controller
    {
        private Entities db = new Entities();

        // GET: Units
        public ActionResult Index()
        {
            return View(db.Units.ToList());
        }

        // GET: Units/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Units units = db.Units.Find(id);
            if (units == null)
            {
                return HttpNotFound();
            }
            return View(units);
        }

        // GET: Units/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Units/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EnhedId,Navn")] Units units)
        {
            if (ModelState.IsValid)
            {
                db.Units.Add(units);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(units);
        }

        // GET: Units/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Units units = db.Units.Find(id);
            if (units == null)
            {
                return HttpNotFound();
            }
            return View(units);
        }

        // POST: Units/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EnhedId,Navn")] Units units)
        {
            if (ModelState.IsValid)
            {
                db.Entry(units).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(units);
        }

        // GET: Units/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Units units = db.Units.Find(id);
            if (units == null)
            {
                return HttpNotFound();
            }
            return View(units);
        }

        // POST: Units/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Units units = db.Units.Find(id);
            db.Units.Remove(units);
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
