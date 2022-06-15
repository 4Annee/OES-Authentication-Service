using AuthenticationService.Data;
using AuthenticationService.DTOs.AppUser;
using AuthenticationService.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository userrepo;
        private readonly UserServiceContext context;
        private readonly IMapper mapper;

        public UsersController(IUserRepository userrepo,UserServiceContext context,IMapper mapper)
        {
            this.userrepo = userrepo;
            this.context = context;
            this.mapper = mapper;
        }


        /// <summary>
        /// Search For A User Using A Specific Term
        /// </summary>
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


        /// <summary>
        /// Get User Details
        /// </summary>
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


        /// <summary>
        /// Get Students In A Group
        /// </summary>
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


        /// <summary>
        /// Get Students In A Section
        /// </summary>
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


        /// <summary>
        /// Get Students In A Year
        /// </summary>
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


        /// <summary>
        /// Get List Of Teachers
        /// </summary>
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
        
        /// <summary>
        /// Get List Of Teachers Of Module
        /// </summary>
        [HttpGet("teachers/module/{id}")]
        public async Task<IActionResult> GetTeachersofModuleAsync(string id)
        {
            var res = await context.Modules.Include(u=>u.CourseTeachers).FirstOrDefaultAsync(u=>u.Id == id);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<List<AppUserDto>>(res.CourseTeachers));
        }
    }
}
