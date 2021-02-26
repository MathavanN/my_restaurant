using MyRestaurant.Core;
using MyRestaurant.Models;
using MyRestaurant.Services.Common;
using System;
using System.Linq;
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
        public async Task<Supplier> AddSupplierAsync(Supplier supplier)
        {
            _context.Create(supplier);
            await _context.CommitAsync();
            return supplier;
        }

        public async Task DeleteSupplierAsync(Supplier supplier)
        {
            _context.Delete(supplier);
            await _context.CommitAsync();
        }

        public async Task<Supplier> GetSupplierAsync(Expression<Func<Supplier, bool>> expression) => await _context.GetFirstOrDefaultAsync(expression);

        public async Task<CollectionEnvelop<Supplier>> GetSuppliersAsync(string name, string city, string contactPerson, int page, int itemsPerPage) {
            var suppliers = await _context.GetAllAsync<Supplier>();

            if (!string.IsNullOrWhiteSpace(name))
                suppliers = suppliers.Where(d => d.Name.Contains(name, StringComparison.InvariantCultureIgnoreCase));

            if (!string.IsNullOrWhiteSpace(city))
                suppliers = suppliers.Where(d => d.City.Equals(city, StringComparison.InvariantCultureIgnoreCase));

            if (!string.IsNullOrWhiteSpace(contactPerson))
                suppliers = suppliers.Where(d => d.ContactPerson.Equals(contactPerson, StringComparison.InvariantCultureIgnoreCase));

            
            var toSkip = page * itemsPerPage;
            return new CollectionEnvelop<Supplier>
            {
                TotalItems = suppliers.Count(),
                ItemsPerPage = itemsPerPage,
                Items = page == 0 ? suppliers.OrderBy(d => d.Name).AsQueryable().Take(itemsPerPage) :
                                    suppliers.OrderBy(d => d.Name).AsQueryable().Skip(toSkip).Take(itemsPerPage),
            };
        }

        public async Task UpdateSupplierAsync(Supplier supplier)
        {
            _context.Modify(supplier);
            await _context.CommitAsync();
        }
    }
}
