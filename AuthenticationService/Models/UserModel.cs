using Microsoft.AspNetCore.Identity;

namespace AuthenticationService.Models
{
    public class UserModel : IdentityUser
    {

        public string FullName { get; set; }
        public DateTime Birthday { get; set; }
        public string? StudentCardId { get; set; }
        public string CIDNumber { get; set; }

        // Many To One Relationship
        public Group? Group { get; set; }
        public Guid? GroupId { get; set; }
        public ICollection<StudyModule>? Teaching { get; set; }
    }
}
