using System.ComponentModel.DataAnnotations;

namespace AuthenticationService.DTOs.Module
{
    public class ModuleDTOAssignSection
    {
        [Required]
        public Guid SectionId { get; set; }
        [Required]
        public string ModuleId { get; set; }
    }
}
