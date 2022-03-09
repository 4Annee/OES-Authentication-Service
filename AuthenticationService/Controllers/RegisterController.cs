using AuthenticationService.DTOs.AppUser;
using AuthenticationService.Models;
using AuthenticationService.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.Controllers
{
    [Route("api/auth/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IUserRegistrationService service;


        /// TODO : Make The Endpoints Using Services So That They Can Be Reused
        public RegisterController(IUserRegistrationService service)
        {
            this.service = service;
        }

        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> PostAppUser(AppUserDtoForCreation user)
        {
            if (await service.RegisterAppUser(user))
                return Ok();
            return BadRequest();
        }



        [HttpPost]
        [Authorize]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword(AppUserDtoForChangingPassword model)
        {
            if (await service.ChangeAppUserPassword(User, model))
                return Ok();
            return Unauthorized();
        }
    }
}
