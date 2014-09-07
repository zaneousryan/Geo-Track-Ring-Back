using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;

using Hackathon2.Web.Models;
using Hackathon2.Entity;

namespace Hackathon2.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = new HomeModel();
            model.CompanyId = int.Parse(System.Configuration.ConfigurationManager.AppSettings["CompanyId"].ToString());
            model.CompanyName = new Logic().GetCompanyName(model.CompanyId);
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}