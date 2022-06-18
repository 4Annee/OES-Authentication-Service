namespace AuthenticationService.Models
{
    public class Question
    {
        public Guid Id { get; set; }
        public string question { get; set; }
        public QuestionType qtType { get; set; }
        public string Description { get; set; }
        public double score { get; set; }
        public List<Choice> Choices{ get; set; }
        // Solution Reference
        public QuestionSolution Solution { get; set; }
        // Exam Reference
        public Exam Exam { get; set; }
        public Guid ExamId { get; set; }
    }
    public enum QuestionType
    {
        QCM,
        Text,
        Code
    }
}
