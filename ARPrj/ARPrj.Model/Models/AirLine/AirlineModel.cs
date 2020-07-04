using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARPrj.Model.Models.AirLine
{
    public class AirlineModel
    {        
        public AirlineModel()
        {
        }
        public int AirlineId { get; set; }
        public string AirlineName { get; set; }
        public decimal NetPrice { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
