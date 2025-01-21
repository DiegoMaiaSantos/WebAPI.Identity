using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class Role : IdentityRole
    {
        public List<UserRole> UserRoles { get; set; }
    }
}
