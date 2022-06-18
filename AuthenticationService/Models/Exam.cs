namespace AuthenticationService.Models
{
    public class Exam
    {
        public Guid Id { get; set; }
        public string AssessmentTitle { get; set; }
        // Teacher Reference
        public UserModel Teacher { get; set; }
        public string TeacherId { get; set; }
        // Module Reference
        public StudyModule StudyModule { get; set; }
        public string StudyModuleId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool Published { get; set; }
        public List<ExamGroup> AssignedGroups { get; set; }
        public List<Question> Questions { get; set; }
        public List<StudentPassedExam> StudentsSubmitions { get; set; }
    }
}
