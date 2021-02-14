using MyRestaurant.Business.Dtos.V1;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyRestaurant.Business.Repositories.Contracts
{
    public interface IStockItemRepository
    {
        Task<IEnumerable<GetStockItemDto>> GetStockItemsAsync();
        Task<StockItemEnvelop> GetStockItemByType(int typeId, int? limit, int? offset);
        Task<GetStockItemDto> GetStockItemAsync(long id);
        Task<GetStockItemDto> CreateStockItemAsync(CreateStockItemDto stockItemDto);
        Task<GetStockItemDto> UpdateStockItemAsync(long id, EditStockItemDto stockItemDto);
        Task DeleteStockItemAsync(long id);
    }
}
