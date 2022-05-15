using MedicationReminder.Models;
using MedicationReminder.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MedicationReminder.ViewModels
{
    class AddReminderViewModel: BaseViewModel
    {
        public ObservableCollection<RemindTime> RemindTimes { get; set; }
        public Command AddRemindTimeCommand { get; }
        public Command DeleteCommand { get; }
        public Command SaveCommand { get; }
        public Command<RemindTime> RemindTimeTapped { get; }
        public int newDose { get; set; }
        public TimeSpan newTime { get; set; }
        public string newMedicineName { get; set; }
        public bool newIsImmunosupressive { get; set; }
        public bool newIsSelected { get; set; }

        public AddReminderViewModel()
        {
            Title = "Dodaj przypomnienie";
            RemindTimes = new ObservableCollection<RemindTime>();
            AddRemindTimeCommand = new Command(AddRemindTime);
            DeleteCommand = new Command(OnDeleteTapped);
            SaveCommand = new Command(Save);
            newDose = 1;
            newTime = new TimeSpan(8, 0, 0);
            newIsSelected = false;
        }

        private void AddRemindTime()
        {
            RemindTimes.Add(new RemindTime
            {
                RemindTimeId = Guid.NewGuid().ToString(),
                Dose = newDose,
                Time = newTime,
                IsSelected = false
            });

        }

        private void OnDeleteTapped(object obj)
        {
            var content = obj as RemindTime;
            RemindTimes.Remove(content);
        }

        private async void Save()
        {
            bool isSucceeded = await SaveRemindTimeAndMedicine();
            if (isSucceeded)
            {
                await Application.Current.MainPage.DisplayAlert("Operacja powiodła się", "Twoje przypomnienie zostało dodane", "OK");
                RemindTimes.Clear();
                newMedicineName = "";
                newIsImmunosupressive = false;
                OnPropertyChanged(nameof(newMedicineName));
                OnPropertyChanged(nameof(newIsImmunosupressive));
                OnPropertyChanged(nameof(RemindTimes));

            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Operacja nie powiodła się", "Wystąpił błąd!", "OK");
            }

        }

        public async Task<bool> SaveRemindTimeAndMedicine()
        {
            HttpClientHandler insecureHandler = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };
            HttpClient client = new HttpClient(insecureHandler);

            var medicine = new Medicine
            {
                MedicineId = Guid.NewGuid().ToString(),
                MedicineName = newMedicineName,
                IsImmunosuppressive = newIsImmunosupressive
            };
            var jsonMedicine = JsonConvert.SerializeObject(medicine);
            HttpContent contentMedicine = new StringContent(jsonMedicine, Encoding.UTF8, "application/json");
            var responseMedicine = await client.PostAsync("http://10.0.2.2:9481/api/reminder/savemedicine", contentMedicine).ConfigureAwait(false);

            bool remindResponse = false;
            
            foreach (var remindTime in RemindTimes)
            {

                var jsonRemindTime = JsonConvert.SerializeObject(new RemindTimeToDatabase
                {
                    RemindTimeId = Guid.NewGuid().ToString(),
                    UserId = Application.Current.Properties["CurrentUsername"].ToString(),
                    MedicineId = medicine.MedicineId,
                    Time = remindTime.Time.ToString(),
                    Dose = remindTime.Dose,
                    IsSelected = remindTime.IsSelected

                });

                HttpContent contentRemindTime = new StringContent(jsonRemindTime, Encoding.UTF8, "application/json");
                var responseRemindTime = await client.PostAsync("http://10.0.2.2:9481/api/reminder/saveremindtime", contentRemindTime).ConfigureAwait(false);
               
                if (responseRemindTime.IsSuccessStatusCode)
                {
                    remindResponse = true;
                }
                else
                {
                    remindResponse = false;
                    break;
                }
            }

            return remindResponse;

        }

        

    }
}
