using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hackathon2.Web.Models
{
    public class OutingModel
    {
        private IList<string> _stops;

        public int OutingId { get; set; }

        public IList<string> Stops
        {
            get { return this._stops ?? (this._stops = new List<string>()); }
            set { this._stops = value; }
        }
    }
}