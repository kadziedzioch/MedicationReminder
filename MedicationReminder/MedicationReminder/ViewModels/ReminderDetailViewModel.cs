using MedicationReminder.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;

namespace MedicationReminder.ViewModels
{
    [QueryProperty(nameof(MedicineId), nameof(MedicineId))]
    class ReminderDetailViewModel : BaseViewModel
    {
        public ObservableCollection<RemindTime> RemindTimes { get; set; }
        private string medicineId;

        public string Id { get; set; }
        public string MedicineName { get; set; }

        public ReminderDetailViewModel()
        {
            RemindTimes = new ObservableCollection<RemindTime>();
        }

        public string MedicineId
        {
            get
            {
                return medicineId;
            }
            set
            {
                medicineId = value;
                LoadItemId(value);
            }
        }


        public async void LoadItemId(string medicineId)
        {
            try
            {
                RemindTimes.Clear();
                HttpClientHandler insecureHandler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                };
                HttpClient client = new HttpClient(insecureHandler);

                string UserId = Application.Current.Properties["CurrentUsername"].ToString();
                var response = await client.GetAsync("http://10.0.2.2:9481/api/reminder/GetUsersRemindTimes?userName=" + UserId);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var remindTimesFromDb = JsonConvert.DeserializeObject<ObservableCollection<RemindTimeToDatabase>>(content);
                    var remindTimes = remindTimesFromDb.Where(x => x.MedicineId == medicineId).ToList();
                    foreach (RemindTimeToDatabase remindtimefromdb in remindTimes)
                    {
                        RemindTimes.Add(new RemindTime
                        {
                            RemindTimeId = remindtimefromdb.RemindTimeId,
                            Dose = remindtimefromdb.Dose,
                            MedicineId = remindtimefromdb.MedicineId,
                            UserId = remindtimefromdb.UserId,
                            Time = TimeSpan.Parse(remindtimefromdb.Time),
                            IsSelected = remindtimefromdb.IsSelected

                        });
                    }
                }

                var response2 = await client.GetAsync("http://10.0.2.2:9481/api/reminder/GetMedicineName?medicineId=" + medicineId);

                if (response2.IsSuccessStatusCode)
                {
                    MedicineName = await response2.Content.ReadAsStringAsync();
                    Debug.WriteLine("cos");
                }
                

            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        

    }
}
