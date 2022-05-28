using Plugin.LocalNotification;
using System;
using System.ComponentModel;
using System.Linq;
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

        async void OnLogout_Clicked(object sender, EventArgs e)
        {

            Application.Current.Properties["IsUserLoggedIn"] = false;
            string username = Application.Current.Properties["CurrentUsername"].ToString();
            Application.Current.Properties["CurrentUsername"] = "";
            Application.Current.MainPage = new AppShell();
            await Application.Current.SavePropertiesAsync();

            var list = NotificationCenter.Current.GetPendingNotificationList().Result.Where(x => x.Subtitle == username);

            foreach (var item in list)
            {
                NotificationCenter.Current.Cancel(item.NotificationId);
            }

            
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            

        }
    }
}