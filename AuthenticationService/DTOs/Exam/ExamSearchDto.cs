using System.ComponentModel.DataAnnotations;

namespace AuthenticationService.DTOs.Exam
{
    public class ExamSearchDto
    {
        [Required]
        public string StudentId { get; set; }
        [Required]
        public Guid ExamId { get; set; }
    }
}
