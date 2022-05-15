using MedicationReminder.Models;
using MedicationReminder.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MedicationReminder.ViewModels
{
    class TodayRemaindersViewModel :BaseViewModel
    {
        public ObservableCollection<Medicine> Medicines { get; set; }
        public Command LoadMedicinesCommand { get; }
        private Medicine _selectedMedicine;
        public Command<Medicine> MedicineTapped { get; }
        public Command DeleteMedicine { get; }
        public TodayRemaindersViewModel()
        {
            Title = "Wszystkie przypomnienia";
            Medicines = new ObservableCollection<Medicine>();
            LoadMedicinesCommand = new Command(async () => await ExecuteLoadItems());
            MedicineTapped = new Command<Medicine>(OnMedicineSelected);
            DeleteMedicine = new Command(OnDeleteTapped);
        }

        public Medicine SelectedMedicine
        {
            get => _selectedMedicine;
            set
            {
                SetProperty(ref _selectedMedicine, value);
                OnMedicineSelected(value);
            }
        }

        public async Task ExecuteLoadItems()
        {
            IsBusy = true;

            try
            {
                Medicines.Clear();
                HttpClientHandler insecureHandler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                };
                HttpClient client = new HttpClient(insecureHandler);

                string UserId = Application.Current.Properties["CurrentUsername"].ToString();
                var response = await client.GetAsync("http://10.0.2.2:9481/api/reminder/GetUsersMedicines?userName="+ UserId);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var medicines = JsonConvert.DeserializeObject<ObservableCollection<Medicine>>(content);
                    foreach (Medicine medicine in medicines)
                    {
                        Medicines.Add(medicine);
                    }
                }
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedMedicine = null;
        }

        async void OnMedicineSelected(Medicine medicine)
        {
            if (medicine == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(ReminderDetailPage)}?{nameof(ReminderDetailViewModel.MedicineId)}={medicine.MedicineId}");
        }

        private async void OnDeleteTapped(object obj)
        {
            var content = obj as Medicine;
            
            bool answer = await Application.Current.MainPage.DisplayAlert("Potwierdzenie", "Czy na pewno chcesz usunąć przypomnienia o tym leku?", "Tak", "Nie");

            if (answer)
            {
                HttpClientHandler insecureHandler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                };
                HttpClient client = new HttpClient(insecureHandler);
                var response = await client.GetAsync("http://10.0.2.2:9481/api/reminder/DeleteMedicine?medicineId=" + content.MedicineId);

                if (response.IsSuccessStatusCode)
                {
                    var medicine = Medicines.Where(x => x.MedicineId == content.MedicineId).FirstOrDefault();
                    if (medicine != null)
                    {
                        Medicines.Remove(medicine);
                    }
                }
            }
            
        }
    }
}
