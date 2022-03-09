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
