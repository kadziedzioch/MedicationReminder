using MedicationReminder.Web.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicationReminder.Web
{
    public class DatabaseContext:DbContext 
    {
        public DbSet<MedicineEntity> Medicines { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<RemindTimeEntity> RemindTimes { get; set; }

        public DatabaseContext(DbContextOptions options): base(options)
        {

        }
    }
}
