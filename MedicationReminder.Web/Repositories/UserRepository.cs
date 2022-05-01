using MedicationReminder.Web.Entities;
using MedicationReminder.Web.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicationReminder.Web.Repositories
{
    public class UserRepository: BaseRepository<UserEntity>, IUserRepository
    {
        protected override DbSet<UserEntity> DbSet => mDbContext.Users;

        public UserRepository(DatabaseContext dbContext):base(dbContext)
        {

        }

        public IEnumerable<UserEntity> GetAllUsers()
        {
            return DbSet.Select(x => x);
        }

        public bool AddNewUser(UserEntity user)
        {
            DbSet.Add(user);

            return SaveChanges();
        }

        public bool Delete(UserEntity user)
        {
            var foundUser = DbSet.Where(x => x.UserId == user.UserId).FirstOrDefault();
            
            if(foundUser != null)
            {
                DbSet.Remove(foundUser);
                return SaveChanges();
            }

            return false;
            
        }

    }
}
