//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ARPrj.DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public Nullable<int> OrderId { get; set; }
        public Nullable<int> FlightId { get; set; }
        public int Count { get; set; }
        public Nullable<int> TicketsTypeId { get; set; }
        public string CustomerFullName { get; set; }
        public string PhoneNumber { get; set; }
    
        public virtual Flight Flight { get; set; }
        public virtual Order Order { get; set; }
        public virtual TicketsType TicketsType { get; set; }
    }
}
