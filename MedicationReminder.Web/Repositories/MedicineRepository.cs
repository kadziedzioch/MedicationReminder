using MedicationReminder.Web.Entities;
using MedicationReminder.Web.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicationReminder.Web.Repositories
{
    public class MedicineRepository:BaseRepository<MedicineEntity>, IMedicineRepository
    {
        protected override DbSet<MedicineEntity> DbSet => mDbContext.Medicines;

        public MedicineRepository(DatabaseContext dbContext) : base(dbContext)
        {

        }

        public IEnumerable<MedicineEntity> GetAllMedicines()
        {
            return DbSet.Select(x => x);
        }

        public bool AddNewMedicine(MedicineEntity medicine)
        {
            DbSet.Add(medicine);

            return SaveChanges();
        }

        public bool Delete(MedicineEntity medicine)
        {
            var foundMedicine = DbSet.Where(x => x.MedicineId == medicine.MedicineId).FirstOrDefault();

            if (foundMedicine != null)
            {
                DbSet.Remove(foundMedicine);
                return SaveChanges();
            }

            return false;

        }
    }
}
