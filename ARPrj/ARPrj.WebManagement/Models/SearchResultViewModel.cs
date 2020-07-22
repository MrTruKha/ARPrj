using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ARPrj.DataAccess;

namespace ARPrj.WebManagement.Models
{
    public class SearchResultViewModel
    {
        public Airport From { get; set; }
        public Airport To { get; set; }
        public int Amount { get; set; }
        public DateTime DepartureDate { get; set; }

    }
}