using MyRestaurant.Business.Dtos.V1;

namespace MyRestaurant.Business.Repositories.Contracts
{
    public interface IRestaurantInfoRepository
    {
        Task<IEnumerable<GetRestaurantInfoDto>> GetRestaurantInfosAsync();
        Task<GetRestaurantInfoDto> GetRestaurantInfoAsync(int id);
        Task<GetRestaurantInfoDto> CreateRestaurantInfoAsync(CreateRestaurantInfoDto infoDto);
    }
}
