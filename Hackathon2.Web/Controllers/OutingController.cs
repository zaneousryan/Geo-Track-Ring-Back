using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Owin;
using Hackathon2.Web.Models;
using Hackathon2.Entity;
using System.Web.Security;
using Hackathon2.Models;
using Newtonsoft.Json;

namespace Hackathon2.Web.Controllers
{
    public class OutingController : Controller
    {
        // GET: Outing
        public async Task<ActionResult> UpdateOutingState()
        {
            await new Logic().UpdateActiveOutingPassengers();
            return View();
        }
    }
}