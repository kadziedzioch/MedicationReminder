using MedicationReminder.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace MedicationReminder.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}