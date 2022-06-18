using System.ComponentModel.DataAnnotations;

namespace AuthenticationService.DTOs.StudentAnswer
{
    public class StudentAnswerScoreDto
    {
        [Required]
        public Guid IdAnswer { get; set; }
        [Required]
        public bool IsCorrectAnswer { get; set; }
        [Required]
        public double Score { get; set; }
    }
}
