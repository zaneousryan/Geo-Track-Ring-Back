using Hackathon2.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon2.Services.Models
{
    public class LocationMobile : ILocationInfo
    {

        public Decimal Longitude { get; set; }
        public Decimal Latitude { get; set; }
    }
}
