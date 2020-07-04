using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARPrj.Model.Models.AirLine
{
    public class AirlinesTicketsTypeModel
    {
        public int Id { get; set; }
        public int? NumberOfSeats { get; set; }
        public int? AirlineId { get; set; }
        public int? TicketsTypeId { get; set; }
        public int? Price { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual AirlineModel Airline { get; set; }
        public virtual TicketsTypeModel TicketsType { get; set; }
    }
}
