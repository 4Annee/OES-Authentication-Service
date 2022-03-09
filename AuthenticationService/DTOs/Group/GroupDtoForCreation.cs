using System.ComponentModel.DataAnnotations;

namespace AuthenticationService.DTOs.Group
{
    public class GroupDtoForCreation
    {
        [Required]
        public string StudyGroup { get; set; }
        [Required]
        public Guid SectionId { get; set; }
    }
}
