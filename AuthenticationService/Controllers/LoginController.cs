using AuthenticationService.DTOs.AppUser;
using AuthenticationService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthenticationService.Controllers
{
    [Route("api/auth/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserManager<UserModel> userManager;
        private readonly IConfiguration configuration;

        public LoginController(UserManager<UserModel> userManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }


        /// <summary>
        /// Logs User In , User Sends In Login Data, Gets Back token, userid and role
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> LoginUser(AppUserDtoForLogin model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            var role = (await userManager.GetRolesAsync(user)).FirstOrDefault();
            if (user is not null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                var TokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(new Claim[] {
                        new Claim("UserID", user.Id.ToString()),
                        new Claim(ClaimTypes.Role, role ?? "User")

                    }),
                    Expires = DateTime.UtcNow.AddDays(3),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                        , SecurityAlgorithms.HmacSha256
                    ),
                    Issuer = configuration["Jwt:Issuer"],
                    Audience = configuration["Jwt:Audience"]                    
                };
                var TokenHandler = new JwtSecurityTokenHandler();
                var securitytoken = TokenHandler.CreateToken(TokenDescriptor);
                var token = TokenHandler.WriteToken(securitytoken);
                return Ok(new { token, user.Id, role});;
            }
            else
                return BadRequest(new { message = "UserName Or Password is Incorrect" });
        }

    }
}
