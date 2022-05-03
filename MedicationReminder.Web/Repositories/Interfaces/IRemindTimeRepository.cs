using MedicationReminder.Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicationReminder.Web.Repositories.Interfaces
{
    public interface IRemindTimeRepository
    {
        IEnumerable<RemindTimeEntity> GetAllRemindTimes();
        bool AddNewRemindTime(RemindTimeEntity remindTime);
        bool Delete(RemindTimeEntity remindTime);
    }
}
