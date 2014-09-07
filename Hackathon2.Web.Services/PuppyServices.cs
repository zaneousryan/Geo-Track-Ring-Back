using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hackathon2.Services.Models;
using System.Net.Http;

namespace Hackathon2.Web.Services
{
    public class PuppyServices
    {

        HttpClient GetClient()
        {
            HttpClient client = new HttpClient();


            return client;
        }


        public async Task<T> Get<T>(string method, Dictionary<string, string> queryStrings)
        {

            StringBuilder builder = new StringBuilder(
                String.Format(@"https://hackathon2.azurewebsites.net/Services/{0}", method));


            StringBuilder queryString = new StringBuilder("cache=");
            queryString.Append(Guid.NewGuid().ToString());

            foreach(var item in queryStrings)
            {
                if (String.IsNullOrWhiteSpace(item.Value))
                    continue;

                if(queryString.Length > 0)
                    queryString.Append("&");

                queryString.Append(String.Format("{0}={1}", item.Key, Uri.EscapeUriString(item.Value ?? "")));
            }

            if(queryString.Length > 0)
            {
                builder.Append("?");
                builder.Append(queryString);
            }


            try
            {
                using (HttpClient client = GetClient())
                {
                    Uri url = new Uri(builder.ToString());

                    var response = await client.GetAsync(url);

                    var result = await response.Content.ReadAsStringAsync();
                    var resultData = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(result);

                    return resultData;
                }
            }
            catch
            {
                throw;
            }

        }

        public class GetTextResponse
        {
            public bool Success { get; set; }
            public string Text { get; set; }
        }




        public async Task<List<StopMobile>> GetPossibleStopsForPassenger(int passengerId, string filter)
        {

            Dictionary<string, string> query = new Dictionary<string,string>();
            query.Add("passengerId", passengerId.ToString());
            query.Add("filter", filter);

            var returnValue = await Get<List<StopMobile>>("GetPossibleStopsForPassenger", query);

            return returnValue;
        }



        public class CreateOutingResponse
        {
            public bool Success { get; set; }
            public int OutingId { get; set; }
        }

        public async Task<int> CreateOuting(int passengerId)
        {
            Dictionary<string, string> query = new Dictionary<string, string>();
            query.Add("passengerId", passengerId.ToString());

            var returnValue = await Get<CreateOutingResponse>("CreateOuting", query);

            return returnValue.OutingId;
        }


        class AddStopResponse
        {
            public bool Success { get; set; }
            public int StopId { get; set; }
        }

        public async Task<int> AddStop(int passengerId, string description, decimal latitude, decimal longitude)
        {
            
            Dictionary<string, string> query = new Dictionary<string, string>();
            query.Add("passengerId", passengerId.ToString());
            query.Add("description", description.ToString());
            query.Add("latitude", latitude.ToString());
            query.Add("longitude", longitude.ToString());

            var returnValue = await Get<AddStopResponse>("AddStop", query);


            
            return returnValue.StopId;
        }

        
        public async Task<string> GetText(int passengerId, short textId)
        {

            Dictionary<string, string> query = new Dictionary<string, string>();
            query.Add("passengerId", passengerId.ToString());
            query.Add("textId", textId.ToString());

            var returnValue = await Get<GetTextResponse>("GetText", query);

            return returnValue.Text;
        }
        public async Task<List<StopMobile>> GetStopsForPassenger(int passengerId)
        {

            Dictionary<string, string> query = new Dictionary<string, string>();
            query.Add("passengerId", passengerId.ToString());

            var returnValue = await Get<List<StopMobile>>("GetStopsForPassenger", query);

            return returnValue;
        }

        class AuthenticateResponse
        {
            public int PassengerId { get; set; }
        }

        public async Task<int> Authenticate(string username)
        {
            Dictionary<string, string> query = new Dictionary<string, string>();
            query.Add("username", username);

            var returnValue = await Get<AuthenticateResponse>("Authenticate", query);

            return returnValue.PassengerId;
        }

        class RockThePuppyResponse
        {
             public bool Success { get; set; }
        }

        public async Task<bool> RockThePuppy(int passengerId, bool complete)
        {
            Dictionary<string, string> query = new Dictionary<string, string>();
            query.Add("passengerId", passengerId.ToString());
            query.Add("complete", complete.ToString());

            var returnValue = await Get<RockThePuppyResponse>("RockThePuppy", query);

            return returnValue.Success;
        }
    }
}
