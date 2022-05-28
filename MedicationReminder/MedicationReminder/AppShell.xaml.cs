using MedicationReminder.ViewModels;
using MedicationReminder.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MedicationReminder
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
           

            Routing.RegisterRoute(nameof(ReminderDetailPage), typeof(ReminderDetailPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
