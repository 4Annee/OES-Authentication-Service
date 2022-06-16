using AuthenticationService.DTOs.AppUser;
using AuthenticationService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.Controllers
{
    [Route("api/auth/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IUserRegistrationService service;
        public RegisterController(IUserRegistrationService service)
        {
            this.service = service;
        }


        /// <summary>
        /// Create New User
        /// </summary>
        [HttpPost]
        //[Authorize(Roles ="Admin")]
        public async Task<IActionResult> PostAppUser(AppUserDtoForCreation user)
        {
            var res = await service.RegisterAppUser(user);
            if (res == "true")
                return Ok();
            return BadRequest(res);
        }


        /// <summary>
        /// Change User Password
        /// </summary>
        [HttpPost]
        //[Authorize]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword(AppUserDtoForChangingPassword model)
        {
            if (await service.ChangeAppUserPassword(User, model))
                return Ok();
            return Unauthorized();
        }
    }
}
