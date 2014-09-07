using Hackathon2.Infrastructure;
using Hackathon2.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon2.TCS.ToolKit
{
    public class LocationService
    {
        const string baseURL = @"https://cs6-nbwa.navbuilder.nimlbs.net/lws/api/v2/{0}/json?apikey=RwKEdt2mXQLNz58W7cuVJDzLxP5JVcnkSwnLf9rz&pid=411164";



        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("MOZILLA", "5.0"));
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("(WINDOWS NT 6.1; WOW64)"));
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("APPLEWEBKIT", "537.1"));
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("(KHTML, LIKE GECKO)"));
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("CHROME", "21.0.1180.75"));
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("SAFARI", "537.1"));

            return client;

        }

        public async Task<DirectionsResponse> GetDirections(ILocationInfo origin, ILocationInfo destination)
        {
            DirectionsResponse returnValue = null;

            try
            {
                string urlRequest =
                    String.Format("{2}&optimize=fastest&vehicle=car&avoid=hov&origin={0}&destination={1}",
                        Uri.EscapeUriString(String.Format("{0},{1}", origin.Latitude, origin.Longitude)),
                        Uri.EscapeUriString(String.Format("{0},{1}", destination.Latitude, destination.Longitude)),
                        String.Format(baseURL, "directions"));


                using (HttpClient client = GetClient())
                {
                    var responseContent = await client.GetStringAsync(urlRequest);
                    returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<DirectionsResponse>(responseContent);

                }

            }
            catch
            {
                throw;
            }

            return returnValue;
        }


        public async Task<RouteResponse> GetRoute(ILocationInfo origin, ILocationInfo destination)
        {
            RouteResponse returnValue = null;

            try
            {
                string urlRequest =
                    String.Format("{2}&optimize=fastest&vehicle=car&avoid=hov&origin={0}&destination={1}",
                        Uri.EscapeUriString(String.Format("{0},{1}", origin.Latitude, origin.Longitude)),
                        Uri.EscapeUriString(String.Format("{0},{1}", destination.Latitude, destination.Longitude)),
                        String.Format(baseURL, "route"));


                using (HttpClient client = GetClient())
                {
                    var responseContent = await client.GetStringAsync(urlRequest);
                    returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<RouteResponse>(responseContent);

                }

            }
            catch
            {
                throw;
            }


            var maneuvers = returnValue.maneuvers;
            var trItems = returnValue.traffic_flows.SelectMany(x=> x.traffic_flow_items);

            foreach(var m in maneuvers)
            {
                foreach(var tr in m.traffic_regions)
                {
                    var tf = trItems.FirstOrDefault(x => x.location == tr.location);

                    if (tf != null)
                    {
                        tr.ActualSpeed = tf.speed;
                        tr.CurrentTrafficGauge = tf.CurrentTrafficGauge;
                    }
                    else
                    {
                        tr.ActualSpeed = m.speed;
                        tr.CurrentTrafficGauge = TrafficGauge.Good;
                    }


                    
                }
            }


            return returnValue;
        }


        public class GetMapResponse
        {
            public byte[] image { get; set; }
        }

        public async Task<byte[]> GetMap(ILocationInfo origin) 
        {
            try
            {
                string urlRequest =
                    String.Format("{1}?center={0}&scale=1&width=256&height=256&ppi=318&apikey=RwKEdt2mXQLNz58W7cuVJDzLxP5JVcnkSwnLf9rz&pid=411164",
                        Uri.EscapeUriString(String.Format("{0},{1}", origin.Latitude, origin.Longitude)),
                        String.Format(@"https://cs6-nbwa.navbuilder.nimlbs.net/lws/api/v2/map/png"));


                using (HttpClient client = GetClient())
                {
                    var responseContent = await client.GetStringAsync(urlRequest);
                    var returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<GetMapResponse>(responseContent);
                    return returnValue.image;

                }

            }
            catch
            {
                throw;
            }
        }
    }


    


    public class Coordinate
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    }

    public class Destination
    {
        public string airport { get; set; }
        public string areaname { get; set; }
        public string city { get; set; }
        public Coordinate coordinate { get; set; }
        public string country_code { get; set; }
        public string country { get; set; }
        public string formatted_address { get; set; }
        public string intersection { get; set; }
        public string postal { get; set; }
        public string house_number { get; set; }
        public string state { get; set; }
        public string street { get; set; }
        public string type { get; set; }
    }

    public class Coordinate2
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    }

    public class Origin
    {
        public string airport { get; set; }
        public string areaname { get; set; }
        public string city { get; set; }
        public Coordinate2 coordinate { get; set; }
        public string country_code { get; set; }
        public string country { get; set; }
        public string formatted_address { get; set; }
        public string intersection { get; set; }
        public string postal { get; set; }
        public string house_number { get; set; }
        public string state { get; set; }
        public string street { get; set; }
        public string type { get; set; }
    }

    public class BottomRightCoordinate
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    }

    public class TopLeftCoordinate
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    }

    public class RouteExtents
    {
        public BottomRightCoordinate bottom_right_coordinate { get; set; }
        public TopLeftCoordinate top_left_coordinate { get; set; }
    }

    public class Bounds
    {
        public double minLon { get; set; }
        public double minLat { get; set; }
        public double maxLon { get; set; }
        public double maxLat { get; set; }
    }

    public class Coordinate3
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    }

    public class CurrentRoadInfo
    {
        public string secondary_name { get; set; }
        public string primary_name { get; set; }
    }

    public class Segment
    {
        public double lon { get; set; }
        public double len { get; set; }
        public double lat { get; set; }
        public double heading { get; set; }
    }

    public class CountryInfo
    {
        public string driving_side { get; set; }
        public string code { get; set; }
    }

    public class TurnRoadInfo
    {
        public string secondary_name { get; set; }
        public string primary_name { get; set; }
        public CountryInfo country_info { get; set; }
    }

    public class Maneuver
    {
        public double totalLen { get; set; }
        public Bounds bounds { get; set; }
        public string command { get; set; }
        public Coordinate3 coordinate { get; set; }
        public double distance { get; set; }
        public double heading { get; set; }
        public double max_instruction_distance { get; set; }
        public double speed { get; set; }
        public bool is_stack_advise { get; set; }
        public CurrentRoadInfo current_road_info { get; set; }
        public List<Segment> segments { get; set; }
        public List<TrafficRegion> traffic_regions { get; set; }
        public TurnRoadInfo turn_road_info { get; set; }
    }


    public class TrafficRegion
    {
        public double start { get; set; }
        public string location { get; set; }
        public double length { get; set; }

        public double ActualSpeed { get; set; }

        public double TimeInSecondsToLeaveRegion
        {
            get
            {
                return length / (ActualSpeed <= 0 ? 1 : ActualSpeed);
            }
        }

        public TrafficGauge CurrentTrafficGauge { get; set; }
    }


    public class TrafficFlowItem
    {
        public double speed { get; set; }
        public string location { get; set; }
        public double free_flow_speed { get; set; }
        public string color { get; set; }

        public TrafficGauge CurrentTrafficGauge
        {
            get
            {
                TrafficGaugeColors colorEnum = TrafficGaugeColors.G;
                if (Enum.TryParse<TrafficGaugeColors>(color, out colorEnum))
                {

                }

                return (TrafficGauge)((int)colorEnum);
            }
        }
    }


    public class TrafficFlow
    {
        public string type { get; set; }
        public string age { get; set; }
        public List<TrafficFlowItem> traffic_flow_items { get; set; }
    }


    public class DirectionsResponse
    {
        public Destination destination { get; set; }
        public Origin origin { get; set; }
        public RouteExtents route_extents { get; set; }
        public string route_id { get; set; }
        public List<Maneuver> maneuvers { get; set; }
    }


    public class RouteResponse
    {
        public string session_id { get; set; }
        public Destination destination { get; set; }
        public Origin origin { get; set; }
        public RouteExtents route_extents { get; set; }
        public string route_id { get; set; }
        public List<Maneuver> maneuvers { get; set; }
        public List<TrafficFlow> traffic_flows { get; set; }

        public TrafficGauge CurrentGauge
        {
            get
            {
                var allGauges = maneuvers.SelectMany(x => x.traffic_regions)
                    .Select(x => x.CurrentTrafficGauge);

                if (allGauges.Contains(TrafficGauge.Bad))
                    return TrafficGauge.Bad;
                else if(allGauges.Contains(TrafficGauge.Okay))
                    return TrafficGauge.Okay;
                else
                    return TrafficGauge.Good;
            }
        } 

        public double RouteTimeInSeconds
        {
            get
            {
                return maneuvers.SelectMany(x => x.traffic_regions)
                    .Sum(x => x.TimeInSecondsToLeaveRegion);
            }
        }
    }
}
