using AuthenticationService.Data;
using AuthenticationService.DTOs.Module;
using AuthenticationService.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AuthenticationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModulesController : ControllerBase
    {
        private readonly UserServiceContext context;
        private readonly IMapper mapper;

        public ModulesController(UserServiceContext context,IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }


        /// <summary>
        /// Get User Modules For Both Teacher And Student
        /// </summary>
        [HttpGet("/{id}")]
        public async Task<IActionResult> GetUserModules(string id)
        {
            var user = await context.Users.Include(u => u.Group).Include(u=>u.Teaching).FirstOrDefaultAsync(u=>u.Id == id);
            
            if (user == null)
                return NotFound("User Doesn't Exist");
            if (user.Teaching != null)
            {
                return Ok(mapper.Map<List<ModuleDto>>(user.Teaching));
            }
            if (user.Group == null)
                return NotFound("User Doesn't Have Group");
            var section = context.Sections.Include(s => s.StudyModules).FirstOrDefault(s=>s.Id == user.Group.SectionId);
            if (section == null)
                return NotFound("Section Doesn't Exist");
            return Ok(mapper.Map<List<ModuleDto>>(section.StudyModules));
        }

        /// <summary>
        /// Gets Modules Given the year ID
        /// </summary>
        [HttpGet("/Year/{year}")]
        public async Task<IActionResult> Get(Guid idyear)
        {
            return Ok( mapper.Map<List<ModuleDto>>(await context.Sections.Where(s=>s.YearId == idyear)
                            .Include(s=>s.StudyModules).Select(s=>s.StudyModules).ToListAsync()));
        }

        /// <summary>
        /// Get Module By Id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(mapper.Map<ModuleDto>(await context.Modules.FindAsync(id)));
        }

        /// <summary>
        /// Create A new Module
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ModuleDtoForCreation dto)
        {
            context.Add(mapper.Map<StudyModule>(dto));
            await context.SaveChangesAsync();
            return Ok(dto);
        }

        /// <summary>
        /// Assign Module To Teacher
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AssignModuleToTeacher([FromBody]ModuleDTOAssignTeacher dto)
        {
            var module = await context.Modules.Include(m=>m.CourseTeachers).FirstOrDefaultAsync(u=>u.Id == dto.ModuleId);
            var user = await context.Users.FindAsync(dto.UserId);
            if (module != null && user != null)
            {
                if(module.CourseTeachers == null)
                {
                    module.CourseTeachers = new Collection<UserModel>();
                }
                module.CourseTeachers.Add(user);
            }
            return Ok(await context.SaveChangesAsync());
        }
        

        /// <summary>
        /// Assign Module To Section
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AssignModuleToSection([FromBody]ModuleDTOAssignSection dto)
        {
            var module = await context.Modules.Include(m => m.CourseTeachers).FirstOrDefaultAsync(u => u.Id == dto.ModuleId);
            var section = await context.Sections.FindAsync(dto.SectionId);
            if (module != null && section != null)
            {
                if (module.CourseStudents == null)
                {
                    module.CourseStudents = new Collection<Section>();
                }
                module.CourseStudents.Add(section);
            }
            return Ok(await context.SaveChangesAsync());
        }



        /// <summary>
        /// Delete A Module
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var module = await context.Modules.FindAsync(id);
            if (module == null)
                return NotFound();
            context.Modules.Remove(module);
            return NoContent();
        }
    }
}
