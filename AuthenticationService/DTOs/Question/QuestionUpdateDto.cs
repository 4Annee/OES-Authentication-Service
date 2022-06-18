using AuthenticationService.Models;
using System.ComponentModel.DataAnnotations;

namespace AuthenticationService.DTOs.Question
{
    public class QuestionUpdateDto
    {
        public string Question { get; set; }
        public string Description { get; set; }
        public QuestionType qtType { get; set; }
        public double Score { get; set; }
        public String[]? Choices { get; set; }
    }
}
