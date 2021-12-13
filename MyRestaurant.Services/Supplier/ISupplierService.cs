using MyRestaurant.Models;
using MyRestaurant.Services.Common;
using System.Linq.Expressions;

namespace MyRestaurant.Services
{
    public interface ISupplierService
    {
        Task<CollectionEnvelop<Supplier>> GetSuppliersAsync(string name, string city, string contactPerson, int page, int itemsPerPage);
        Task<Supplier?> GetSupplierAsync(Expression<Func<Supplier, bool>> expression);
        Task<Supplier> AddSupplierAsync(Supplier supplier);
        Task UpdateSupplierAsync(Supplier supplier);
        Task DeleteSupplierAsync(Supplier supplier);
    }
}
