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
                Time = TimeSpan.Parse(model2.Time)
            };

            bool result = remindTimeRepository.AddNewRemindTime(remindTime);

            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
