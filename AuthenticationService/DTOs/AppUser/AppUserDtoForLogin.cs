using System.ComponentModel.DataAnnotations;


namespace AuthenticationService.DTOs.AppUser
{
    public class AppUserDtoForLogin
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
