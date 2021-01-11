using MyRestaurant.Business.Dtos.V1;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyRestaurant.Business.Repositories.Contracts
{
    public interface ISupplierRepository
    {
        Task<IEnumerable<GetSupplierDto>> GetSuppliersAsync();
        Task<GetSupplierDto> GetSupplierAsync(long id);
        Task<GetSupplierDto> CreateSupplierAsync(CreateSupplierDto supplierDto);
        Task UpdateSupplierAsync(long id, EditSupplierDto supplierDto);
        Task DeleteSupplierAsync(long id);
    }
}
