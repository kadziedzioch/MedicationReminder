using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicationReminder.Web.Repositories
{
    public abstract class BaseRepository<Entity> where Entity:class
    {
        protected DatabaseContext mDbContext;

        protected abstract DbSet<Entity> DbSet {get; }

        public BaseRepository(DatabaseContext dbContext)
        {
            mDbContext = dbContext;
        }

        public List<Entity> GetAll()
        {
            var list = new List<Entity>();
            var entities = DbSet;

            foreach (var entity in entities)
            {
                list.Add(entity);
            }
            return list;

        }

        public bool SaveChanges()
        {
            return mDbContext.SaveChanges() > 0;
        }
    }
}
