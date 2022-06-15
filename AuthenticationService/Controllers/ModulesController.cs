using AuthenticationService.Data;
using AuthenticationService.DTOs.Module;
using AuthenticationService.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        /// Gets Modules Given the year ID
        /// </summary>
        [HttpGet("/Year/{year}")]
        public async Task<IActionResult> Get(Guid year)
        {
            return Ok( mapper.Map<List<ModuleDto>>(await context.Sections.Where(s=>s.YearId == year)
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
