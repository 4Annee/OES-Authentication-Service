namespace AuthenticationService.Models
{
    public class ExamGroup
    {
        public Group Group { get; set; }
        public Guid GroupId { get; set; }
        public Exam Exam { get; set; }
        public Guid ExamId { get; set; }
    }
}
