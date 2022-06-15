using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.Controllers
{
    [Route("api/auth/[controller]")]
    [ApiController]
    public class VerificationController : ControllerBase
    {

        /// <summary>
        /// Verify The Validity Of A Token
        /// </summary>
        [HttpGet("token")]
        public IActionResult VerifyToken()
        {
            if(User.Identity != null)
                if (User.Identity.IsAuthenticated)
                    return Ok();
            return Unauthorized();
        }

        /// <summary>
        /// Check The Role Of A User
        /// </summary>
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
