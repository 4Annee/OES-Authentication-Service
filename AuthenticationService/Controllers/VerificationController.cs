using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.Controllers
{
    [Route("api/auth/[controller]")]
    [ApiController]
    public class VerificationController : ControllerBase
    {
        [HttpGet("token")]
        public IActionResult VerifyToken()
        {
            if(User.Identity != null)
                if (User.Identity.IsAuthenticated)
                    return Ok();
            return Unauthorized();
        }

        [HttpGet("role/{role}")]
        [Authorize]
        public IActionResult VerifyRole(string role)
        {
            if (User.IsInRole(role))
                return Ok();
            return Unauthorized();
        }
    }
}
