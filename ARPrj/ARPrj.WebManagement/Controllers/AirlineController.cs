using ARPrj.Model.Models.AirLine;
using ARPrj.Services.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ARPrj.WebManagement.Controllers
{
    public class AirlineController : Controller
    {
        private readonly IAirlineService _airlineService;
        public AirlineController(IAirlineService airlineService)
        {
            _airlineService = airlineService;
        }
        // GET: Airline
        public ActionResult Index()
        {
            var models = _airlineService.GetAirlines();
            return View(models);
        }
        //GET:Create
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(AirlineModel model)
        {          
            _airlineService.CreateAirline(model);
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            var model = _airlineService.GetAirlines().FirstOrDefault(x => x.AirlineId == id);
            return View(model);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteAirline(int? id)
        {
            var model = _airlineService.GetAirlines().FirstOrDefault(x => x.AirlineId == id);
            _airlineService.DeleteAirline(model);
            return RedirectToAction("Index");
        }

    }
}