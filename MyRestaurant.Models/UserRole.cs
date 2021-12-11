using Microsoft.AspNetCore.Identity;

namespace MyRestaurant.Models
{
    public class UserRole : IdentityUserRole<Guid>
    {
        public UserRole()
        {
            User = default!;
            Role = default!;
        }

        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}
