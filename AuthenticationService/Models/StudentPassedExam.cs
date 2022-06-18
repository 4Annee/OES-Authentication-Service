namespace AuthenticationService.Models
{
    public class StudentPassedExam
    {
        public UserModel Student { get; set; }
        public string StudentId { get; set; }
        public Exam Exam { get; set; }
        public Guid ExamId { get; set; }
        public double FinalScore { get; set; }
        public bool Corrected { get; set; }
        public List<StudentAnswer> StudentAnswers { get; set; }
    }
}
