using Microsoft.AspNetCore.Identity;

namespace MyRestaurant.Models
{
    public class Role : IdentityRole<Guid>
    {
        public Role()
        {
            UserRoles = new HashSet<UserRole>();
        }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }

    public enum Roles
    {
        SuperAdmin,
        Admin,
        Report,
        Normal
    }
}
