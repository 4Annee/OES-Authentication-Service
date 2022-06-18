namespace AuthenticationService.DTOs.Exam
{
    public class ExamDtoForResult
    {
        public string AssessmentTitle { get; set; }
        public List<StudentResult> Results { get; set; }
    }

    public class StudentResult
    {
        public string FullName { get; set; }
        public double FinalScore { get; set; }
    }
}
