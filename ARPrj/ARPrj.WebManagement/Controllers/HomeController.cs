using ARPrj.DataAccess;
using ARPrj.WebManagement.Models;
using System.Web.Mvc;
using System.Linq;
using System.Data.Entity;
using ARPrj.DataAccess.Model;
using ARPrj.Service;
using Microsoft.AspNet.Identity;

namespace ARPrj.WebManagement.Controllers
{
    public class HomeController : Controller
    {
        private ARPrjEntities db = new ARPrjEntities();

        protected IUserService _userManager { get; set; }
        public ActionResult Index()
        {
            ViewBag.To = new SelectList(db.Airports, "AirportId", "Name");
            ViewBag.From = new SelectList(db.Airports, "AirportId", "Name");
            SearchResultViewModel model = new SearchResultViewModel();
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult List()
        {
            return View();
        }


        public ActionResult Order()
        {
            var flights = db.Flights.Include(f => f.Airline).Include(f => f.Airport).Include(f => f.Airport1);
            return View(flights.ToList());
        }
        public ActionResult AirlineList()
        {
            var flights = db.Flights.Include(f => f.Airline).Include(f => f.Airport).Include(f => f.Airport1);
            return View(flights.ToList());
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        public ActionResult SearchFlights([Bind(Include = "From,To,DepartureDate,Amount")]SearchResultViewModel searchModel)
        {
            var flights = db.Flights.Include(f => f.Airline).Include(f => f.Airport).Include(f => f.Airport1)
                //.Where(x => x.DepartureDay.Value.Year == searchModel.DepartureDate.Year
                //            && x.DepartureDay.Value.Month == searchModel.DepartureDate.Month
                //            && x.DepartureDay.Value.Day == searchModel.DepartureDate.Day)
                //.Where(x => x.SeatsLeft.Value >= searchModel.Amount)
                .Where(x => x.Airport.AirportId == searchModel.To)
                .Where(x => x.Airport1.AirportId == searchModel.From);
            searchModel.Flights = flights.ToList();
            ViewBag.To = new SelectList(db.Airports, "AirportId", "Name",searchModel.To);
            ViewBag.From = new SelectList(db.Airports, "AirportId", "Name",searchModel.From);
            return View("index", searchModel);
        }
        //[HttpPost]
        //public ActionResult OrderDetail(OrderDetailViewModel orderDetail)
        //{
        //    var currentUser = _userManager.GetUserById(User.Identity.GetUserId());
        //    var user = new User();
        //    if (currentUser != null)
        //    {
        //        user = db.Users.Include(x=>x.Orders).FirstOrDefault(x => x.UserName == currentUser.UserName);
        //        if (user.Orders.FirstOrDefault(x=>x.OrderId==orderDetail.OrderId))

        //    }

        //    var order=new OrderDetail()
        //    {
        //        FlightId = orderDetail.FlightId,
        //        PhoneNumber = orderDetail.PhoneNumber,
        //        Count =orderDetail.Amount,
        //        CustomerFullName = orderDetail.FullName,
        //        TicketsTypeId = orderDetail?.TicketTypeId,
        //    };

        //}
        [HttpPost]
        public ActionResult SubmitOrder()
        {
            return View("");
        }
    }
}