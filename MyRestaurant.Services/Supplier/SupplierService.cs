using MyRestaurant.Core;
using MyRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyRestaurant.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly IMyRestaurantContext _context;
        public SupplierService(IMyRestaurantContext context)
        {
            _context = context;
        }
        public async Task AddSupplierAsync(Supplier supplier)
        {
            await _context.InsertAsync(supplier);
            await _context.CommitAsync();
        }

        public async Task DeleteSupplierAsync(Supplier supplier)
        {
            _context.Delete(supplier);
            await _context.CommitAsync();
        }

        public async Task<Supplier> GetSupplierAsync(Expression<Func<Supplier, bool>> expression) => await _context.GetFirstOrDefaultAsync(expression);

        public async Task<IEnumerable<Supplier>> GetSuppliersAsync() => await _context.GetAllAsync<Supplier>();

        public async Task UpdateSupplierAsync(Supplier supplier)
        {
            _context.Modify(supplier);
            await _context.CommitAsync();
        }
    }
}
