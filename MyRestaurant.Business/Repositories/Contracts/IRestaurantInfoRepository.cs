using MyRestaurant.Business.Dtos.V1;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyRestaurant.Business.Repositories.Contracts
{
    public interface IRestaurantInfoRepository
    {
        Task<IEnumerable<GetRestaurantInfoDto>> GetRestaurantInfosAsync();
        Task<GetRestaurantInfoDto> GetRestaurantInfoAsync(int id);
        Task<GetRestaurantInfoDto> CreateRestaurantInfoAsync(CreateRestaurantInfoDto infoDto);
        //Task<GetRestaurantInfoDto> UpdateRestaurantInfoAsync(int id, EditRestaurantInfoeDto infoDto);
        //Task DeleteRestaurantInfoAsync(int id);
    }
}
