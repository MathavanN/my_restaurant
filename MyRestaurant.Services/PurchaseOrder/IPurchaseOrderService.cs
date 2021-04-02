using MyRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyRestaurant.Services
{
    public interface IPurchaseOrderService
    {
        Task<IEnumerable<PurchaseOrder>> GetPurchaseOrdersAsync(Expression<Func<PurchaseOrder, bool>> expression = null);
        Task<PurchaseOrder> GetPurchaseOrderAsync(Expression<Func<PurchaseOrder, bool>> expression);
        Task<PurchaseOrder> AddPurchaseOrderAsync(PurchaseOrder order);
        Task UpdatePurchaseOrderAsync(PurchaseOrder order);
        Task DeletePurchaseOrderAsync(PurchaseOrder order);
    }
}
