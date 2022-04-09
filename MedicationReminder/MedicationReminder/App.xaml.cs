﻿using MedicationReminder.Services;
using MedicationReminder.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MedicationReminder
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
            //MainPage = new NavigationPage(new Views.LoginPage());

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
