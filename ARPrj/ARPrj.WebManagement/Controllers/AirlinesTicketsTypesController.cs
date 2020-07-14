using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ARPrj.DataAccess;
using PMS.Web.Security;

namespace ARPrj.WebManagement.Controllers
{
    [Authorize]
    [CustomAuthorize("Admin")]
    public class AirlinesTicketsTypesController : Controller
    {
        private ARPrjEntities db = new ARPrjEntities();

        // GET: AirlinesTicketsTypes
        public ActionResult Index()
        {
            var airlinesTicketsTypes = db.AirlinesTicketsTypes.Include(a => a.Airline).Include(a => a.TicketsType);
            return View(airlinesTicketsTypes.ToList());
        }

        // GET: AirlinesTicketsTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AirlinesTicketsType airlinesTicketsType = db.AirlinesTicketsTypes.Find(id);
            if (airlinesTicketsType == null)
            {
                return HttpNotFound();
            }
            return View(airlinesTicketsType);
        }

        // GET: AirlinesTicketsTypes/Create
        public ActionResult Create()
        {
            ViewBag.AirlineId = new SelectList(db.Airlines, "AirlineId", "AirlineName");
            ViewBag.TicketsTypeId = new SelectList(db.TicketsTypes, "TicketsTypeId", "TypeName");
            return View();
        }

        // POST: AirlinesTicketsTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NumberOfSeats,AirlineId,TicketsTypeId,Price,CreateDate,UpdateDate")] AirlinesTicketsType airlinesTicketsType)
        {
            if (ModelState.IsValid)
            {
                db.AirlinesTicketsTypes.Add(airlinesTicketsType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AirlineId = new SelectList(db.Airlines, "AirlineId", "AirlineName", airlinesTicketsType.AirlineId);
            ViewBag.TicketsTypeId = new SelectList(db.TicketsTypes, "TicketsTypeId", "TypeName", airlinesTicketsType.TicketsTypeId);
            return View(airlinesTicketsType);
        }

        // GET: AirlinesTicketsTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AirlinesTicketsType airlinesTicketsType = db.AirlinesTicketsTypes.Find(id);
            if (airlinesTicketsType == null)
            {
                return HttpNotFound();
            }
            ViewBag.AirlineId = new SelectList(db.Airlines, "AirlineId", "AirlineName", airlinesTicketsType.AirlineId);
            ViewBag.TicketsTypeId = new SelectList(db.TicketsTypes, "TicketsTypeId", "TypeName", airlinesTicketsType.TicketsTypeId);
            return View(airlinesTicketsType);
        }

        // POST: AirlinesTicketsTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NumberOfSeats,AirlineId,TicketsTypeId,Price,CreateDate,UpdateDate")] AirlinesTicketsType airlinesTicketsType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(airlinesTicketsType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AirlineId = new SelectList(db.Airlines, "AirlineId", "AirlineName", airlinesTicketsType.AirlineId);
            ViewBag.TicketsTypeId = new SelectList(db.TicketsTypes, "TicketsTypeId", "TypeName", airlinesTicketsType.TicketsTypeId);
            return View(airlinesTicketsType);
        }

        // GET: AirlinesTicketsTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AirlinesTicketsType airlinesTicketsType = db.AirlinesTicketsTypes.Find(id);
            if (airlinesTicketsType == null)
            {
                return HttpNotFound();
            }
            return View(airlinesTicketsType);
        }

        // POST: AirlinesTicketsTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AirlinesTicketsType airlinesTicketsType = db.AirlinesTicketsTypes.Find(id);
            db.AirlinesTicketsTypes.Remove(airlinesTicketsType);
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
