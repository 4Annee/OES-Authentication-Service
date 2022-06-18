namespace AuthenticationService.Models
{
    public class QuestionSolution
    {
        public Guid Id { get; set; }
        public string Answer { get; set; }
        public Question Question { get; set; }
        public Guid QuestionId { get; set; }
    }
}
