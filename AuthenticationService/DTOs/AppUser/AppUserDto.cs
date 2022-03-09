namespace AuthenticationService.DTOs.AppUser
{
    public class AppUserDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string StudentCardId { get; set; }
        public Guid GroupId { get; set; }
    }
}
