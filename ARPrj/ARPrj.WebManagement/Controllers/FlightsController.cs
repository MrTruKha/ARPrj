using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ARPrj.DataAccess;
using ARPrj.WebManagement.Models;
using PMS.Web.Security;

namespace ARPrj.WebManagement.Controllers
{
    [Authorize]
    [CustomAuthorize("Admin")]
    public class FlightsController : Controller
    {
        private ARPrjEntities db = new ARPrjEntities();

        // GET: Flights
        public ActionResult Index()
        {
            var flights = db.Flights.Include(f => f.Airline).Include(f=>f.Airport).Include(f=>f.Airport1);
            return View(flights.ToList());
        }

        // GET: Flights/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight flight = db.Flights.Find(id);
            if (flight == null)
            {
                return HttpNotFound();
            }
            return View(flight);
        }

        // GET: Flights/Create
        public ActionResult Create()
        {
            ViewBag.AirlineId = new SelectList(db.Airlines, "AirlineId", "AirlineName");
            ViewBag.To = new SelectList(db.Airports, "AirportId", "Name");
            ViewBag.From = new SelectList(db.Airports, "AirportId", "Name");
            return View();
        }

        // POST: Flights/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FlightId,From,To,DepartureTime,ArrivalTime,DepartureDay,ArrivalDay,SeatsLeft,InformationFlightID,AirlineId,CreateDate,UpdateDate")] Flight flight)
        {
            if (ModelState.IsValid)
            {
                db.Flights.Add(flight);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AirlineId = new SelectList(db.Airlines, "AirlineId", "AirlineName", flight.AirlineId);
            ViewBag.To = new SelectList(db.Airports, "AirportId", "Name", flight.To);
            ViewBag.From = new SelectList(db.Airports, "AirportId", "Name", flight.From);
            return View(flight);
        }

        // GET: Flights/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight flight = db.Flights.Find(id);
            if (flight == null)
            {
                return HttpNotFound();
            }
            ViewBag.AirlineId = new SelectList(db.Airlines, "AirlineId", "AirlineName", flight.AirlineId);
            ViewBag.To = new SelectList(db.Airports, "AirportId", "Name", flight.To);
            ViewBag.From = new SelectList(db.Airports, "AirportId", "Name", flight.From);
            return View(flight);
        }

        // POST: Flights/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FlightId,From,To,DepartureTime,ArrivalTime,DepartureDay,ArrivalDay,SeatsLeft,InformationFlightID,AirlineId,CreateDate,UpdateDate")] Flight flight)
        {
            if (ModelState.IsValid)
            {
                db.Entry(flight).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AirlineId = new SelectList(db.Airlines, "AirlineId", "AirlineName", flight.AirlineId);
            ViewBag.To = new SelectList(db.Airports, "AirportId", "Name", flight.To);
            ViewBag.From = new SelectList(db.Airports, "AirportId", "Name", flight.From);
            return View(flight);
        }

        // GET: Flights/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight flight = db.Flights.Find(id);
            if (flight == null)
            {
                return HttpNotFound();
            }
            return View(flight);
        }

        // POST: Flights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Flight flight = db.Flights.Find(id);
            db.Flights.Remove(flight);
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
