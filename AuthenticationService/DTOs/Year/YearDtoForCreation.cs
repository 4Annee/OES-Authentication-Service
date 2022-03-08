using System.ComponentModel.DataAnnotations;

namespace AuthenticationService.DTOs.Year
{
    public class YearDtoForCreation
    {
        [Required]
        public string StudyYear { get; set; }
    }
}
