﻿using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Application template is documented at http://go.microsoft.com/fwlink/?LinkId=234227

namespace PuppyTrackerApp
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public sealed partial class App 
    {

        private WinRTContainer container;

        public App()
        {
            InitializeComponent();
        }


        protected override void Configure()
        {
            container = new WinRTContainer();

            container.RegisterWinRTServices();

            container.PerRequest<MainViewModel>();
            container.PerRequest<LoginViewModel>();
            
        }


        protected override void OnActivated(IActivatedEventArgs args)
        {
            base.OnActivated(args);

        }

        protected override void PrepareViewFirst(Frame rootFrame)
        {
            container.RegisterNavigationService(rootFrame);
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            DisplayRootView<LoginView>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }
    }
}