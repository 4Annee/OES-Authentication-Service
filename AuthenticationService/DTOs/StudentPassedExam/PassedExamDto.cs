using System.ComponentModel.DataAnnotations;

namespace AuthenticationService.DTOs.StudentPassedExam
{
    public class PassedExamDto
    {
        [Required]
        public string StudentId { get; set; }
        [Required]
        public Guid ExamId { get; set; }
    }
}
