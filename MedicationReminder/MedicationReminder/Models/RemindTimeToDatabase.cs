using System;
using System.Collections.Generic;
using System.Text;

namespace MedicationReminder.Models
{
    class RemindTimeToDatabase
    {
        public string RemindTimeId { get; set; }

        public string MedicineId { get; set; }

        public string UserId { get; set; }

        public string Time { get; set; }

        public int Dose { get; set; }

        public bool IsSelected { get; set; }

    }
}
