using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class UserRole : IdentityUserRole
    {
        public User User { get; set; }
        public Role Role { get; set; }
    }
}
