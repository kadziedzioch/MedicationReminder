using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicationReminder.Web.Entities
{
    public class UserEntity
    {
        [Key]
        public string UserId { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }
    }
}
