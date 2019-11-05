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
    public class EquipmentsController : Controller
    {
        private Entities db = new Entities();

        // GET: Equipments
        public ActionResult Index()
        {
            return View(db.Equipments.ToList());
        }

        // GET: Equipments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipments equipments = db.Equipments.Find(id);
            if (equipments == null)
            {
                return HttpNotFound();
            }
            return View(equipments);
        }

        // GET: Equipments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Equipments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UdstyrId,Navn")] Equipments equipments)
        {
            if (ModelState.IsValid)
            {
                db.Equipments.Add(equipments);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(equipments);
        }

        // GET: Equipments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipments equipments = db.Equipments.Find(id);
            if (equipments == null)
            {
                return HttpNotFound();
            }
            return View(equipments);
        }

        // POST: Equipments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UdstyrId,Navn")] Equipments equipments)
        {
            if (ModelState.IsValid)
            {
                db.Entry(equipments).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(equipments);
        }

        // GET: Equipments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipments equipments = db.Equipments.Find(id);
            if (equipments == null)
            {
                return HttpNotFound();
            }
            return View(equipments);
        }

        // POST: Equipments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Equipments equipments = db.Equipments.Find(id);
            db.Equipments.Remove(equipments);
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
