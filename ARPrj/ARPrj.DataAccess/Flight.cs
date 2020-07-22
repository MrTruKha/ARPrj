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
    
    public partial class Flight
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Flight()
        {
            this.InformationFlights = new HashSet<InformationFlight>();
            this.OrderDetails = new HashSet<OrderDetail>();
        }
    
        public int FlightId { get; set; }
        public Nullable<System.TimeSpan> DepartureTime { get; set; }
        public Nullable<System.TimeSpan> ArrivalTime { get; set; }
        public Nullable<int> SeatsLeft { get; set; }
        public Nullable<int> AirlineId { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public Nullable<int> To { get; set; }
        public Nullable<int> From { get; set; }
        public Nullable<System.DateTime> DepartureDay { get; set; }
        public Nullable<System.DateTime> ArrivalDay { get; set; }
    
        public virtual Airline Airline { get; set; }
        public virtual Airport Airport { get; set; }
        public virtual Airport Airport1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InformationFlight> InformationFlights { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
