using MedicationReminder.Models;
using MedicationReminder.Views;
using System;
using System.Collections.Generic;
using System.Text;
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

            var isVaild = AreCredentialsCorrect(user);
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

        bool AreCredentialsCorrect(User user)
        {
            return user.Username == Constants.Username && user.Password == Constants.Password;

        }

    }
}
