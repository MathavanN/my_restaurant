using MyRestaurant.Business.Dtos.V1;
using System.Threading.Tasks;

namespace MyRestaurant.Business.Repositories.Contracts
{
    public interface IAccountRepository
    {
        Task<RegisterResultDto> RegisterAdminAsync(RegisterAdminDto registerDto);
        Task<RegisterResultDto> RegisterNormalAsync(RegisterNormalDto registerDto);
        Task<TokenResultDto> LoginAsync(LoginDto loginDto, string ipAddress);
        Task RevokeToken(RevokeDto revokeDto, string ipAddress);
        Task<TokenResultDto> RefreshToken(RefreshDto refreshDto, string ipAddress);
        CurrentUserDto GetCurrentUser();
    }
}
