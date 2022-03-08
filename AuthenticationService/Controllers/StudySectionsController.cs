#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AuthenticationService.Data;
using AuthenticationService.Models;
using AuthenticationService.Repositories;
using AuthenticationService.DTOs.Section;

namespace AuthenticationService.Controllers
{
    [Route("api/study/[controller]")]
    [ApiController]
    public class StudySectionsController : ControllerBase
    {
        private readonly ISectionRepository repo;

        public StudySectionsController(ISectionRepository repo)
        {
            this.repo = repo;
        }
        // GET: api/StudySections/5
        [HttpGet("{id}")]
        public ActionResult<Section> GetSections(Guid id)
        {
            var sections = repo.GetYearSections(id);
            if (sections == null)
            {
                return NotFound();
            }
            return Ok(sections);
        }


        // POST: api/StudySections
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Section> PostSection(SectionDtoForCreation section)
        {
            return Ok(repo.AddSection(section));
        }

        // DELETE: api/StudySections/5
        [HttpDelete("{id}")]
        public IActionResult DeleteSection(Guid id)
        {
            repo.RemoveSection(id);
            return NoContent();
        }

    }
}
