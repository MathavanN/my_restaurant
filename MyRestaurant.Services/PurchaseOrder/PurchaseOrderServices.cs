using MyRestaurant.Core;
using MyRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyRestaurant.Services
{
    public class PurchaseOrderServices : IPurchaseOrderServices
    {
        private readonly IMyRestaurantContext _context;
        public PurchaseOrderServices(IMyRestaurantContext context)
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

        public async Task<IEnumerable<PurchaseOrder>> GetPurchaseOrdersAsync() => await _context.GetAllAsync<PurchaseOrder>();

        public async Task UpdatePurchaseOrderAsync(PurchaseOrder order)
        {
            _context.Modify(order);
            await _context.CommitAsync();
        }
    }
}
