using AuthenticationService.DTOs.Question;

namespace AuthenticationService.DTOs.Exam
{
    public class ExamDto
    {
        public Guid Id { get; set; }
        public string ModuleId { get; set; }
        public string AssessmentTitle { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Guid[] AssignedGroups { get; set; }
        public List<QuestionDto>? Questions { get; set; }
    }
}
