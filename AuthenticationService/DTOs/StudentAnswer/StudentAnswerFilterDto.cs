using System.ComponentModel.DataAnnotations;

namespace AuthenticationService.DTOs.StudentAnswer
{
    public class StudentAnswerFilterDto
    {
        [Required]
        public string IdStudent { get; set; }
        [Required]
        public Guid ExamId { get; set; }
    }
}
