using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ARPrj.DataAccess;
using ARPrj.Model.Models.Flight;

namespace ARPrj.WebManagement.Models
{
    public class SearchResultViewModel
    {
        public SearchResultViewModel()
        {
            Flights = new List<Flight>();
        }
        public int From { get; set; }
        public int To { get; set; }
        public int Amount { get; set; }
        public DateTime DepartureDate { get; set; }
        public List<Flight> Flights { get; set; }

    }
}