using Hackathon2.Entity;
using Hackathon2.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using System.Configuration;
using Hackathon2.TCS.ToolKit;

namespace Hackathon2.Web.Controllers
{
    [Authorize]
    public class ServicesController : Controller
    {

        [HttpGet]
        [AllowAnonymous]
        public async Task<JsonResult> RockThePuppy(int passengerId, bool complete)
        {
            var logic = new Logic();
            await logic.RockThePuppy(passengerId, complete);

            return this.Json(new { Success = true }, JsonRequestBehavior.AllowGet); 
        }

        [HttpGet]
        public JsonResult GetPreferences()
        {
            var logic = new Logic();
            var preferences = logic.GetPreferences();

            IList<PreferenceMobile> mobilePreferences = new List<PreferenceMobile>();

            foreach (var preference in preferences)
            {
                mobilePreferences.Add(new PreferenceMobile() { PreferenceId = preference.PreferenceId, Description = preference.Description });
            }

            return this.Json(mobilePreferences, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetPassengerPreferences(int id)
        {
            var logic = new Logic();
            var userPreferences = logic.GetPassengerPreferences(id);

            IList<PassengerPreferenceMobile> mobilePassengerPreferences = new List<PassengerPreferenceMobile>();

            foreach (var preference in userPreferences)
            {
                mobilePassengerPreferences.Add(new PassengerPreferenceMobile() { PreferenceId = preference.PreferenceId, PreferenceDescription = preference.Preference.Description, PreferenceValue = preference.Value });
            }

            //mobilePassengerPreferences.Add(new PassengerPreferenceMobile() { PreferenceId = 1, PreferenceDescription = "description 1", PreferenceValue = "value 1" });
            //mobilePassengerPreferences.Add(new PassengerPreferenceMobile() { PreferenceId = 2, PreferenceDescription = "description 2", PreferenceValue = "value 2" });
            //mobilePassengerPreferences.Add(new PassengerPreferenceMobile() { PreferenceId = 3, PreferenceDescription = "description 3", PreferenceValue =  "value 3"});

            return this.Json(mobilePassengerPreferences, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult GetText(int passengerId, short textId)
        {
            var logic = new Logic();
            var text = logic.GetText(passengerId, textId);

            return this.Json(new { Success = true, Text = text }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult SetPassengerPreference(int passengerId, int preferenceId, string preferenceValue)
        {
            var logic = new Logic();
            
            try 
            {
                logic.UpdatePassengerPreference(passengerId, preferenceId, preferenceValue);
                return this.Json(new { Success = true }, JsonRequestBehavior.AllowGet);
            }
            catch  (Exception exc)
            {
                return this.Json(new { Success = false, Message = exc.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult GetPassengerId(Guid userId)
        {
            var logic = new Logic();

            try
            {
                var r = logic.GetPassengerId(userId);
                return this.Json(new { Success = true, PassengerId = r }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return this.Json(new { Success = false, Message = exc.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult Authenticate(string username)
        {
            try
            {
                string userId = null;
                using (System.Data.SqlClient.SqlConnection sqlConnection1 = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                    System.Data.SqlClient.SqlDataReader reader;

                    cmd.CommandText = "select * from aspnetusers where email = @email";
                    cmd.CommandType = System.Data.CommandType.Text;


                    cmd.Parameters.Add("@email", System.Data.SqlDbType.NVarChar);
                    cmd.Parameters["@email"].Value = username;

                    //cmd.Parameters.Add("@email", System.Data.SqlDbType.VarChar).Value = username;
                    cmd.Connection = sqlConnection1;



                    sqlConnection1.Open();

                    reader = cmd.ExecuteReader();
                    // Data is accessible through the DataReader object here.
                    if (reader.Read())
                    {
                        userId = reader.GetString(0);
                    }

                    sqlConnection1.Close();
                }

                var passengerId = new Logic().GetPassengerId(new Guid(userId));

                return this.Json(new { PassengerId = passengerId }, JsonRequestBehavior.AllowGet);
            }

            catch
            {
                return this.Json(new { Success = false }, JsonRequestBehavior.AllowGet);
            }

            //var user = await UserManager.FindAsync(username, password);
            //if (user != null)
            //{
            //    var passengerId =new Logic().GetPassengerId(new Guid(user.Id));

            //    return this.Json(new { PassengerId = passengerId }, JsonRequestBehavior.AllowGet);
                
            //}
            //else
            //{
            //    return this.Json(new { Success = false }, JsonRequestBehavior.AllowGet);
            //}
        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult GetNextStop(int passengerId)
        {
            var logic = new Logic();

            try
            {
                var r = logic.GetNextStop(passengerId);
                var r1 = new StopMobile()
                {
                    Description = r.Description,
                    Latitude = r.Latitude,
                    Longitude = r.Longitude,
                    StopStatusId = r.StopStatusId,
                    StopId = r.StopId
                };

                return this.Json(r1, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return this.Json(new { Success = false, Message = exc.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult GetStopsForPassenger(int passengerId)
        {
            var logic = new Logic();

            try
            {
                var r = logic.GetStopsForPassenger(passengerId);
                var r1 = r.Select(x => new StopMobile()
                {
                    Description = x.Description,
                    Latitude = x.Latitude,
                    Longitude = x.Longitude,
                    StopStatusId = x.StopStatusId,
                    StopId = x.StopId,
                    RankSeq = x.RankSeq
                });

                return this.Json(r1, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return this.Json(new { Success = false, Message = exc.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        class TheLocationInfo : Hackathon2.Infrastructure.ILocationInfo
        {

            public decimal Longitude
            {
                get;
                set;
            }

            public decimal Latitude
            {
                get;
                set;
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetMap(int passengerId)
        {
            var logic = new Logic();

            var currentOuting = logic.GetCurrentOuting(passengerId);

            if(currentOuting.HasValue)
            {
                var location = logic.GetCurrentOutingLocation(currentOuting.Value);
                if(location != null)
                {
                    var imageData = await new LocationService()
                    .GetMap(new TheLocationInfo()
                    {
                        Latitude = Convert.ToDecimal(location.Latitude),
                        Longitude = Convert.ToDecimal(location.Longitude)
                    });

                    return File(imageData, "image/png");
                }
            }

            return File(new Byte[0], "images/png");
            

        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult GetPossibleStopsForPassenger(int passengerId, string filter)
        {
            var logic = new Logic();

            try
            {
                var r = logic.GetPossibleStopsForPassenger(passengerId, filter);
                var r1 = r.Select(x => new StopMobile()
                {
                    Description = x.Description,
                    Latitude = x.Latitude,
                    Longitude = x.Longitude
                });

                return this.Json(r1, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return this.Json(new { Success = false, Message = exc.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult SetCurrentOutingLocation(int outingId, decimal Latitude, decimal Longitude)
        {
            var logic = new Logic();

            try
            {
                logic.SetCurrentOutingLocation(outingId, Latitude, Longitude);
                return this.Json(new { Success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return this.Json(new { Success = false, Message = exc.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult GetCurrentOutingLocation(int outingId)
        {
            var logic = new Logic();

            try
            {
                var l = logic.GetCurrentOutingLocation(outingId);
                var r = new LocationMobile()
                {
                    Latitude = l.Latitude,
                    Longitude = l.Longitude
                };
                return this.Json(r, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return this.Json(new { Success = false, Message = exc.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult GetPossibleLocations(int passengerId, string term)
        {

            var logic = new Logic();
            var mobileStops = new List<StopMobile>();
          
            var possibleStops = logic.GetPossibleStopsForPassenger(passengerId, term);

           foreach(var possibleStop in possibleStops)
           {
               mobileStops.Add(new StopMobile() { Description = possibleStop.Description, Latitude = possibleStop.Latitude, Longitude = possibleStop.Longitude });
           }

           return this.Json(mobileStops, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult CreateOuting(int passengerId)
        {
            var logic = new Logic();

            try
            {
                var outingId = logic.CreateOuting(passengerId);
                return this.Json(new { Success = true, OutingId = outingId }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return this.Json(new { Success = false, Message = exc.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult AddStop(int passengerId, string description, decimal latitude, decimal longitude)
        {
            var logic = new Logic();

            try
            {
                var stopId = logic.AddStop(passengerId, description, latitude, longitude);
                return this.Json(new { Success = true, StopId = stopId }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return this.Json(new { Success = false, Message = exc.Message }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}