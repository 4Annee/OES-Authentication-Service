using AuthenticationService.DTOs.AppUser;
using AuthenticationService.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository userrepo;

        public UsersController(IUserRepository userrepo)
        {
            this.userrepo = userrepo;
        }

        [HttpGet("search/{searchterm}")]
        public IActionResult SearchUser(string searchterm)
        {
            var res = userrepo.SearchForUser(searchterm);
            if(res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }

        [HttpGet("details/{id}")]
        public IActionResult GetUserDetails(string id)
        {
            var res = userrepo.GetUserDetails(id);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }

        [HttpGet("group/{id}")]
        public IActionResult GetGroupStudents(Guid id)
        {
            var res = userrepo.GetStudentsByGroup(id);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }

        [HttpGet("section/{id}")]
        public IActionResult GetSectionStudents(Guid id)
        {
            var res = userrepo.GetStudentsBySection(id);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }

        [HttpGet("year/{id}")]
        public IActionResult GetYearStudents(Guid id)
        {
            var res = userrepo.GetStudentsByYear(id);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }

        [HttpGet("teachers")]
        public IActionResult GetTeachersList()
        {
            var res = userrepo.GetTeachersList();
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }
    }
}
