using MedicationReminder.Models;
using MedicationReminder.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MedicationReminder.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel();
        }
        async void SigUpButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(SignUpPage)}");

        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (Application.Current.Properties["IsUserLoggedIn"].Equals(true))
            {
                await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
            }
        }
        
    }
}