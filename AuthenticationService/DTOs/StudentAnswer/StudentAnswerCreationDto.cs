using System.ComponentModel.DataAnnotations;

namespace AuthenticationService.DTOs.StudentAnswer
{
    public class StudentAnswerCreationDto
    {
        [Required]
        public string StudentId { get; set; }
        [Required]
        public Guid ExamId { get; set; }
        [Required]
        public Guid QuestionId { get; set; }
        [Required]
        public string Answer { get; set; }
    }
}
