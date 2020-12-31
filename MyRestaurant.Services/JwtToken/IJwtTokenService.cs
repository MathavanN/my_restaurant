using MyRestaurant.Models;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyRestaurant.Services
{
    public interface IJwtTokenService
    {
        Task<string> GenerateAccessToken(User user);
        Task<RefreshToken> GenerateRefreshToken(Guid userId, string ipAddress);
        bool ValidateRefreshToken(string token);
        Task<RefreshToken> GetRefreshTokenAsync(Expression<Func<RefreshToken, bool>> expression);
        Task UpdateRefreshTokenAsync(RefreshToken token);
    }
}
