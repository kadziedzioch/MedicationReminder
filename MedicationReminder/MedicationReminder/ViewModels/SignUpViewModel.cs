using MedicationReminder.Models;
using MedicationReminder.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MedicationReminder.ViewModels
{
    class SignUpViewModel : BaseViewModel
    {
        public string newUsername { get; set; }
        public string newPassword { get; set; }
        public string newEmail { get; set; }
        public string newErrorText { get; set; }

        public Command SignUpCommand { get; set; }
        public SignUpViewModel()
        {
            SignUpCommand = new Command(SignUpButtonClicked);
        }

        private async void SignUpButtonClicked(object obj)
        {
            var user = new User
            {
                Username = newUsername,
                Password = newPassword,
                Email = newEmail
            };
            var signUpSucceeded = AreDetailsValid(user);
            HttpClientHandler insecureHandler = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            }; 
            HttpClient client = new HttpClient(insecureHandler);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            var json = JsonConvert.SerializeObject(user);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("http://10.0.2.2:9481/api/account/register", content).ConfigureAwait(false);

            Device.BeginInvokeOnMainThread(async () =>
            {
                if (response.IsSuccessStatusCode)
                {
                    Application.Current.Properties["IsUserLoggedIn"] = true;
                    if (!Application.Current.Properties.ContainsKey("CurrentUsername"))
                    {
                        Application.Current.Properties.Add("CurrentUsername", user.Username);
                    }
                    Application.Current.Properties["CurrentUsername"] = user.Username;
                    await Application.Current.SavePropertiesAsync();
                    await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");

                }
                else
                {
                    newErrorText = "Błąd rejestracji!";
                    OnPropertyChanged(nameof(newErrorText));
                }
            });
            
        }

        private bool AreDetailsValid(User user)
        {
            return !string.IsNullOrWhiteSpace(user.Username) && !string.IsNullOrWhiteSpace(user.Password) && !string.IsNullOrWhiteSpace(user.Email) && user.Email.Contains("@");
        }

        
    }
}
