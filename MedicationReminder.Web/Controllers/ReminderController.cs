using MedicationReminder.Web.Entities;
using MedicationReminder.Web.Models;
using MedicationReminder.Web.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicationReminder.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReminderController : ControllerBase
    {
        public IRemindTimeRepository remindTimeRepository;
        public IMedicineRepository medicineRepository;
        public IUserRepository userRepository;
        public ReminderController(IRemindTimeRepository remindTimeRepository,
            IMedicineRepository medicineRepository, 
            IUserRepository userRepository)
        {
            this.remindTimeRepository = remindTimeRepository;
            this.medicineRepository = medicineRepository;
            this.userRepository = userRepository;
        }


        [AllowAnonymous]
        [Route("SaveMedicine")]
        public IActionResult SaveMedicine(MedicineBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var medicine = new MedicineEntity()
            {
                MedicineId = model.MedicineId,
                MedicineName = model.MedicineName,
                IsImmunosuppressive = model.IsImmunosuppressive
            };

            bool result = medicineRepository.AddNewMedicine(medicine);

            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }

        [AllowAnonymous]
        [Route("SaveRemindTime")]
        public IActionResult SaveRemindTime(RemindTimeBindingModel model2)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var remindTime = new RemindTimeEntity()
            {
                RemindTimeId = model2.RemindTimeId,
                Dose = model2.Dose,
                MedicineId = model2.MedicineId,
                UserId = userRepository.GetAllUsers().Where(x => x.Username == model2.UserId).Select(x => x.UserId).FirstOrDefault(),
                Time = TimeSpan.Parse(model2.Time),
                IsSelected = model2.IsSelected
            };

            bool result = remindTimeRepository.AddNewRemindTime(remindTime);

            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }

        [AllowAnonymous]
        [Route("GetUsersMedicines")]
        public IActionResult GetUsersMedicines(string userName)
        {
            var userId = userRepository.GetAllUsers().Where(x => x.Username == userName).Select(x => x.UserId).FirstOrDefault();
            if(userId == null)
            {
                return BadRequest();
            }
            var medicinesId = remindTimeRepository.GetAllRemindTimes().Where(x => x.UserId == userId).Select(x => x.MedicineId).ToList();
            if(medicinesId == null)
            {
                return BadRequest();
            }

            var medicines = new List<MedicineEntity>();
            foreach(var medicineId in medicinesId)
            {
                var medicine = medicineRepository.GetAllMedicines().Where(x => x.MedicineId == medicineId).FirstOrDefault();
                medicines.Add(medicine);
            }

            return Ok(medicines);
        }

        [AllowAnonymous]
        [Route("GetUsersRemindTimes")]
        public IActionResult GetUsersRemindTimes(string userName)
        {
            var userId = userRepository.GetAllUsers().Where(x => x.Username == userName).Select(x => x.UserId).FirstOrDefault();
            if (userId == null)
            {
                return BadRequest();
            }
            var remindTimes = remindTimeRepository.GetAllRemindTimes().Where(x => x.UserId == userId).ToList();
            if (remindTimes == null)
            {
                return BadRequest();
            }
            List<RemindTimeBindingModel> remindTimesToSend = new List<RemindTimeBindingModel>();
            foreach(var remindTime in remindTimes)
            {
                remindTimesToSend.Add(new RemindTimeBindingModel
                {
                    MedicineId = remindTime.MedicineId,
                    RemindTimeId = remindTime.RemindTimeId,
                    Dose = remindTime.Dose,
                    Time = remindTime.Time.ToString(),
                    IsSelected = remindTime.IsSelected,
                    UserId = remindTime.UserId

                });
            }
            return Ok(remindTimesToSend);

        }

        [AllowAnonymous]
        [Route("DeleteMedicine")]
        public IActionResult DeleteMedicine(string medicineId)
        {
            bool result = false;
            bool result2 = false;

            List<RemindTimeEntity> remindTimes = remindTimeRepository.GetAllRemindTimes().Where(x => x.MedicineId == medicineId).ToList();

            if (remindTimes != null)
            {
                foreach(RemindTimeEntity remindTime in remindTimes)
                {
                    result2 = remindTimeRepository.Delete(remindTime);
                }
            }

            MedicineEntity medicine = medicineRepository.GetAllMedicines().Where(x => x.MedicineId == medicineId).FirstOrDefault();
            if (medicine != null)
            {
                result = medicineRepository.Delete(medicine);
            }

            if ((result & result2)==false)
            {
                return BadRequest();
            }

            return Ok();
        }

        [AllowAnonymous]
        [Route("GetMedicineName")]
        public IActionResult GetMedicineName(string medicineId)
        {
            MedicineEntity medicine = medicineRepository.GetAllMedicines().Where(x => x.MedicineId == medicineId).FirstOrDefault();

            return medicine == null ? BadRequest() : Ok(medicine.MedicineName);
        }
    }
}
