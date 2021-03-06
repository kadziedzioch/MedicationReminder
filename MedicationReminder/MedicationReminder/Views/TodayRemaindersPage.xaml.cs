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
    public partial class TodayRemaindersPage : ContentPage
    {
        TodayRemaindersViewModel _viewModel;
        public TodayRemaindersPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new TodayRemaindersViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}