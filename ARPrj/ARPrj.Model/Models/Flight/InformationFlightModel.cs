using ARPrj.Model.Models.AirLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARPrj.Model.Models.Flight
{
    public class InformationFlightModel
    {
        public int InformationFlightID { get; set; }
        public Nullable<int> TicketsTypeId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public virtual TicketsTypeModel TicketsType { get; set; }
    }
}
