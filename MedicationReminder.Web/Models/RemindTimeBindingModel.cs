using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicationReminder.Web.Models
{
    public class RemindTimeBindingModel
    {
        public string RemindTimeId { get; set; }
        public string MedicineId { get; set; }
        public string UserId { get; set; }
        public string Time { get; set; }
        public int Dose { get; set; }
        public bool IsSelected { get; set; }
    }
}
