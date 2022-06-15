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
using Microsoft.AspNetCore.Authorization;

namespace AuthenticationService.Controllers
{
    [Route("api/study/[controller]")]
    [ApiController]
    [Authorize]
    public class StudyYearController : ControllerBase
    {
        private readonly IYearRepository yearRepo;

        public StudyYearController(IYearRepository yearRepo)
        {
            this.yearRepo = yearRepo;
        }


        /// <summary>
        /// Get Years List
        /// </summary>
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



        /// <summary>
        /// Add New Year
        /// </summary>
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult> PostYear(YearDtoForCreation year)
        {
            return Ok(await yearRepo.AddYear(year));
        }


        /// <summary>
        /// Delete A Year
        /// </summary>
        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteYear(Guid id)
        {
            await yearRepo.RemoveYear(id);
            return NoContent();
        }
    }
}
