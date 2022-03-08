using System.ComponentModel.DataAnnotations;

namespace AuthenticationService.DTOs.Section
{
    public class SectionDtoForCreation
    {
        [Required]
        public string StudySection { get; set; }
    }
}
