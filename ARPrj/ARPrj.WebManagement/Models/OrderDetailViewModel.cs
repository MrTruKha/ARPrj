using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ARPrj.WebManagement.Models
{
    public class OrderDetailViewModel
    {
        public  int OrderId { get;set }
        public int FlightId { get; set; }
        public int TicketTypeId { get; set; }
        public int Amount { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }

    }
}