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
    
    public partial class Stop
    {
        public Stop()
        {
            this.StopPassengers = new HashSet<StopPassenger>();
        }
    
        public int StopId { get; set; }
        public Nullable<int> OutingId { get; set; }
        public Nullable<short> RankSeq { get; set; }
        public string Description { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public short StopStatusId { get; set; }
    
        public virtual Outing Outing { get; set; }
        public virtual ICollection<StopPassenger> StopPassengers { get; set; }
        public virtual StopStatus StopStatus { get; set; }
    }
}