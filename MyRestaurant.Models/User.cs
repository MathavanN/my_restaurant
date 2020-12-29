using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace MyRestaurant.Models
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
    }
}
