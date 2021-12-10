using MyRestaurant.Business.Dtos.V1;

namespace MyRestaurant.Business.Repositories.Contracts
{
    public interface IPurchaseOrderRepository
    {
        Task<IEnumerable<GetPurchaseOrderDto>> GetPurchaseOrdersAsync();
        Task<IEnumerable<GetPurchaseOrderDto>> GetPurchaseOrdersAllowToCreateGRN();
        Task<GetPurchaseOrderDto> GetPurchaseOrderAsync(long id);
        Task<GetPurchaseOrderDto> CreatePurchaseOrderAsync(CreatePurchaseOrderDto purchaseOrderDto);
        Task<GetPurchaseOrderDto> UpdatePurchaseOrderAsync(long id, EditPurchaseOrderDto purchaseOrderDto);
        Task DeletePurchaseOrderAsync(long id);
        Task<GetPurchaseOrderDto> ApprovalPurchaseOrderAsync(long id, ApprovalPurchaseOrderDto purchaseOrderDto);
    }
}
