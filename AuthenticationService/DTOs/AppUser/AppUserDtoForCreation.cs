namespace AuthenticationService.DTOs.AppUser
{
    public class AppUserDtoForCreation
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public DateTime Birthday { get; set; }
        public string? StudentID { get; set; }
        public string CIDNumber { get; set; }
        public Guid? GroupID { get; set; }
    }
}
