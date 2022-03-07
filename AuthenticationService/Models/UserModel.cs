using Microsoft.AspNetCore.Identity;

namespace AuthenticationService.Models
{
    public class UserModel : IdentityUser
    {
        // Many To One Relationship
        public Group Group { get; set; }
        public Guid GroupId { get; set; }

    }
}
