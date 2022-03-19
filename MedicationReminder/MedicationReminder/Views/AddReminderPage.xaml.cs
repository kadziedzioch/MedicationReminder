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
    public partial class AddReminderPage : ContentPage
    {
        public AddReminderPage()
        {
            InitializeComponent();
            this.BindingContext = new AddReminderViewModel();
        }
    }
}