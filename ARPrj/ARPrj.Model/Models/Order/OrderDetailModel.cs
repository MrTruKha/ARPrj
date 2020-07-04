using ARPrj.Model.Models.AirLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ARPrj.Model.Models.Order
{
    public class OrderDetailModel
    {
        public int OrderDetailId { get; set; }
        public int? OrderId { get; set; }
        public int? FlightId { get; set; }
        public int Count { get; set; }
        public int? TicketsTypeId { get; set; }

        public virtual Flight.FlightModel Flight { get; set; }
        public virtual OrderModel Order { get; set; }
        public virtual TicketsTypeModel TicketsType { get; set; }
    }
}
