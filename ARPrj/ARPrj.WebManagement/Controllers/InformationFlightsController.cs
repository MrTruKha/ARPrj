using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ARPrj.DataAccess;

namespace ARPrj.WebManagement.Controllers
{
    public class InformationFlightsController : Controller
    {
        private ARPrjEntities db = new ARPrjEntities();

        // GET: InformationFlights
        public ActionResult Index()
        {
            var informationFlights = db.InformationFlights.Include(i => i.Flight).Include(i => i.TicketsType);
            return View(informationFlights.ToList());
        }

        // GET: InformationFlights/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InformationFlight informationFlight = db.InformationFlights.Find(id);
            if (informationFlight == null)
            {
                return HttpNotFound();
            }
            return View(informationFlight);
        }

        // GET: InformationFlights/Create
        public ActionResult Create()
        {
            ViewBag.FlightId = new SelectList(db.Flights, "FlightId", "FlightId");
            ViewBag.TicketsTypeId = new SelectList(db.TicketsTypes, "TicketsTypeId", "TypeName");
            return View();
        }

        // POST: InformationFlights/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Count,InformationFlightID,TicketsTypeId,CreateDate,UpdateDate,FlightId")] InformationFlight informationFlight)
        {
            if (ModelState.IsValid)
            {
                db.InformationFlights.Add(informationFlight);
                db.SaveChanges();
                var flight = db.Flights.FirstOrDefault(x => x.FlightId == informationFlight.FlightId);
                flight.SeatsLeft = db.InformationFlights.Where(x => x.FlightId == informationFlight.FlightId).Sum(x => x.Count);
                db.Entry(flight).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FlightId = new SelectList(db.Flights, "FlightId", "FlightId", informationFlight.FlightId);
            ViewBag.TicketsTypeId = new SelectList(db.TicketsTypes, "TicketsTypeId", "TypeName", informationFlight.TicketsTypeId);
            return View(informationFlight);
        }

        // GET: InformationFlights/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InformationFlight informationFlight = db.InformationFlights.Find(id);
            if (informationFlight == null)
            {
                return HttpNotFound();
            }
            ViewBag.FlightId = new SelectList(db.Flights, "FlightId", "FlightId", informationFlight.FlightId);
            ViewBag.TicketsTypeId = new SelectList(db.TicketsTypes, "TicketsTypeId", "TypeName", informationFlight.TicketsTypeId);
            return View(informationFlight);
        }

        // POST: InformationFlights/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Count,InformationFlightID,TicketsTypeId,CreateDate,UpdateDate,FlightId")] InformationFlight informationFlight)
        {
            if (ModelState.IsValid)
            {
                db.Entry(informationFlight).State = EntityState.Modified;
                db.SaveChanges();
                var flight = db.Flights.FirstOrDefault(x => x.FlightId == informationFlight.FlightId);
                flight.SeatsLeft = db.InformationFlights.Where(x => x.FlightId == informationFlight.FlightId).Sum(x => x.Count);
                db.Entry(flight).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FlightId = new SelectList(db.Flights, "FlightId", "FlightId", informationFlight.FlightId);
            ViewBag.TicketsTypeId = new SelectList(db.TicketsTypes, "TicketsTypeId", "TypeName", informationFlight.TicketsTypeId);
            return View(informationFlight);
        }

        // GET: InformationFlights/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InformationFlight informationFlight = db.InformationFlights.Find(id);
            if (informationFlight == null)
            {
                return HttpNotFound();
            }
            return View(informationFlight);
        }

        // POST: InformationFlights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InformationFlight informationFlight = db.InformationFlights.Find(id);
            db.InformationFlights.Remove(informationFlight);
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
