//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Hackathon2.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Outing
    {
        public Outing()
        {
            this.Stops = new HashSet<Stop>();
            this.Passengers = new HashSet<Passenger>();
        }
    
        public int OutingId { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public Nullable<int> DriverId { get; set; }
        public Nullable<decimal> CurrentLatitude { get; set; }
        public Nullable<decimal> CurrentLongitude { get; set; }
        public Nullable<System.DateTime> CurrentLocationLastUpdated { get; set; }
        public string Text1 { get; set; }
        public string Text2 { get; set; }
        public string Text3 { get; set; }
        public Nullable<System.DateTime> Text1LastUpdated { get; set; }
        public Nullable<System.DateTime> Text2LastUpdated { get; set; }
        public Nullable<System.DateTime> Text3LastUpdated { get; set; }
    
        public virtual Company Company { get; set; }
        public virtual Driver Driver { get; set; }
        public virtual ICollection<Stop> Stops { get; set; }
        public virtual ICollection<Passenger> Passengers { get; set; }
    }
}
