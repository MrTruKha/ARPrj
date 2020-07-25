using ARPrj.DataAccess;
using ARPrj.WebManagement.Models;
using System.Web.Mvc;
using System.Linq;
using System.Data.Entity;
using ARPrj.DataAccess.Model;
using ARPrj.Service;
using Microsoft.AspNet.Identity;
using Microsoft.Ajax.Utilities;
using System.Threading.Tasks;
using System.Configuration;

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

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        public ActionResult SearchFlights([Bind(Include = "From,To,DepartureDate,Amount")]SearchResultViewModel searchModel)
        {
            var flights = db.Flights.Include(f => f.Airline).Include(f => f.Airport).Include(f => f.Airport1)
                .Where(x => x.DepartureDay.Value.Year == searchModel.DepartureDate.Year
                            && x.DepartureDay.Value.Month == searchModel.DepartureDate.Month
                            && x.DepartureDay.Value.Day == searchModel.DepartureDate.Day)
                .Where(x => x.SeatsLeft.Value >= searchModel.Amount)
                .Where(x => x.Airport.AirportId == searchModel.To)
                .Where(x => x.Airport1.AirportId == searchModel.From);
            searchModel.Flights = flights.ToList();
            ViewBag.To = new SelectList(db.Airports, "AirportId", "Name",searchModel.To);
            ViewBag.From = new SelectList(db.Airports, "AirportId", "Name",searchModel.From);
            return View("index", searchModel);
        }
        [HttpPost]
        public async Task<ActionResult> OrderDetail(OrderDetailViewModel orderDetail)
        {
            var currentUser = _userManager.GetUserById(User.Identity.GetUserId());
            var user = new User();
            if (currentUser != null)
            {
                user = db.Users.Include(x=>x.Orders).FirstOrDefault(x => x.UserName == currentUser.UserName);             

            }       
            if (orderDetail.OrderId==0 && user!=null)
            {
                var orderEntity = new Order() { CustomerId = user.UserId };
                db.Orders.Add(orderEntity);
                db.SaveChanges();
                orderDetail.OrderId = orderEntity.OrderId;
            }

            var order=new OrderDetail()
            {
                FlightId = orderDetail.FlightId,
                PhoneNumber = orderDetail.PhoneNumber,
                Count =orderDetail.Amount,
                CustomerFullName = orderDetail.FullName,
                TicketsTypeId = orderDetail?.TicketTypeId,
                OrderId=user!=null?orderDetail.OrderId:0
            };
            db.OrderDetails.Add(order);
            db.SaveChanges();
            string content = "You are tasked:<br /> " +
                                 
                                  "";
            await EmailService.SendEmailAsync(
                ConfigurationManager.AppSettings["SystemEmail"],
                ConfigurationManager.AppSettings["SystemEmailPassword"],
                ConfigurationManager.AppSettings["SystemEmailSmtp"],
                ConfigurationManager.AppSettings["SystemEmailSmtpPort"],
                user.Email, user.UserName, content);
            return View();
        }
        [HttpPost]
        public ActionResult SubmitOrder()
        {
            return View("");
        }
    }
}