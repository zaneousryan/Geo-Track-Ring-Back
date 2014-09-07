using Hackathon2.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon2.Services.Models
{
    public class StopMobile : IStopInfo
    {
        public StopMobile()
        {


        }

        public string Description { get; set; }

        [Obsolete]
        public int StopId { get; set; }

         [Obsolete]
        public int OutingId { get; set; }

        public int StopStatusId { get; set; }
        public Decimal Longitude { get; set; }
        public Decimal Latitude { get; set; }

        public short? RankSeq { get; set; }
    }
}
