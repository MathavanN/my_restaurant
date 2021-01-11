using MyRestaurant.Business.Dtos.V1;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyRestaurant.Business.Repositories.Contracts
{
    public interface IPurchaseOrderItemRepository
    {
        Task<IEnumerable<GetPurchaseOrderItemDto>> GetPurchaseOrderItemsAsync(long orderId);
        Task<GetPurchaseOrderItemDto> CreatePurchaseOrderItemAsync(CreatePurchaseOrderItemDto purchaseOrderItemDto);
        Task<GetPurchaseOrderItemDto> GetPurchaseOrderItemAsync(long id);
    }
}
