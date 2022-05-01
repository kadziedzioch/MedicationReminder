using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicationReminder.Web.Entities
{
    public class MedicineEntity
    {
        [Key]
        public string MedicineId { get; set; }
        public string MedicineName { get; set; }
        public bool IsImmunosuppressive { get; set; }

    }
}
