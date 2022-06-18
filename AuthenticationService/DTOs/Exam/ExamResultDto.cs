namespace AuthenticationService.DTOs.Exam
{
    public class ExamResultDto
    {
        public string AssessmentTitle { get; set; }
        public DateTime PassingDate { get; set; }
        public double FinalScore { get; set; }
        public bool Corrected { get; set; }
    }
}
