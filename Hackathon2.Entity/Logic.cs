using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hackathon2.Infrastructure;

namespace Hackathon2.Entity
{
    public partial class Logic
    {
        public int GetPassengerId(Guid userId) 
        {
            using (var c = new Hackathon2Entities())
            {
                return c.Passengers.First(x => x.UserId == userId).PassengerId;
            }
        }

        public IList<PassengerPreference> GetPassengerPreferences(int passengerId)
        {
            using (var c = new Hackathon2Entities())
            {
                return c.PassengerPreferences.Where(x => x.PassengerId == passengerId).ToList();
            }
        }

        public IList<Preference> GetPreferences()
        {
            using (var c = new Hackathon2Entities())
            {
                return c.Preferences.ToList();
            }
        }

        public string GetCompanyName(int companyId)
        {
            using(var c = new Hackathon2Entities())
            {
                return c.Companies.First(x => x.CompanyId == companyId).Name;
            }
        }

        public const int Preference_MusicVolume = 1;
        public const int Comfort_Temperature = 2;
        public const int Preference_MusicGenre = 3;
        public const int Preference_Spotify = 4;

        public string GetPassengerPreference(int passengerId, int preferenceId)
        {
            using (var c = new Hackathon2Entities())
            {
                try
                {
                    return c.PassengerPreferences.First(x => x.PassengerId == passengerId && x.PreferenceId == preferenceId).Value;
                }
                catch
                {
                    return null;
                }
            }
        }

        public void UpdatePassengerPreference(int passengerId, int preferenceId, string value)
        {
            using (var c = new Hackathon2Entities())
            {
                var a = c.PassengerPreferences.FirstOrDefault(x => x.PassengerId == passengerId && x.PreferenceId == preferenceId);
                if (a == null)
                {
                    a = new PassengerPreference()
                    {
                        PassengerId = passengerId,
                        PreferenceId = preferenceId,
                        Value = value
                    };
                    c.PassengerPreferences.Add(a);
                }
                else
                {
                    a.Value = value;
                }
                c.SaveChanges();
            }
        }


        public int? GetPassengerPreferenceInt(int passengerId, int preferenceId)
        {
            try
            {
                return int.Parse(GetPassengerPreference(passengerId, preferenceId));
            }
            catch
            {
                return null;
            }


        }

        public short StopStatus_Open = 1;
        public short StopStatus_Processing = 2;
        public short StopStatus_Closed = 3;

        public short GetStopStatus(int outingId, int stopId)
        {
            using (var c = new Hackathon2Entities())
            {
                return c.Stops.First(x => x.OutingId == outingId && x.StopId == stopId).StopStatusId;
            }
        }

        public void UpdateStopStatus(int outingId, int stopId, short stopStatusId)
        {
            using (var c = new Hackathon2Entities())
            {
                var a = c.Stops.First(x => x.OutingId == outingId && x.StopId == stopId);
                if (a == null)
                {
                    throw new Exception();
                }
                else
                {
                    a.StopStatusId = stopStatusId;
                }
                c.SaveChanges();
            }

        }

        public IList<Stop> GetStopsForPassenger(int passengerId)
        {
            var outingId = GetCurrentOuting(passengerId);
            using (var c = new Hackathon2Entities())
            {
                try
                {
                    //old - var outingId = c.Outings.First(x => x.Stops.Any(r => r.StopPassengers.Any(p => p.PassengerId == passengerId))).OutingId;
                    return c.Stops.Where(x => x.OutingId == outingId).OrderBy(x => x.RankSeq).ToList();
                }
                catch
                {
                    return new List<Stop>();
                }
            }
        }

        private class Location : ILocationInfo
        {
            public decimal Latitude { get; set; }
            public decimal Longitude { get; set; }
        }

        //public ILocationInfo GetCurrentPassengerLocation(int passengerId)
        //{
        //    using (var c = new Hackathon2Entities())
        //    {
        //        var p = c.Passengers.First(x => x.PassengerId == passengerId);
        //        if (!p.CurrentLatitude.HasValue)
        //        {
        //            return null;
        //        }
        //        return new Location()
        //        {
        //            Latitude = p.CurrentLatitude.Value,
        //            Longitude = p.CurrentLongitude.Value
        //        };
        //    }
        //}

        //public void SetCurrentPassengerLocation(int passengerId, decimal Latitude, decimal Longitude)
        //{
        //    using (var c = new Hackathon2Entities())
        //    {
        //        var p = c.Passengers.First(x => x.PassengerId == passengerId);
        //        p.CurrentLatitude = Latitude;
        //        p.CurrentLongitude = Longitude;
        //        c.SaveChanges();
        //    }
        //}

        public ILocationInfo GetCurrentOutingLocation(int outingId)
        {
            using (var c = new Hackathon2Entities())
            {
                var p = c.Outings.First(x => x.OutingId == outingId);
                if (!p.CurrentLatitude.HasValue)
                {
                    return null;
                }
                return new Location()
                {
                    Latitude = p.CurrentLatitude.Value,
                    Longitude = p.CurrentLongitude.Value
                };
            }
        }

        public void SetCurrentOutingLocation(int outingId, decimal Latitude, decimal Longitude)
        {
            using (var c = new Hackathon2Entities())
            {
                var p = c.Outings.First(x => x.OutingId == outingId);
                p.CurrentLatitude = Latitude;
                p.CurrentLongitude = Longitude;
                p.CurrentLocationLastUpdated = DateTime.Now;
                c.SaveChanges();
            }
        }

