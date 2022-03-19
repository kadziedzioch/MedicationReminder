using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MedicationReminder.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddReminderPage());
        }
    }
}