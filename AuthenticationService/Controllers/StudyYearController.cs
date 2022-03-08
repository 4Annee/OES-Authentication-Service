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
using AuthenticationService.DTOs.Year;

namespace AuthenticationService.Controllers
{
    [Route("api/study/[controller]")]
    [ApiController]
    public class StudyYearController : ControllerBase
    {
        private readonly IYearRepository yearRepo;

        public StudyYearController(IYearRepository yearRepo)
        {
            this.yearRepo = yearRepo;
        }

        // GET: api/StudyYear
        [HttpGet]
        public ActionResult<IEnumerable<YearDto>> GetYears()
        {
            var Years = yearRepo.GetAllYears();
            if (Years == null)
            {
                return NotFound();
            }
            return Ok(Years);
        }


        // POST: api/StudyYear
        [HttpPost]
        public ActionResult PostYear(YearDtoForCreation year)
        {
            return Ok(yearRepo.AddYear(year));
        }

        // DELETE: api/StudyYear/5
        [HttpDelete("{id}")]
        public IActionResult DeleteYear(Guid id)
        {
            yearRepo.RemoveYear(id);
            return NoContent();
        }
    }
}
