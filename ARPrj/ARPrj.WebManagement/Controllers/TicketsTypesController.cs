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
    public class TicketsTypesController : Controller
    {
        private ARPrjEntities db = new ARPrjEntities();

        // GET: TicketsTypes
        public ActionResult Index()
        {
            return View(db.TicketsTypes.ToList());
        }

        // GET: TicketsTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketsType ticketsType = db.TicketsTypes.Find(id);
            if (ticketsType == null)
            {
                return HttpNotFound();
            }
            return View(ticketsType);
        }

        // GET: TicketsTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TicketsTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TicketsTypeId,TypeName,CreateDate,UpdateDate")] TicketsType ticketsType)
        {
            if (ModelState.IsValid)
            {
                db.TicketsTypes.Add(ticketsType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ticketsType);
        }

        // GET: TicketsTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketsType ticketsType = db.TicketsTypes.Find(id);
            if (ticketsType == null)
            {
                return HttpNotFound();
            }
            return View(ticketsType);
        }

        // POST: TicketsTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TicketsTypeId,TypeName,CreateDate,UpdateDate")] TicketsType ticketsType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticketsType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ticketsType);
        }

        // GET: TicketsTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketsType ticketsType = db.TicketsTypes.Find(id);
            if (ticketsType == null)
            {
                return HttpNotFound();
            }
            return View(ticketsType);
        }

        // POST: TicketsTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TicketsType ticketsType = db.TicketsTypes.Find(id);
            db.TicketsTypes.Remove(ticketsType);
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
