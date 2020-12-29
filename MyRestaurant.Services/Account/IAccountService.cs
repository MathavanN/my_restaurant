using MyRestaurant.Models;
using System;
using System.Threading.Tasks;

namespace MyRestaurant.Services.Account
{
    public interface IAccountService
    {
        Task<string> GenerateAccessToken(User user);
        Task<RefreshToken> GenerateRefreshToken(Guid userId, string ipAddress);
        bool ValidateRefreshToken(string token);
    }
}
