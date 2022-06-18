using System.ComponentModel.DataAnnotations;

namespace AuthenticationService.DTOs.Module
{
    public class ModuleDtoForCreation
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
