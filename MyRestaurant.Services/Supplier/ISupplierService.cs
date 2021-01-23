using MyRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyRestaurant.Services
{
    public interface ISupplierService
    {
        IQueryable<Supplier> GetSuppliersAsync();
        Task<Supplier> GetSupplierAsync(Expression<Func<Supplier, bool>> expression);
        Task AddSupplierAsync(Supplier supplier);
        Task UpdateSupplierAsync(Supplier supplier);
        Task DeleteSupplierAsync(Supplier supplier);
    }
}
