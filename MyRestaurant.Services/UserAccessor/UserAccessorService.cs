using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;

namespace MyRestaurant.Services
{
    public class UserAccessorService : IUserAccessorService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserAccessorService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public CurrentUser GetCurrentUser()
        {
            var userClaims = _httpContextAccessor.HttpContext.User?.Claims;
            return new CurrentUser
            {
                UserId = new Guid(userClaims?.FirstOrDefault(x => x.Type == "id")?.Value),
                Email = userClaims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value,
                FirstName = userClaims?.FirstOrDefault(x => x.Type == "firstName")?.Value,
                LastName = userClaims?.FirstOrDefault(x => x.Type == "lastName")?.Value,
                Roles = userClaims?.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value)
            };
            
        }
    }
}
