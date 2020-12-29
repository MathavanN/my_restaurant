using MyRestaurant.Business.Dtos.V1;
using System.Threading.Tasks;

namespace MyRestaurant.Business.Repositories.Contracts
{
    public interface IAccountRepository
    {
        Task<RegisterResultDto> RegisterAdminAsync(RegisterDto registerDto);
        Task<TokenResultDto> LoginAsync(LoginDto loginDto, string ipAddress);
    }
}
