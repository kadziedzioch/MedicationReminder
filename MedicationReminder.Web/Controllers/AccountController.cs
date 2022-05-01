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
    public class AccountController : ControllerBase
    {
        IUserRepository userRepository;
        public AccountController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [AllowAnonymous]
        [Route("Register")]
        public IActionResult Register(RegisterBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new UserEntity()
            {
                Username = model.Username,
                Password = model.Password,
                Email = model.Email,
                UserId = Guid.NewGuid().ToString()
            };

            bool result = userRepository.AddNewUser(user);

            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }

        [AllowAnonymous]
        [Route("Login")]
        public IActionResult Login(RegisterBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var users = userRepository.GetAllUsers().ToList();
            var user = users.Where(x => x.Username == model.Username && x.Password == model.Password).FirstOrDefault();

            if(user == null)
            {
                return BadRequest();
            }
            return Ok();

        }
    }
}
