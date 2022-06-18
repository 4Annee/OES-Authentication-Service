using AuthenticationService.Data;
using AuthenticationService.DTOs.StudentAnswer;
using AuthenticationService.DTOs.StudentPassedExam;
using AuthenticationService.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswersController : ControllerBase
    {
        private readonly UserServiceContext _context;
        private readonly IMapper mapper;

        public AnswersController(UserServiceContext _context,IMapper mapper)
        {
            this._context = _context;
            this.mapper = mapper;
        }

        /// <summary>
        /// Get Answers Of A Student - @Body {idExam, idStud} /
        /// </summary>
        [HttpGet("/Exam")]
        public async Task<ActionResult<List<StudentAnswerDto>>> getStudentAnswers(StudentAnswerFilterDto key)
        {
            var answers = await _context.StudentPassedExams.Include(o => o.StudentAnswers)
                .FirstOrDefaultAsync(sa => sa.StudentId == key.IdStudent && sa.ExamId == key.ExamId);
            if(answers == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<List<StudentAnswerDto>>(answers.StudentAnswers));
        }

        /// <summary>
        /// Student Passed An Exam PUT /Pass 
        /// </summary>
        [HttpPost("/Pass")]
        public async Task<IActionResult> studentPassedExam(PassedExamDto dto)
        {
            await _context.StudentPassedExams.AddAsync(new StudentPassedExam() {
                ExamId = dto.ExamId,
                Corrected = false,
                FinalScore = 0.0,
                StudentId = dto.StudentId
            });
            return Ok(await _context.SaveChangesAsync());
        }


        /// <summary>
        /// Answer Submit /Submit
        /// </summary>
        [HttpPost("/Submit")]
        public async Task<IActionResult> submitAnswer(StudentAnswerCreationDto answerDto)
        {
            var passed = await _context.StudentPassedExams.Include(u => u.StudentAnswers)
                .FirstOrDefaultAsync(u => u.ExamId == answerDto.ExamId && u.StudentId == answerDto.StudentId);
            if(passed == null)
            {
                return NotFound();
            }
            var answer = mapper.Map<StudentAnswer>(answerDto);
            //! TODO : Score The Answer If it is Correct or Not
            passed.StudentAnswers.Add(answer);
            return Ok(_context.SaveChangesAsync());
        }

        /// <summary>
        /// Give A Score To An Answer /Score
        /// </summary>
        [HttpPost("/Score")]
        public async Task<IActionResult> scoreAnswer(StudentAnswerScoreDto dto) {
            var answer = await _context.StudentAnswers.FindAsync(dto.IdAnswer);
            if (answer == null)
                return NotFound();
            answer.CorrectAnswer = dto.IsCorrectAnswer;
            answer.Score = dto.Score;
            return Ok(await _context.SaveChangesAsync());
        }


        /// <summary>
        /// Calculate Students Final Score /CalculateFinalSore
        /// </summary>
        [HttpPost("/CalculateScore")]
        public async Task<IActionResult> CalculateScore(StudentAnswerFilterDto filter)
        {
            var submittion = await _context.StudentPassedExams.Include(ps => ps.StudentAnswers).FirstOrDefaultAsync(u=>u.StudentId == filter.IdStudent && u.ExamId == filter.ExamId);
            if (submittion == null)
                return NotFound();
            submittion.FinalScore = submittion.StudentAnswers.Sum(o => o.Score);
            submittion.Corrected = true;
            return Ok(await _context.SaveChangesAsync());
        }
    }
}
