using AuthenticationService.Models;
using System.ComponentModel.DataAnnotations;

namespace AuthenticationService.DTOs.Question
{
    public class QuestionCreationDto
    {
        [Required]
        public Guid ExamId { get; set; }
        [Required]
        public string question { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public QuestionType qtType{ get; set; }
        [Required]
        public double Score { get; set; }
        [Required]
        public String[]? Choices { get; set; }
    }
}
