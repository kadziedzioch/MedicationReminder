using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MedicationReminder.Models
{
    class RemindTime
    {
        public string RemindTimeId { get; set; }

        public string MedicineId { get; set; }

        public TimePicker Time { get; set; }

        public int Dose { get; set; }

    }
}
