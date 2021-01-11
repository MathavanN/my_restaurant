using MyRestaurant.Core;
using MyRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyRestaurant.Services
{
    public class PurchaseOrderItemService : IPurchaseOrderItemService
    {
        private readonly IMyRestaurantContext _context;
        public PurchaseOrderItemService(IMyRestaurantContext context)
        {
            _context = context;
        }

        public async Task AddPurchaseOrderItemAsync(PurchaseOrderItem orderItem)
        {
            await _context.InsertAsync(orderItem);
            await _context.CommitAsync();
        }

        public async Task<IEnumerable<PurchaseOrderItem>> GetPurchaseOrderItemsAsync(Expression<Func<PurchaseOrderItem, bool>> expression) => await _context.GetAllAsync(expression);

        public async Task<PurchaseOrderItem> GetPurchaseOrderItemAsync(Expression<Func<PurchaseOrderItem, bool>> expression) => await _context.GetFirstOrDefaultAsync(expression);
    }
}
