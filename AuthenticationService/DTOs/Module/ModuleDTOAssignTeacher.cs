using System.ComponentModel.DataAnnotations;

namespace AuthenticationService.DTOs.Module
{
    public class ModuleDTOAssignTeacher
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string ModuleId { get; set; }
    }
}
