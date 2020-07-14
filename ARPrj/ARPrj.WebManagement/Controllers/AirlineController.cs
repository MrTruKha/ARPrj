using ARPrj.Model.Models.AirLine;
using ARPrj.Services.Service;
using PMS.Web.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ARPrj.WebManagement.Controllers
{
    [Authorize]
    [CustomAuthorize("Admin")]
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
        public ActionResult Edit(int id)
        {
            var model = _airlineService.GetAirlines().FirstOrDefault(x => x.AirlineId == id);
            return View(model);
        }
        [HttpPost, ActionName("Edit")]
        public ActionResult EditAirline( AirlineModel model)//[Bind(Include = "AirLineId,AirlineName,NetPrice,CreateDate,UpdateDate")]
        {
            //var model = _airlineService.GetAirlines().FirstOrDefault(x => x.AirlineId == id);
            _airlineService.EditAirline(model);
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            var model = _airlineService.GetAirlines().FirstOrDefault(x => x.AirlineId == id);
            return View(model);
        }

    }
}