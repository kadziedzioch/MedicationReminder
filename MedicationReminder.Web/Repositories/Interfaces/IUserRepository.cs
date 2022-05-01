using MedicationReminder.Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicationReminder.Web.Repositories.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<UserEntity> GetAllUsers();
        bool AddNewUser(UserEntity user);
        bool Delete(UserEntity user);
    }
}
