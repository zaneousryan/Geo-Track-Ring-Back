using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hackathon2.Infrastructure;

namespace Hackathon2.Entity
{
    partial class Outing : ILocationInfo
    {
        decimal ILocationInfo.Latitude
        {
            get
            {
                return CurrentLatitude.Value;
            }
            set
            {
                CurrentLatitude = value;
            }
        }
        decimal ILocationInfo.Longitude
        {
            get
            {
                return CurrentLongitude.Value;
            }
            set
            {
                CurrentLongitude = value;
            }
        }
    }
}
