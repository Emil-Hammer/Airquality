using Airquality.Models;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.UI.DataVisualization.Charting;
using PagedList;

namespace Airquality.Controllers
{
    public class MeasurementsController : Controller
    {
        private Entities db = new Entities();

        // GET: Measurements
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var measurements = db.Measurements.Include(m => m.Compounds).Include(m => m.Instruments)
                .Include(m => m.Units);
            if (!String.IsNullOrEmpty(searchString))
            {
                measurements = measurements.Where(s => s.Compounds.StofNavn.Contains(searchString)
                                               || s.Instruments.Stations.Navn.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    measurements = measurements.OrderByDescending(s => s.Id);
                    break;
                case "Date":
                    measurements = measurements.OrderBy(s => s.DatoMaerke);
                    break;
                case "date_desc":
                    measurements = measurements.OrderByDescending(s => s.DatoMaerke);
                    break;
                default:
                    measurements = measurements.OrderBy(s => s.Id);
                    break;
            }

            int pageSize = 250;
            int pageNumber = (page ?? 1);
            return View(measurements.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult CreateLineChart()
        {
            //Create bar chart      
            var measurements = db.Measurements.GroupBy(x => x.Compounds.StofNavn, x => x.Resultat,
                (key, m) => new {StofId = key, TotalResult = m.Average()});

            Series s = new Series();
            s.ChartType = SeriesChartType.Pie;
            
            // Filtrering af enkel fejlmåling
            s.Points.DataBindXY(measurements.Select(x => x.StofId), measurements.Select(x => x.TotalResult).Where(x => x < 50));
            s.IsValueShownAsLabel = true;
            s["PieLabelStyle"] = "Outside";
            s["PieLineColor"] = "Black";
            s.Palette = ChartColorPalette.Excel;

            Chart c = new Chart();
            c.ChartAreas.Add(new ChartArea());
            c.Series.Add(s);
            c.Width = 800;
            c.Height = 400;
            c.Legends.Add(new Legend("Legend"));
            c.ChartAreas[0].Area3DStyle.Enable3D = true;
            c.ChartAreas[0].Area3DStyle.Inclination = 0;

            foreach (DataPoint p in c.Series[0].Points)
            {
                p.Label = "#PERCENT - #VALX";
            }

            using (MemoryStream ms = new MemoryStream())
            {
                c.SaveImage(ms, ChartImageFormat.Png);

                return File(ms.ToArray(), "image/bytes");
            }
        }

        // GET: Measurements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Measurements measurements = db.Measurements.Find(id);
            if (measurements == null)
            {
                return HttpNotFound();
            }
            return View(measurements);
        }

        // GET: Measurements/Create
        public ActionResult Create()
        {
            ViewBag.StofId = new SelectList(db.Compounds, "StofId", "StofNavn");
            ViewBag.OpstillingsId = new SelectList(db.Instruments, "OpstillingId", "Kode");
            ViewBag.EnhedId = new SelectList(db.Units, "EnhedId", "Navn");
            return View();
        }

        // POST: Measurements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DatoMaerke,OpstillingsId,Resultat,EnhedId,StofId")] Measurements measurements)
        {
            if (ModelState.IsValid)
            {
                db.Measurements.Add(measurements);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StofId = new SelectList(db.Compounds, "StofId", "StofNavn", measurements.StofId);
            ViewBag.OpstillingsId = new SelectList(db.Instruments, "OpstillingId", "Kode", measurements.OpstillingsId);
            ViewBag.EnhedId = new SelectList(db.Units, "EnhedId", "Navn", measurements.EnhedId);
            return View(measurements);
        }

        // GET: Measurements/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Measurements measurements = db.Measurements.Find(id);
            if (measurements == null)
            {
                return HttpNotFound();
            }
            ViewBag.StofId = new SelectList(db.Compounds, "StofId", "StofNavn", measurements.StofId);
            ViewBag.OpstillingsId = new SelectList(db.Instruments, "OpstillingId", "Kode", measurements.OpstillingsId);
            ViewBag.EnhedId = new SelectList(db.Units, "EnhedId", "Navn", measurements.EnhedId);
            return View(measurements);
        }

        // POST: Measurements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DatoMaerke,OpstillingsId,Resultat,EnhedId,StofId")] Measurements measurements)
        {
            if (ModelState.IsValid)
            {
                db.Entry(measurements).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StofId = new SelectList(db.Compounds, "StofId", "StofNavn", measurements.StofId);
            ViewBag.OpstillingsId = new SelectList(db.Instruments, "OpstillingId", "Kode", measurements.OpstillingsId);
            ViewBag.EnhedId = new SelectList(db.Units, "EnhedId", "Navn", measurements.EnhedId);
            return View(measurements);
        }

        // GET: Measurements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Measurements measurements = db.Measurements.Find(id);
            if (measurements == null)
            {
                return HttpNotFound();
            }
            return View(measurements);
        }

        // POST: Measurements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Measurements measurements = db.Measurements.Find(id);
            db.Measurements.Remove(measurements);
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
