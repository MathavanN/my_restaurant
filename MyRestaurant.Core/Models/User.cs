using Microsoft.AspNetCore.Identity;
using System;

namespace MyRestaurant.Core.Models
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
