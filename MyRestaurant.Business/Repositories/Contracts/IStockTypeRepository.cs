using MyRestaurant.Business.Dtos.V1;

namespace MyRestaurant.Business.Repositories.Contracts
{
    public interface IStockTypeRepository
    {
        Task<IEnumerable<GetStockTypeDto>> GetStockTypesAsync();
        Task<GetStockTypeDto> GetStockTypeAsync(int id);
        Task<GetStockTypeDto> CreateStockTypeAsync(CreateStockTypeDto stockTypeDto);
        Task<GetStockTypeDto> UpdateStockTypeAsync(int id, EditStockTypeDto stockTypeDto);
        Task DeleteStockTypeAsync(int id);
    }
}
