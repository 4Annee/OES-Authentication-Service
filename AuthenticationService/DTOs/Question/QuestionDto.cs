using AuthenticationService.Models;

namespace AuthenticationService.DTOs.Question
{
    public class QuestionDto
    {
        public Guid Id { get; set; }
        public string question { get; set; }
        public QuestionType qtType { get; set; }
        public string Description { get; set; }
        public double score { get; set; }
        public List<string>? Choices { get; set; }
    }
}
