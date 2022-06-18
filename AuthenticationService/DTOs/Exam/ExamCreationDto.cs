using System.ComponentModel.DataAnnotations;

namespace AuthenticationService.DTOs.Exam
{
    public class ExamCreationDto
    {
        [Required]
        public string ModuleId { get; set; }
        [Required]
        public string TeacherId { get; set; }
        [Required]
        public string AssessmentTitle { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        [Required]
        public Guid[] AssignedGroups { get; set; }
    }
}
