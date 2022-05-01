using MedicationReminder.Models;
using MedicationReminder.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MedicationReminder.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }
        public string newUsername {get; set;}
        public string newPassword { get; set; }
        public string newLoginFaild { get; set; }

        public LoginViewModel()
        {
            LoginCommand = new Command(Login_Clicked);
        }

        async void Login_Clicked(object obj)
        {

            var user = new User
            {
                Username = newUsername,
                Password = newPassword
            };

            var isVaild = await AreCredentialsCorrect(user);
            if (isVaild)
            {
                Application.Current.Properties["IsUserLoggedIn"] = true;
                newUsername = string.Empty;
                newPassword = string.Empty;
                newLoginFaild = string.Empty;
                OnPropertyChanged(nameof(newUsername));
                OnPropertyChanged(nameof(newPassword));
                OnPropertyChanged(nameof(newLoginFaild));

                await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
            }
            else
            {
                newLoginFaild = "Nieprawidłowe hasło lub nazwa użytkownika!";
                newPassword = string.Empty;
                OnPropertyChanged(nameof(newLoginFaild));
                OnPropertyChanged(nameof(newPassword));
            }
            
        }

        private async Task<bool> AreCredentialsCorrect(User user)
        {
            //return user.Username == Constants.Username && user.Password == Constants.Password;
            HttpClientHandler insecureHandler = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };
            HttpClient client = new HttpClient(insecureHandler);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            var json = JsonConvert.SerializeObject(user);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("http://10.0.2.2:9481/api/account/login", content).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            return true;
        }

    }
}
