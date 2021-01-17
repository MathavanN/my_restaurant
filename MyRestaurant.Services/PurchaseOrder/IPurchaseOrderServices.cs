using MyRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyRestaurant.Services
{
    public interface IPurchaseOrderServices
    {
        Task<IEnumerable<PurchaseOrder>> GetPurchaseOrdersAsync();
        Task<PurchaseOrder> GetPurchaseOrderAsync(Expression<Func<PurchaseOrder, bool>> expression);
        Task AddPurchaseOrderAsync(PurchaseOrder order);
        Task UpdatePurchaseOrderAsync(PurchaseOrder order);
        Task DeletePurchaseOrderAsync(PurchaseOrder order);
    }
}
