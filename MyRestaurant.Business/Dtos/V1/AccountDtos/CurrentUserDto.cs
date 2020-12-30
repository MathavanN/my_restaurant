using System;
using System.Collections.Generic;

namespace MyRestaurant.Business.Dtos.V1
{
    public class CurrentUserDto
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
