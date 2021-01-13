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
            _context.Create(orderItem);
            await _context.CommitAsync();
        }

        public async Task<IEnumerable<PurchaseOrderItem>> GetPurchaseOrderItemsAsync(Expression<Func<PurchaseOrderItem, bool>> expression) => await _context.GetAllAsync(expression);

        public async Task<PurchaseOrderItem> GetPurchaseOrderItemAsync(Expression<Func<PurchaseOrderItem, bool>> expression) => await _context.GetFirstOrDefaultAsync(expression);

        public async Task UpdatePurchaseOrderItemAsync(PurchaseOrderItem orderItem)
        {
            _context.Modify(orderItem);
            await _context.CommitAsync();
        }
        public async Task DeletePurchaseOrderItemAsync(PurchaseOrderItem orderItem)
        {
            _context.Delete(orderItem);
            await _context.CommitAsync();
        }
    }
}
