using System;
using System.Collections.Generic;
using System.Text;

namespace MedicationReminder.Models
{
    class Medicine
    {
        public string MedicineId { get; set; }
        public string MedicineName { get; set; }
        public bool IsImmunosuppressive { get; set; }

    }
}
