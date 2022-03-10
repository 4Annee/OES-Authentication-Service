using AuthenticationService.Data;
using AuthenticationService.DTOs.IdentityRole;
using AuthenticationService.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationService.Services
{
    public interface IRoleManagementService
    {
        Task<List<IdentityRoleDto>> GetAllRolesAsync();
        Task<IdentityRoleDto> CreateRole(IdentityRoleDtoForCreation role);
        Task<bool> AddUserToRole(string userid,string roleid);
        Task<bool> RemoveUserFromRole(string userid,string roleid);
    }
    public class RoleManagementService : IRoleManagementService
    {
        private readonly UserManager<UserModel> userMang;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserServiceContext context;
        private readonly IMapper mapper;

        public RoleManagementService(UserManager<UserModel> userMang,RoleManager<IdentityRole> roleManager
            ,UserServiceContext context,
            IMapper mapper)
        {
            this.userMang = userMang;
            this.roleManager = roleManager;
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<bool> AddUserToRole(string userid, string roleid)
        {
            if (roleManager.Roles.Any(e => e.Id == roleid) && context.Users.Any(u=>u.Id == userid))
            {
                await userMang.AddToRoleAsync(await userMang.FindByIdAsync(userid),
                    (await roleManager.FindByIdAsync(roleid.ToString())).Name);
                return true;
            }
            return false;
        }

        public async Task<IdentityRoleDto> CreateRole(IdentityRoleDtoForCreation role)
        {
            var rolemodel = mapper.Map<IdentityRole>(role);
            await roleManager.CreateAsync(rolemodel);
            var dto = mapper.Map<IdentityRoleDto>(await roleManager.FindByNameAsync(role.Name));
            return dto;
        }

        public Task<List<IdentityRoleDto>> GetAllRolesAsync()
        {
            return Task.FromResult(mapper.Map<List<IdentityRoleDto>>(roleManager.Roles.ToList()));
        }

        public async Task<bool> RemoveUserFromRole(string userid, string roleid)
        {
            if (roleManager.Roles.Any(e => e.Id == roleid) && context.Users.Any(u => u.Id == userid))
            {
                await userMang.RemoveFromRoleAsync(await userMang.FindByIdAsync(userid),
                    (await roleManager.FindByIdAsync(roleid.ToString())).Name);
                return true;
            }
            return false;
        }
    }
}
