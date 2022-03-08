using AuthenticationService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.Controllers
{
    [Route("api/auth/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserManager<UserModel> userManager;
        private readonly IConfiguration configuration;
        private readonly SignInManager<UserModel> signInManager;

        public LoginController(UserManager<UserModel> userManager, IConfiguration configuration, SignInManager<UserModel> signInManager)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.signInManager = signInManager;
        }
    }
}
