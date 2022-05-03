using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicationReminder.Web.Models
{
    public class MedicineBindingModel
    {
        public string MedicineId { get; set; }
        public string MedicineName { get; set; }
        public bool IsImmunosuppressive { get; set; }
    }
}
