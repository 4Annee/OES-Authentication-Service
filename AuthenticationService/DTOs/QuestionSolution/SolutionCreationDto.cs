using System.ComponentModel.DataAnnotations;

namespace AuthenticationService.DTOs.QuestionSolution
{
    public class SolutionCreationDto
    {
        [Required]
        public Guid QtId { get; set; }
        [Required]
        public string Answer { get; set; }
    }
}