        public DateTime? GetCurrentOutingLocationLastUpdated(int outingId)
        {
            using (var c = new Hackathon2Entities())
            {
                return c.Outings.First(x => x.OutingId == outingId).CurrentLocationLastUpdated.Value;
            }
        }

        public int CreateOuting(int passengerId)
        {
            using (var c = new Hackathon2Entities())
            {
                var x = new Outing()
                {
                    DriverId = 1,
                    CompanyId = 1,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddHours(1)
                };

                c.Outings.Add(x);
                c.SaveChanges();
                var p = c.Passengers.First(g => g.PassengerId == passengerId);
                

                p.CurrentOutingId = x.OutingId;

                c.SaveChanges();

                return x.OutingId;
            }
        }

        public int? GetCurrentOuting(int passengerId)
        {
            using (var c = new Hackathon2Entities())
            {
                return c.Passengers.First(x => x.PassengerId == passengerId).CurrentOutingId;
            }
        }

        public Outing GetCurrentOutingModel(int passengerId)
        {
            using (var c = new Hackathon2Entities())
            {
                var outingId = c.Passengers.First(x => x.PassengerId == passengerId).CurrentOutingId;

                return c.Outings.Where(x => x.OutingId == outingId).FirstOrDefault();
            }
        }

        public int AddStop(int passengerId, string description, decimal latitude, decimal longitude, short? rankSeq = null)
        {
            var outingId = GetCurrentOuting(passengerId);
            using (var c = new Hackathon2Entities())
            {
                if (!rankSeq.HasValue)
                {
                    if (c.Stops.Where(y => y.OutingId == outingId).Any())
                    {
                        rankSeq = (short)(c.Stops.Where(y => y.OutingId == outingId).Max(x => x.RankSeq) + 1);
                    }
                    else
                    {
                        rankSeq = 1;
                    }
                }

                var o = new Stop()
                {
                    OutingId = outingId,
                    Description = description,
                    Latitude = latitude,
                    Longitude = longitude,
                    StopStatusId = StopStatus_Open,
                    RankSeq = rankSeq
                };

                c.Stops.Add(o);

                c.SaveChanges();

                return o.StopId;
            }
        }

        private class StopInfo : IStopInfo
        {
            public int StopId { get; set; }
            public string Description { get; set; }
            public decimal Latitude { get; set; }
            public decimal Longitude { get; set; }

        }

        public Stop GetNextStop(int passengerId)
        {
            var outingId = GetCurrentOuting(passengerId);
            using (var c = new Hackathon2Entities())
            {
                return c.Stops.First(x => x.OutingId == outingId && x.StopStatusId != StopStatus_Closed);
            }
        }

        public IStopInfo GetPossibleStopInfoByDescription(string description)
        {
            var r = _PossibleLocations.FirstOrDefault(x => x.Item1.Equals(description, StringComparison.OrdinalIgnoreCase));
            return new StopInfo()
            {
                Description = r.Item1,
                Latitude = r.Item2,
                Longitude = r.Item3
            };
        }
 
        public IStopInfo[] GetPossibleStopsForPassenger(int passengerId, string filter)
        {
            using (var c = new Hackathon2Entities())
            {
                var currentStops = GetStopsForPassenger(passengerId).Select(x => x.Description).ToList();

                var r = PossibleLocationsSorted.Where(x => !currentStops.Contains(x.Item1)).Select(v =>
                    new StopInfo()
                    {
                        Description = v.Item1,
                        Latitude = v.Item2,
                        Longitude = v.Item3
                    });

                if (!string.IsNullOrWhiteSpace(filter))
                {
                    return r.Where(x => x.Description.StartsWith(filter, StringComparison.OrdinalIgnoreCase)).ToArray();
                }
                else
                {
                    return r.ToArray();
                }
            }

        }

        public void UpdateText(int passengerId, short textId, string text)
        {
            var outingId = GetCurrentOuting(passengerId);
            using (var c = new Hackathon2Entities())
            {
                var outing = c.Outings.First(x => x.OutingId == outingId);
                if (textId == 1)
                {
                    outing.Text1 = text;
                    outing.Text1LastUpdated = DateTime.Now;
                }
                else if (textId == 2)
                {
                    outing.Text2 = text;
                    outing.Text2LastUpdated = DateTime.Now;
                }
                else if (textId == 1)
                {
                    outing.Text2 = text;
                    outing.Text2LastUpdated = DateTime.Now;
                }
                else
                {
                    throw new Exception();
                }

                c.SaveChanges();
            }
        }
        public string GetText(int passengerId, short textId)
        {
            var outingId = GetCurrentOuting(passengerId);
            using (var c = new Hackathon2Entities())
            {
                var outing = c.Outings.First(x => x.OutingId == outingId);
                if (textId == 1)
                {
                    return outing.Text1;
                }
                else if (textId == 2)
                {
                    return outing.Text1;
                }
                else if (textId == 1)
                {
                    return outing.Text1;
                }
                else
                {
                    return "One second please!";
                }
            }
        }

        public DateTime? GetTextLastUpdated(int outingId, short textId)
        {
            using (var c = new Hackathon2Entities())
            {
                var o = c.Outings.First(x => x.OutingId == outingId);
                
                if (textId == 1)
                {
                    return o.Text1LastUpdated;
                }
                else if (textId == 2)
                {
                    return o.Text2LastUpdated;
                }
                else if (textId == 1)
                {
                    return o.Text3LastUpdated;
                }
                else
                {
                    throw new Exception();
                }
            }
        }
    }
}
