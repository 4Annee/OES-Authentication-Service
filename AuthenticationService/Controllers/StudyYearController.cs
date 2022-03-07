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

namespace AuthenticationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudyYearController : ControllerBase
    {
        private readonly UserServiceContext _context;

        public StudyYearController(UserServiceContext context)
        {
            _context = context;
        }

        // GET: api/StudyYear
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Year>>> GetYear()
        {
            return await _context.Years.ToListAsync();
        }

        // GET: api/StudyYear/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Year>> GetYear(Guid id)
        {
            var year = await _context.Years.FindAsync(id);

            if (year == null)
            {
                return NotFound();
            }

            return year;
        }

        // PUT: api/StudyYear/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutYear(Guid id, Year year)
        {
            if (id != year.Id)
            {
                return BadRequest();
            }

            _context.Entry(year).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!YearExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/StudyYear
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Year>> PostYear(Year year)
        {
            _context.Years.Add(year);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetYear", new { id = year.Id }, year);
        }

        // DELETE: api/StudyYear/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteYear(Guid id)
        {
            var year = await _context.Years.FindAsync(id);
            if (year == null)
            {
                return NotFound();
            }

            _context.Years.Remove(year);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool YearExists(Guid id)
        {
            return _context.Years.Any(e => e.Id == id);
        }
    }
}
