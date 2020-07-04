using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ARPrj.Model.Models.Flight
{
    public class FlightModel
    {
        public int FlightId { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public TimeSpan? DepartureTime { get; set; }
        public TimeSpan? ArrivalTime { get; set; }
        public int? SeatsLeft { get; set; }
        public int? InformationFlightID { get; set; }
        public int? AirlineId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
