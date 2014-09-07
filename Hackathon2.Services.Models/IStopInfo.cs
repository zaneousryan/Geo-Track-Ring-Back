using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon2.Infrastructure
{
    public interface IStopInfo : ILocationInfo
    {
        string Description { get; set; }
    }
}
