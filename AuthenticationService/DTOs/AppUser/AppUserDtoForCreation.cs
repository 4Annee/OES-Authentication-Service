using System.ComponentModel.DataAnnotations;

namespace AuthenticationService.DTOs.AppUser
{
    public class AppUserDtoForCreation
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public DateTime Birthday { get; set; }
        [Required]
        public string CIDNumber { get; set; }
        public Guid? GroupID { get; set; }
    }
}
