using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon2.Services.Models
{
    public class OutingMobile
    {
        private ObservableCollection<StopMobile> _stopsMobile;

        //public OutingMobile(string outingId, string startDate, string endDate, string companyId, 
        //    string driverId, string currentLatitude, string currentLongitude)
        //{
        //    OutingId = Int32.Parse(outingId);
        //}
        //public int OutingId { get; set; }
        //public DateTime StartDate { get; set; }
        //public DateTime EndDate { get; set; }
        //public int CompanyId { get; set; }
        //public int DriverId { get; set; }
        //public decimal CurrentLatitude { get; set; }
        //public decimal CurrentLongitude { get; set; }
        public ObservableCollection<StopMobile> Stops { get { return this._stopsMobile ?? (this._stopsMobile = new ObservableCollection<StopMobile>()); } }
    }
}
