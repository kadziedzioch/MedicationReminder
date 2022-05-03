using MedicationReminder.Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicationReminder.Web.Repositories.Interfaces
{
    public interface IMedicineRepository
    {
        IEnumerable<MedicineEntity> GetAllMedicines();
        bool AddNewMedicine(MedicineEntity medicine);
        bool Delete(MedicineEntity medicine);
    }
}
