using AuthenticationService.DTOs.AppUser;
using AuthenticationService.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace AuthenticationService.Services
{

    public interface IUserRegistrationService
    {
        Task<string> RegisterAppUser(AppUserDtoForCreation user);
        Task<bool> ChangeAppUserPassword(ClaimsPrincipal User,AppUserDtoForChangingPassword model);
    }
    public class UserRegistrationService : IUserRegistrationService
    {
        private readonly UserManager<UserModel> userManager;
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;

        public UserRegistrationService(UserManager<UserModel> userManager, IConfiguration configuration, IMapper mapper)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.mapper = mapper;
        }

        public async Task<bool> ChangeAppUserPassword(ClaimsPrincipal User,AppUserDtoForChangingPassword model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            string Userid;
            try
            {
                Userid = User.Claims.FirstOrDefault(c => c.Type == "UserID").Value;
            }
            catch
            {
                return false;
            }
            if (user.Id != Userid)
                return false;

            if (user is not null && await userManager.CheckPasswordAsync(user, model.OldPassword))
            {
                var res = await userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                if (res.Succeeded)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public async Task<string> RegisterAppUser(AppUserDtoForCreation user)
        {
            try
            {
                var appUser = mapper.Map<UserModel>(user);
                await userManager.CreateAsync(appUser, user.Password);
                return "true";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
