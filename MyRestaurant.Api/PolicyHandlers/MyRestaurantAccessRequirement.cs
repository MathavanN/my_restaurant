using Microsoft.AspNetCore.Authorization;

namespace MyRestaurant.Api.PolicyHandlers
{
    public class MyRestaurantAccessRequirement : IAuthorizationRequirement
    {
        public string Role { get; }
        public MyRestaurantAccessRequirement(string role)
        {
            Role = role;
        }
    }
}
