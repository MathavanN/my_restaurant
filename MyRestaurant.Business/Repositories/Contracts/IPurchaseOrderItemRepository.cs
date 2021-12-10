using MyRestaurant.Business.Dtos.V1;

namespace MyRestaurant.Business.Repositories.Contracts
{
    public interface IPurchaseOrderItemRepository
    {
        Task<IEnumerable<GetPurchaseOrderItemDto>> GetPurchaseOrderItemsAsync(long orderId);
        Task<GetPurchaseOrderItemDto> CreatePurchaseOrderItemAsync(CreatePurchaseOrderItemDto purchaseOrderItemDto);
        Task<GetPurchaseOrderItemDto> GetPurchaseOrderItemAsync(long id);
        Task<GetPurchaseOrderItemDto> UpdatePurchaseOrderItemAsync(long id, EditPurchaseOrderItemDto purchaseOrderItemDto);
        Task DeletePurchaseOrderItemAsync(long id);
    }
}
