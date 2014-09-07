using Hackathon2.Infrastructure;
using Hackathon2.TCS.ToolKit;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon2.Entity
{
    public partial class Logic
    {
        public async Task RockThePuppy(int passengerId, bool complete)
        {
            var outingId = GetCurrentOuting(passengerId);

            if (!outingId.HasValue)
                return;

            var currentLocation = this.GetCurrentOutingLocation(outingId.Value);
            
            var nextStop = this.GetNextStop(passengerId);

            if (currentLocation == null)
            {
                SetCurrentOutingLocation(
                    outingId.Value,
                    nextStop.Latitude,
                    nextStop.Longitude
                );
            }
            else if(nextStop.StopStatusId == StopStatus_Processing)
            {
                var passengerStops = GetStopsForPassenger(passengerId);
                var nextRankSeq = nextStop.RankSeq + 1;

                var nextNextStop = passengerStops.FirstOrDefault(x => x.RankSeq == nextRankSeq);

                if(nextNextStop != null)
                {
                    nextStop = nextNextStop;
                    var routeInfo = await new LocationService()
                                .GetRoute(currentLocation, nextStop);

                    var maneuverCounter = routeInfo.maneuvers.Count() / 2;
                    var maneuver = routeInfo.maneuvers.Skip(maneuverCounter).First();
                    var segmentCount = maneuver.segments.Count() / 4;
                    var segment = maneuver.segments.Skip(segmentCount).First();

                    SetCurrentOutingLocation(
                        outingId.Value,
                        Convert.ToDecimal(segment.lat),
                        Convert.ToDecimal(segment.lon)
                    );
                }
                    
            }
            else if(complete)
            {
                SetCurrentOutingLocation(
                    outingId.Value,
                    nextStop.Latitude,
                    nextStop.Longitude
                );
            }
            else
            {
                 var routeInfo = await new LocationService()
                                .GetRoute(currentLocation, nextStop);

                 var maneuverCounter = routeInfo.maneuvers.Count() / 2;
                 var maneuver = routeInfo.maneuvers.Skip(maneuverCounter).First();
                 var segmentCount = maneuver.segments.Count() / 4;
                 var segment = maneuver.segments.Skip(segmentCount).First();

                 SetCurrentOutingLocation(
                     outingId.Value,
                     Convert.ToDecimal(segment.lat),
                     Convert.ToDecimal(segment.lon)
                 );
            }

            await UpdateOutingState(passengerId);
        }

        public List<int> GetActiveOutingPassengers()
        {
            using (var c = new Hackathon2Entities())
            {
                var p = c.Passengers.Where(x =>
                    x.CurrentOuting.CurrentLatitude.HasValue &&
                    x.CurrentOuting.Stops.Any(y => y.StopStatusId != StopStatus_Closed))
                    .Select(x => x.PassengerId).Distinct().ToList();

                return p;
            }
        }

        public async Task UpdateActiveOutingPassengers()
        {
            foreach (var item in GetActiveOutingPassengers())
            {
                await UpdateOutingState(item);
            }
        }



        public string UpdateOutingText(
            ILocationInfo origin,
            Stop destination,
            RouteResponse routeInfo
            )
        {
            String returnValue =
                String.Format(@"Ring. Ring. Hello and thank you for calling GEO Track Ringback. Your important vehicle is currently {0} minutes from {1}. They are experiencing {2} Traffic.
                ",
                 Math.Round(routeInfo.RouteTimeInSeconds / 60.0, 2),
                 destination.Description,
                 routeInfo.CurrentGauge
                 );

            
            return returnValue;
        }


        public async Task UpdateOutingState(int passengerId)
        {
            try
            {
                var outingId = GetCurrentOuting(passengerId);

                if (!outingId.HasValue)
                    return;

                var currentLocation = this.GetCurrentOutingLocation(outingId.Value);

                if (currentLocation == null)
                    return;

                var locationLastUpdated = this.GetCurrentOutingLocationLastUpdated(outingId.Value);

                if (!locationLastUpdated.HasValue)
                    return;

                var textLastUpdated = this.GetTextLastUpdated(outingId.Value, 1) ?? DateTime.MinValue;

                var nextStop = this.GetNextStop(passengerId);

                if (nextStop == null)
                    return;

                
                var routeInfo = await new LocationService()
                    .GetRoute(currentLocation, nextStop);



               
                if(nextStop.StopStatusId == StopStatus_Open)
                {
                    if (routeInfo.RouteTimeInSeconds < 20)
                    {
                        var getAllStops = this.GetStopsForPassenger(passengerId).OrderByDescending(x=> x.RankSeq)
                            .FirstOrDefault();


                        if(getAllStops.StopId == nextStop.StopId)
                        {
                            UpdateStopStatus(
                                outingId.Value,
                                nextStop.StopId,
                                StopStatus_Closed);
                            nextStop = null;
                        }
                        else
                        {
                            UpdateStopStatus(
                                outingId.Value,
                                nextStop.StopId,
                                StopStatus_Processing);
                        }


                        
                    }
                }
                else if (nextStop.StopStatusId == StopStatus_Processing)
                {
                    if (routeInfo.RouteTimeInSeconds > 20)
                    {
                        UpdateStopStatus(
                            outingId.Value,
                            nextStop.StopId,
                            StopStatus_Closed);

                        nextStop = this.GetNextStop(passengerId);
                        if(nextStop != null)
                        {
                            routeInfo = await new LocationService()
                                .GetRoute(currentLocation, nextStop);
                        }
                    }
                }

                string newStringText = null;

                if (nextStop != null)
                {
                    newStringText = UpdateOutingText(
                        currentLocation, nextStop, routeInfo);
                }
                else
                {
                    newStringText = "Ring. Ring. Hello and thank you for calling GEO Track Ringback. The vehicle has completed it's journey";
                }

                Task.Run(() =>
                    {
                        this.UploadRingTone(newStringText);
                    });

                UpdateText(passengerId, 1, newStringText);
            }
            catch
            {
                return;
            }
        }

        public bool TurnOnRBT(string phoneNumber, string authKey)
        {
            var client = new RestClient("http://api.us.listen.com/Listen-api/restapi/openapi/turnOnRbt");
            var request = new RestRequest(Method.GET);
            request.AddParameter("phonenumber", phoneNumber); // adds to POST or URL querystring based on Method
            request.AddParameter("authkey", authKey);

            RestResponse response = (RestResponse)client.Execute(request);
            var content = response.Content; // raw content as string
          //  using (var fileStream = File.Create("output2.html"))
            //{
            //    byte[] byteArray = Encoding.ASCII.GetBytes(content);
            //    using (MemoryStream stream = new MemoryStream(byteArray))
            //    {
            //        stream.CopyTo(fileStream);
            //    }
            //}
           
            return true;
        }


        public bool UploadRingTone(string messageText)
        {
            string filePath = "DriverMessage.mp3";
            string phoneNumber = "2062184737";
            string authKey = "H743541268";
            TurnOnRBT(phoneNumber, authKey);

            //http://api.us.listen.com/Listen-api/restapi/openapi/uploadRbt?phonenumber=4252236137&authkey=789ee4db-ad76-435b-85c9-c9eb751af6a8&offset=
            var client = new RestClient("http://api.us.listen.com/Listen-api/restapi/openapi/uploadRbt");
            // client.Authenticator = new HttpBasicAuthenticator(username, password);

            var request = new RestRequest(Method.POST);
            request.AddParameter("phonenumber", phoneNumber); // adds to POST or URL querystring based on Method
            request.AddParameter("authkey", authKey);
            request.AddParameter("offset", "0");
            // easily add HTTP Headers
            //request.AddHeader("header", "value");

            // add files to upload (works with compatible verbs)

            //request.AddFile("file", FileToByteArray(filePath), filePath, "audio/mp3");
            request.AddFile("file", GetVoiceFromTextATTAPI(messageText), filePath, "audio/mp3");

            // execute the request
            RestResponse response = (RestResponse)client.Execute(request);
            var content = response.Content; // raw content as string
            //using (var fileStream = File.Create("output.html"))
            //{
            //    byte[] byteArray = Encoding.ASCII.GetBytes(content);
            //    MemoryStream stream = new MemoryStream(byteArray);
            //    stream.CopyTo(fileStream);
            //}
            ////if (content.ToString().Contains("error"))
            //{
            //    System.Diagnostics.Process.Start("output.html");
            //}

            // or automatically deserialize result
            // return content type is sniffed but can be explicitly set via RestClient.AddHandler();
            //RestResponse<Person> response2 = client.Execute<Person>(request);
            //var name = response2.Data.Name;

            //// easy async support
            //client.ExecuteAsync(request, response =>
            //{
            //    Console.WriteLine(response.Content);
            //});

            //// async with deserialization
            //var asyncHandle = client.ExecuteAsync<Person>(request, response =>
            //{
            //    Console.WriteLine(response.Data.Name);
            //});

            //// abort the request on demand
            //asyncHandle.Abort();

            return true;
        }


        public byte[] GetVoiceFromTextATTAPI(string text)
        {
            byte[] receivedBytes = null;
            var parEndPoint = "https://api.att.com/speech/v3/textToSpeech";
            var parURI = "/speech/v3/textToSpeech";
            var parAccessToken = "r7R3Hy5SnUQHJbdfxNSOJa6xzT1xqSOB";
            var parXarg = "ClientApp=C#ClientApp,ClientVersion=2_2,ClientScreen=Browser,ClientSdk=C#Restful,DeviceType=WebServer,DeviceOs=Windows";
            var parContentType = "text/plain";
            var parContent = text;// "AT&T Text To Speech Sample";

            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(string.Empty + parEndPoint + parURI);
            httpRequest.Headers.Add("Authorization", "Bearer " + parAccessToken);
            httpRequest.Headers.Add("X-SpeechContext", parXarg);
            httpRequest.ContentLength = parContent.Length;
            httpRequest.ContentType = parContentType;
            httpRequest.Accept = "audio/x-wav";
            httpRequest.Method = "POST";
            httpRequest.KeepAlive = true;

            UTF8Encoding encoding = new UTF8Encoding();
            byte[] postBytes = encoding.GetBytes(parContent);
            httpRequest.ContentLength = postBytes.Length;

            using (Stream writeStream = httpRequest.GetRequestStream())
            {
                writeStream.Write(postBytes, 0, postBytes.Length);
                writeStream.Close();
            }
            HttpWebResponse speechResponse = (HttpWebResponse)httpRequest.GetResponse();
            int offset = 0;
            int remaining = Convert.ToInt32(speechResponse.ContentLength);
            using (var stream = speechResponse.GetResponseStream())
            {
                receivedBytes = new byte[speechResponse.ContentLength];
                while (remaining > 0)
                {
                    int read = stream.Read(receivedBytes, offset, remaining);
                    if (read <= 0)
                    {
                        throw new Exception(String.Format("End of stream reached with {0} bytes left to read", remaining));
                        //return;
                    }

                    remaining -= read;
                    offset += read;
                }
                //ByteArrayToFile("c:\\users\\ryan_000\\Desktop\\textToSpeech.mp3", receivedBytes);
                //audioPlay.Attributes.Add("src", "data:audio/wav;base64," + Convert.ToBase64String(receivedBytes, Base64FormattingOptions.None));
                // TTSSuccessMessage = "Success";
            }
            return receivedBytes;
        }
        //catch (WebException we)
        //{
        //    string errorResponse = string.Empty;
        //    try
        //    {
        //        using (StreamReader sr2 = new StreamReader(we.Response.GetResponseStream()))
        //        {
        //            errorResponse = sr2.ReadToEnd();
        //            sr2.Close();
        //        }
        //        TTSErrorMessage = errorResponse;
        //    }
        //    catch
        //    {
        //        errorResponse = "Unable to get response";
        //        TTSErrorMessage = errorResponse;
        //    }
        //}
        //catch (Exception ex)
        //{
        //    TTSErrorMessage = ex.Message;
        //    return;
        //}
        //}
    }
}
