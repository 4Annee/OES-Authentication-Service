using System.ComponentModel.DataAnnotations;

namespace AuthenticationService.DTOs.Exam
{
    public class ExamUpdateDto
    {
        [Required]
        public string AssessmentTitle { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool Published { get; set; }
        public Guid[] AssignedGroups { get; set; }
    }
}
