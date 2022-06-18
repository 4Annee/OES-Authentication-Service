namespace AuthenticationService.Models
{
    public class StudentAnswer
    {
        public Guid Id { get; set; }
        public string Answer { get; set; }
        public bool CorrectAnswer { get; set; }
        public double Score { get; set; }
        public StudentPassedExam PassedExam { get; set; }

    }
}
