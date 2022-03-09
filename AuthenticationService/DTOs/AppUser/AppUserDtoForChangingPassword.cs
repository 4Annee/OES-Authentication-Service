namespace AuthenticationService.DTOs.AppUser
{
    public class AppUserDtoForChangingPassword
    {
        public string Email { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
