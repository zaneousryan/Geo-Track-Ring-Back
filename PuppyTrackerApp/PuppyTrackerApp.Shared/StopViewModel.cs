using Caliburn.Micro;
using Hackathon2.Services.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Windows.Web.Http;

namespace PuppyTrackerApp
{
	public class StopViewModel : Screen
	{
        StopMobile _Object;
        MainViewModel _Parent;
        bool _IsRoute;

        public StopViewModel(StopMobile o, MainViewModel parent, bool isRoute)
        {
            _Object = o;
            _Parent = parent;
            _IsRoute = isRoute;
        }

        public void UpdateObject(StopMobile o)
        {
            _Object = o;
            NotifyOfPropertyChange(() => StopStatusId);
        }

        public StopMobile GetObject()
        {
            return _Object; 
        }

        public void DoAddStop()
        {
            if (!_IsRoute)
            {
                _Parent.DoAddStop(_Object);
            }
        }


        public string Description
        {
            get
            {
                return _Object.Description;
            }
        }

        public int StopStatusId
        {
            get
            {
                return _Object.StopStatusId;
            }
        }

	}
}
