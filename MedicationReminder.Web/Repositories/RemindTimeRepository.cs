using MedicationReminder.Web.Entities;
using MedicationReminder.Web.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicationReminder.Web.Repositories
{
    public class RemindTimeRepository : BaseRepository<RemindTimeEntity>, IRemindTimeRepository
    {
        protected override DbSet<RemindTimeEntity> DbSet => mDbContext.RemindTimes;

        public RemindTimeRepository(DatabaseContext dbContext) :base(dbContext)
        {

        }

        public IEnumerable<RemindTimeEntity> GetAllRemindTimes()
        {
            return DbSet.Select(x => x);
        }

        public bool AddNewRemindTime(RemindTimeEntity remindTime)
        {
            DbSet.Add(remindTime);

            return SaveChanges();
        }

        public bool Delete(RemindTimeEntity remindTime)
        {
            var foundRemindTime = DbSet.Where(x => x.RemindTimeId == remindTime.RemindTimeId).FirstOrDefault();

            if (foundRemindTime != null)
            {
                DbSet.Remove(foundRemindTime);
                return SaveChanges();
            }

            return false;

        }
    }
}
