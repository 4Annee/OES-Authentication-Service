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
        public string? StudentCardId { get; set; }
        [Required]
        public string CIDNumber { get; set; }
        [Required]
        public Guid? GroupID { get; set; }
    }
}
