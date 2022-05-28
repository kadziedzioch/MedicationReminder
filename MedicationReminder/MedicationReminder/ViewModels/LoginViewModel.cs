using MedicationReminder.Models;
using MedicationReminder.Views;
using Newtonsoft.Json;
using Plugin.LocalNotification;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
                if (!Application.Current.Properties.ContainsKey("CurrentUsername"))
                {
                    Application.Current.Properties.Add("CurrentUsername", user.Username);
                }
                Application.Current.Properties["CurrentUsername"] = user.Username;
                await Application.Current.SavePropertiesAsync();

                await CreateRemindTimes();
                
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

        private async Task CreateRemindTimes()
        {
            HttpClientHandler insecureHandler = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };
            HttpClient client = new HttpClient(insecureHandler);

            string UserId = Application.Current.Properties["CurrentUsername"].ToString();
            var medicinesFromDb = await client.GetAsync("http://10.0.2.2:9481/api/reminder/GetUsersMedicines?userName=" + UserId);
            var remindTimesFromDb = await client.GetAsync("http://10.0.2.2:9481/api/reminder/GetUsersRemindTimes?userName=" + UserId);

            var firstcontent = await medicinesFromDb.Content.ReadAsStringAsync();
            var medicines = JsonConvert.DeserializeObject<ObservableCollection<Medicine>>(firstcontent);

            var secondcontent = await remindTimesFromDb.Content.ReadAsStringAsync();
            var remindTimes = JsonConvert.DeserializeObject<ObservableCollection<RemindTimeToDatabase>>(secondcontent);

            DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            foreach (var item in remindTimes)
            {
                Random rnd = new Random();
                string medicineName = medicines.Where(x => x.MedicineId == item.MedicineId).Select(x => x.MedicineName).FirstOrDefault();
                TimeSpan time = TimeSpan.Parse(item.Time);
                var notification = new NotificationRequest
                {
                    BadgeNumber = 1,
                    Description = "Przypomnienie o leku: " + medicineName + ", dawka to: " + item.Dose + " tabletka",
                    Title = medicineName,
                    Schedule =
                    {
                        NotifyTime = dt + time,
                        NotifyRepeatInterval = new TimeSpan(24, 0, 0),
                        RepeatType = NotificationRepeat.TimeInterval
                    },
                    NotificationId = rnd.Next(),
                    Subtitle = Application.Current.Properties["CurrentUsername"].ToString()
                };
                await NotificationCenter.Current.Show(notification);


                if (item.IsSelected)
                {
                    var notification2 = new NotificationRequest
                    {
                        BadgeNumber = 1,
                        Description = "Pamiętaj o pozostaniu na czczo przez kolejne 2 godziny!",
                        Title = medicineName,
                        Schedule =
                        {
                            NotifyTime = dt + new TimeSpan(time.Hours-2,time.Minutes, time.Seconds),
                            NotifyRepeatInterval = new TimeSpan(24,0,0),
                            RepeatType = NotificationRepeat.TimeInterval
                        },
                        NotificationId = rnd.Next(),
                        Subtitle = Application.Current.Properties["CurrentUsername"].ToString()

                    };
                    await NotificationCenter.Current.Show(notification2);
                }
            }


        }

    }
}
