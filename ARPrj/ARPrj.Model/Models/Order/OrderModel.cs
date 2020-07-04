using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARPrj.Model.Models.Order
{
    public class OrderModel
    {
        public int OrderId { get; set; }
        public int? CustomerId { get; set; }
        public decimal? TotalPrice { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

    }
}
