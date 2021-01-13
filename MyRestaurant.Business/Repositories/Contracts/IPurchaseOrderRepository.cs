using MyRestaurant.Business.Dtos.V1;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyRestaurant.Business.Repositories.Contracts
{
    public interface IPurchaseOrderRepository
    {
        Task<IEnumerable<GetPurchaseOrderDto>> GetPurchaseOrdersAsync();
        Task<GetPurchaseOrderDto> GetPurchaseOrderAsync(long id);
        Task<GetPurchaseOrderDto> CreatePurchaseOrderAsync(CreatePurchaseOrderDto purchaseOrderDto);
        Task UpdatePurchaseOrderAsync(long id, EditPurchaseOrderDto purchaseOrderDto);
        Task DeletePurchaseOrderAsync(long id);
        Task ApprovalPurchaseOrderAsync(long id, ApprovalPurchaseOrderDto purchaseOrderDto);
    }
}
