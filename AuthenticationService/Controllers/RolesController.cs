using AuthenticationService.DTOs.IdentityRole;
using AuthenticationService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.Controllers
{
    [Route("api/auth/[controller]")]
    [Authorize(Roles ="Admin")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleManagementService roleMng;

        public RolesController(IRoleManagementService service)
        {
            this.roleMng = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var res = await roleMng.GetAllRolesAsync();
            if(res == null)
                return NotFound();
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> AddUserToRole([FromBody]UserRoleDto userRole)
        {
            if (await roleMng.AddUserToRole(userRole.UserId, userRole.RoleId))
                return Ok(userRole);
            return BadRequest();
        }        
        
        
        [HttpDelete]
        public async Task<IActionResult> RemoveUserFromRole([FromBody]UserRoleDto userRole)
        {
            if (await roleMng.RemoveUserFromRole(userRole.UserId, userRole.RoleId))
                return NoContent();
            return BadRequest();
        }



        /**
        Endpoints : Can Be Added if Necessary As They Exist In The Service Injected
            - Add Role 
            3 - Remove User From Role
         */
    }
}
