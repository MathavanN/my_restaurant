using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

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
