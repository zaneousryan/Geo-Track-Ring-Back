using Caliburn.Micro;
using Hackathon2.Web.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace PuppyTrackerApp
{
    public class LoginViewModel : Screen
    {
        INavigationService _navigation;

        public LoginViewModel(INavigationService navigation)
        {
            _navigation = navigation;
            UserName = "shane94@hotmail.com";
            Password = "T3sting!";
        }

        public string UserName
        {
            get;
            set;
        }


        public string Password
        {
            get;
            set;
        }

        string _Status;
        public string Status
        {
            get { return _Status; }
            set
            {

                _Status = value;
                NotifyOfPropertyChange(() => Status);
            }
        }
        public async void Logon()
        {
            Status = "Logging in";
            var result = await new PuppyServices()
            .Authenticate(UserName);

            if (result < 1)
                Status = "failed to login";
            else
            {
                _navigation.UriFor<MainViewModel>()
                    .WithParam(x => x.PassengerId, result)
                    .Navigate();
            }

        }
    }
}
