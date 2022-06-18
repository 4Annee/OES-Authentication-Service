using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AuthenticationService.Data;
using AuthenticationService.Models;
using AuthenticationService.DTOs.Exam;
using AutoMapper;
using AuthenticationService.DTOs.Question;

namespace AuthenticationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly UserServiceContext _context;
        private readonly IMapper mapper;

        public ExamController(UserServiceContext context,IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }



        /// <summary>
        /// Get Score Of A Single Students In An Exam
        /// </summary>
        [HttpGet("/Score")]
        public async Task<ActionResult<IEnumerable<ExamResultDto>>> GetExamScore([FromBody]ExamSearchDto dto)
        {
            var examresult = await _context.StudentPassedExams.Include(s => s.Exam).FirstOrDefaultAsync(u=>u.StudentId == dto.StudentId && u.ExamId == dto.ExamId);
            if(examresult == null)
            {
                NotFound();
            }
            return Ok(mapper.Map<ExamResultDto>(examresult));
        }

        /// <summary>
        /// Get Score Of All Student
        /// </summary>   
        [HttpGet("/Score/{idExam}")]
        public async Task<ActionResult<IEnumerable<ExamDtoForResult>>> GetExamScores(Guid idExam)
        {
            var examresults = await _context.StudentPassedExams.Include(u=>u.Student).Include(s => s.Exam).Where(u => u.ExamId == idExam).ToListAsync();
            var examresult = mapper.Map<ExamDtoForResult>(examresults[0].Exam);
            examresult.Results = mapper.Map<List<StudentResult>>(examresults);
            if (examresults == null)
            {
                NotFound();
            }
            return Ok(examresult);
        }

        /// <summary>
        /// Get Exams Scheduled For A Student /Schedule/
        /// </summary>
        [HttpGet("/Schedule/{idStud}")]
        public async Task<IActionResult> getUpcomingExams(string idStud)
        {
            var user = await _context.Users.FindAsync(idStud);
            if (user == null)
                return NotFound();
            var exams = await _context.Exams.Include(e=>e.AssignedGroups).Where(e=>e.StartTime >= DateTime.Now && e.AssignedGroups.Any(g=>g.GroupId == user.GroupId)).ToListAsync();
            if (exams == null)
                return NotFound();
            return Ok(mapper.Map<ExamDto>(exams));
        }


        /// <summary>
        /// Get Passed Exams By Student, Get Last 10 /Passed/
        /// </summary>
        [HttpGet("/Passed/{idStudent}")]
        public async Task<ActionResult<IEnumerable<ExamResultDto>>> GetStudentPreviousScore(string idStudent)
        {
            var examresults = await _context.StudentPassedExams.Include(s => s.Exam).Where(u => u.StudentId == idStudent).ToListAsync();
            if (examresults == null)
            {
                NotFound();
            }
            var results = mapper.Map<List<ExamResultDto>>(examresults);
            return Ok(results);
        }


        /// <summary>
        /// Get Exam And Its Questions /Details/{idExam}
        /// </summary>
        [HttpGet("/Details/{idExam}")]
        public async Task<ActionResult<ExamDto>> getExamWithQuestions(Guid idExam)
        {
            var exam = await _context.Exams.Include(e => e.Questions).Include(e=>e.AssignedGroups).FirstOrDefaultAsync(e => e.Id == idExam);
            if (exam == null)
                return NotFound();
            var examres = mapper.Map<ExamDto>(exam);
            examres.AssignedGroups = exam.AssignedGroups.Select(s => s.GroupId).ToArray();
            if(exam.Published)
                examres.Questions = mapper.Map<List<QuestionDto>>(exam.Questions);
            return Ok(examres);
        }

        /// <summary>
        /// Create New Exam /
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> createExam(ExamCreationDto dto)
        {
            var exam = mapper.Map<Exam>(dto);
            if (exam == null)
                return BadRequest();
            exam = (await _context.AddAsync(exam)).Entity;
            exam.AssignedGroups = new List<ExamGroup>();
            foreach (var item in dto.AssignedGroups)
            {
                exam.AssignedGroups.Add(new ExamGroup() {GroupId = item,ExamId = exam.Id });
            };
            return Ok(await _context.SaveChangesAsync());
        }

        /// <summary>
        /// Publish The Exam /Publish/{id}
        /// </summary>
        [HttpPost("/Publish/{idExam}")]
        public async Task<IActionResult> publishExam(Guid idExam)
        {
            var exam = await _context.Exams.FindAsync(idExam);
            if (exam == null)
                return NotFound();
            exam.Published = true;
            await _context.SaveChangesAsync();
            return Ok();
        }

        /// <summary>
        /// Update Exam  PUT /Details/{idExam}
        /// </summary>
        [HttpPut("/Details/{idExam}")]
        public async Task<IActionResult> updateExam(Guid idExam,ExamUpdateDto dto)
        {
            var exam = await _context.Exams.FindAsync(idExam);
            if (exam == null)
                return NotFound();
            var exmup = mapper.Map<Exam>(dto);
            mapper.Map(exmup, exam);
            await _context.SaveChangesAsync();
            return Ok();
        }


        /// <summary>
        /// Remove An Exam /Delete/{idExam}
        /// </summary>



        /// <summary>
        /// Add Question To An Exam POST /Question
        /// </summary>
        [HttpPost("/Question")]
        public async Task<IActionResult> addQuestionToExam(QuestionCreationDto dto)
        {
            var qt = mapper.Map<Question>(dto);
            if (qt == null)
                return BadRequest();
            qt.Choices = new();
            if(dto.Choices != null)
                foreach (var item in dto.Choices)
                {
                    qt.Choices.Add(new Choice() {choice=item});
                }
            var res = await _context.Question.AddAsync(qt);
            await _context.SaveChangesAsync();
            return Ok(res.Entity);
        }


        /// <summary>
        /// Update Question Details PUT /Question/{id}
        /// </summary>
        [HttpPut("/Question/{idQt}")]
        public async Task<IActionResult> updateQuestion(Guid idQt, QuestionUpdateDto dto)
        {
            var question = await _context.Question.FindAsync(idQt);
            if (question == null)
                return NotFound();
            var qtup = mapper.Map<Exam>(dto);
            mapper.Map(qtup, question);
            await _context.SaveChangesAsync();
            return Ok();
        }
        /// <summary>
        /// Delete Question /Question/{id}
        /// </summary>


        /// <summary>
        /// Add Solution To Question 
        /// </summary>


        /// <summary>
        /// Update Solution
        /// </summary>







        // GET: api/Exams
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Exam>>> GetExams()
        {
          if (_context.Exams == null)
          {
              return NotFound();
          }
            return await _context.Exams.ToListAsync();
        }

        // GET: api/Exams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Exam>> GetExam(Guid id)
        {
          if (_context.Exams == null)
          {
              return NotFound();
          }
            var exam = await _context.Exams.FindAsync(id);

            if (exam == null)
            {
                return NotFound();
            }

            return exam;
        }

        // PUT: api/Exams/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExam(Guid id, Exam exam)
        {
            if (id != exam.Id)
            {
                return BadRequest();
            }

            _context.Entry(exam).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamExists(id))
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

        // POST: api/Exams
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Exam>> PostExam(Exam exam)
        {
          if (_context.Exams == null)
          {
              return Problem("Entity set 'UserServiceContext.Exams'  is null.");
          }
            _context.Exams.Add(exam);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExam", new { id = exam.Id }, exam);
        }

        // DELETE: api/Exams/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExam(Guid id)
        {
            if (_context.Exams == null)
            {
                return NotFound();
            }
            var exam = await _context.Exams.FindAsync(id);
            if (exam == null)
            {
                return NotFound();
            }

            _context.Exams.Remove(exam);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExamExists(Guid id)
        {
            return (_context.Exams?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
