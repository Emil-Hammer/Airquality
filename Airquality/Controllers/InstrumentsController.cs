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
    public class InstrumentsController : Controller
    {
        private Entities db = new Entities();

        // GET: Instruments
        public ActionResult Index()
        {
            var instruments = db.Instruments.Include(i => i.Equipments).Include(i => i.Stations);
            return View(instruments.ToList());
        }

        // GET: Instruments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instruments instruments = db.Instruments.Find(id);
            if (instruments == null)
            {
                return HttpNotFound();
            }
            return View(instruments);
        }

        // GET: Instruments/Create
        public ActionResult Create()
        {
            ViewBag.UdstyrId = new SelectList(db.Equipments, "UdstyrId", "Navn");
            ViewBag.MaalestedId = new SelectList(db.Stations, "MaaleStedId", "Navn");
            return View();
        }

        // POST: Instruments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OpstillingId,Kode,MaalestedId,UdstyrId")] Instruments instruments)
        {
            if (ModelState.IsValid)
            {
                db.Instruments.Add(instruments);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UdstyrId = new SelectList(db.Equipments, "UdstyrId", "Navn", instruments.UdstyrId);
            ViewBag.MaalestedId = new SelectList(db.Stations, "MaaleStedId", "Navn", instruments.MaalestedId);
            return View(instruments);
        }

        // GET: Instruments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instruments instruments = db.Instruments.Find(id);
            if (instruments == null)
            {
                return HttpNotFound();
            }
            ViewBag.UdstyrId = new SelectList(db.Equipments, "UdstyrId", "Navn", instruments.UdstyrId);
            ViewBag.MaalestedId = new SelectList(db.Stations, "MaaleStedId", "Navn", instruments.MaalestedId);
            return View(instruments);
        }

        // POST: Instruments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OpstillingId,Kode,MaalestedId,UdstyrId")] Instruments instruments)
        {
            if (ModelState.IsValid)
            {
                db.Entry(instruments).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UdstyrId = new SelectList(db.Equipments, "UdstyrId", "Navn", instruments.UdstyrId);
            ViewBag.MaalestedId = new SelectList(db.Stations, "MaaleStedId", "Navn", instruments.MaalestedId);
            return View(instruments);
        }

        // GET: Instruments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instruments instruments = db.Instruments.Find(id);
            if (instruments == null)
            {
                return HttpNotFound();
            }
            return View(instruments);
        }

        // POST: Instruments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Instruments instruments = db.Instruments.Find(id);
            db.Instruments.Remove(instruments);
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
