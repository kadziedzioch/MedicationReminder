using MedicationReminder.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MedicationReminder.ViewModels
{
    class AddReminderViewModel: BaseViewModel
    {
        public ObservableCollection<Medicine> Medicines { get; set; }
        public ObservableCollection<RemindTime> RemindTimes { get; set; }
        public Command AddRemindTimeCommand { get; }
        public Command DeleteCommand { get; }

        public AddReminderViewModel()
        {
            Title = "Dodaj przypomnienie";
            Medicines = new ObservableCollection<Medicine>();
            RemindTimes = new ObservableCollection<RemindTime>();
            AddRemindTimeCommand = new Command(AddRemindTime);
            DeleteCommand = new Command(OnDeleteTapped);
        }

        private void AddRemindTime()
        {
            RemindTimes.Add(new RemindTime
            {
                RemindTimeId = Guid.NewGuid().ToString(),
                Dose = 1,
                Time = new TimeSpan(8,0,0)
            });

        }

        private void OnDeleteTapped(object obj)
        {
            var content = obj as RemindTime;
            RemindTimes.Remove(content);
        }



    }
}
