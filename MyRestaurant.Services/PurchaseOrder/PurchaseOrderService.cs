using MyRestaurant.Core;
using MyRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyRestaurant.Services
{
    public class PurchaseOrderService : IPurchaseOrderService
    {
        private readonly IMyRestaurantContext _context;
        public PurchaseOrderService(IMyRestaurantContext context)
        {
            _context = context;
        }

        public async Task AddPurchaseOrderAsync(PurchaseOrder order)
        {
            await _context.InsertAsync(order);
            await _context.CommitAsync();
        }

        public async Task DeletePurchaseOrderAsync(PurchaseOrder order)
        {
            _context.Delete(order);
            await _context.CommitAsync();
        }

        public async Task<PurchaseOrder> GetPurchaseOrderAsync(Expression<Func<PurchaseOrder, bool>> expression) => await _context.GetFirstOrDefaultAsync(expression);

        public async Task<IEnumerable<PurchaseOrder>> GetPurchaseOrdersAsync(Expression<Func<PurchaseOrder, bool>> expression = null) => await _context.GetAllAsync(expression);

        public async Task UpdatePurchaseOrderAsync(PurchaseOrder order)
        {
            _context.Modify(order);
            await _context.CommitAsync();
        }
    }
}
