using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MedicationReminder.Web.Entities
{
    public class RemindTimeEntity
    {
        [Key]
        public string RemindTimeId { get; set; }

        [ForeignKey("Medicine")]
        public string MedicineId { get; set; }
        public virtual MedicineEntity Medicine { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual UserEntity User { get; set; }

        public TimeSpan Time { get; set; }

        public int Dose { get; set; }

        public bool IsSelected { get; set; }
    }
}
