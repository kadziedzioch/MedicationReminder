using MedicationReminder.Models;
using MedicationReminder.Views;
using System;
using System.Collections.Generic;
using System.Text;
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
            if (signUpSucceeded)
            {
                Application.Current.Properties["IsUserLoggedIn"] = true;
                await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
                
            }
            else
            {
                newErrorText = "Błąd rejestracji!";
            }

        }

        private bool AreDetailsValid(User user)
        {
            return !string.IsNullOrWhiteSpace(user.Username) && !string.IsNullOrWhiteSpace(user.Password) && !string.IsNullOrWhiteSpace(user.Email) && user.Email.Contains("@");
        }
    }
}
