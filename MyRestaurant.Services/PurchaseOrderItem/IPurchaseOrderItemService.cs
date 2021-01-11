using MyRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyRestaurant.Services
{
    public interface IPurchaseOrderItemService
    {
        Task AddPurchaseOrderItemAsync(PurchaseOrderItem orderItem);
        Task<IEnumerable<PurchaseOrderItem>> GetPurchaseOrderItemsAsync(Expression<Func<PurchaseOrderItem, bool>> expression);
        Task<PurchaseOrderItem> GetPurchaseOrderItemAsync(Expression<Func<PurchaseOrderItem, bool>> expression);
    }
}
