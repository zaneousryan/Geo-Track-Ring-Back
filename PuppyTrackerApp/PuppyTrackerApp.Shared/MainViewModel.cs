using Caliburn.Micro;
using Hackathon2.Services.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Windows.Web.Http;

using Hackathon2.Services;
using Hackathon2.Web.Services;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace PuppyTrackerApp
{
    public class MainViewModel : Screen
    {
        private PuppyServices _Service;
        int _PassengerId;
        public MainViewModel(int passengerId)
        {

        }


        public int PassengerId
        {
            get
            {
                return _PassengerId;
            }
            set
            {
                _PassengerId = value;
        }
        }

        private bool _IsRouteMode = false;

        public string CommandText
        {
            get
            {
                if (_IsRouteMode)
                {
                    return "New Route";
                }
                else
                {
                    return "Done";
                }
            }
        }

        private string _Text1;
        public string Text1
        {
            get
            {
                if (!_IsRouteMode)
                {
                    return "Plan your trip by selecting the order of your stops.";
                }
                return _Text1;
            }
            set
            {
                _Text1 = value;
                NotifyOfPropertyChange(() => Text1);
            }
        }

        public async void DoAddStop(StopMobile o)
        {
            var s = Stops.First(x => x.Description == o.Description);
            Stops.Remove(s);

            await _Service.AddStop(PassengerId, o.Description, o.Latitude, o.Longitude);
        }

        bool _updateCurrentLocation;
        public async void DoCommand()
        {
            
            _IsRouteMode = !_IsRouteMode;
            
            if(_IsRouteMode)
            {
                _updateCurrentLocation = true;
            }
            else
            {
                var result = await _Service.CreateOuting(PassengerId);
                
            }

            NotifyOfPropertyChange(() => CommandText);
            NotifyOfPropertyChange(() => Text1);

            await RefreshStops();
        }

        private async Task RefreshStops()
        {
            await Execute.OnUIThreadAsync(async () =>
                {
                    if (_IsRouteMode)
                    {
                        var k = await _Service.GetStopsForPassenger(PassengerId);
                        if (Stops.Count() != k.Count()
                            || !Stops.Any(x => k.Any(f => x.Description == f.Description)))
                        {
                            Stops.Clear();
                            foreach (var t in k)
                            {
                                Stops.Add(new StopViewModel(t, this, true));
                            }
                        }
                        else
                        {
                            foreach (var t in k)
                            {
                                var ptr = Stops.First(x => x.Description == t.Description);
                                ptr.UpdateObject(t);
                            }
                        }

                        Text1 = await _Service.GetText(PassengerId, 1);
                    }
                    else
                    {
                        Stops.Clear();
                        var x = await _Service.GetPossibleStopsForPassenger(PassengerId, null);
                        foreach (var t in x)
                        {
                            Stops.Add(new StopViewModel(t, this, false));
                        }
                        //Stops.Add(new StopViewModel(new StopMobile() { Description = "One", Latitude = 1, Longitude = 2 }, this));
                        //Stops.Add(new StopViewModel(new StopMobile() { Description = "Two", Latitude = 1, Longitude = 2 }, this));
                        //Stops.Add(new StopViewModel(new StopMobile() { Description = "Three", Latitude = 1, Longitude = 2 }, this));
                    }
                });
        }

        private ObservableCollection<StopViewModel> _Stops;

        public ObservableCollection<StopViewModel> Stops
        {
            get
            {
                if (_Stops == null)
                {
                    _Stops = new ObservableCollection<StopViewModel>();
                }
                return _Stops;
            }
        }


        bool timerExecuting = false;
        protected override async void OnActivate()
        {
            base.OnActivate();
            _Service = new PuppyServices();
            if (!_IsRouteMode)
            {
                var result = await _Service.CreateOuting(PassengerId);
            }

            var observableTimer = Observable.Interval(new TimeSpan(0,0,5));

            observableTimer.Where((x) => !timerExecuting && _IsRouteMode).Subscribe(Every5Seconds);

            _IsRouteMode = false;
            await RefreshStops();
            
        }




        public async Task UpdateGeoLocation()
        {
            await _Service.RockThePuppy(PassengerId, false);
        }

        private void Every5Seconds(long obj)
        {
            Execute.OnUIThread(async () =>
                {
                    timerExecuting = true;
                    try
                    {
                        if (_updateCurrentLocation)
                        {
                            await UpdateGeoLocation();
                            _updateCurrentLocation = false;
                        }
                        else if ((obj % 3) == 0)
                        {
                            await _Service.RockThePuppy(PassengerId, (obj % 6) == 0);
                        }

                        if ((obj % 3) == 0)
                        {
                            await RefreshStops();
                        }
                    }
                    catch
                    {

                    }
                    finally
                    {
                        timerExecuting = false;
                    }
                });
        }
    }
}
