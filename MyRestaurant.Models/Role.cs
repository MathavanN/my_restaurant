using Microsoft.AspNetCore.Identity;
using System;

namespace MyRestaurant.Models
{
    public class Role : IdentityRole<Guid>
    {
    }

    public enum Roles
    {
        SuperAdmin,
        Admin,
        Report,
        Normal
    }
}
