using MyRestaurant.Business.Dtos.V1;
using System.Threading.Tasks;

namespace MyRestaurant.Business.Repositories.Contracts
{
    public interface ISupplierRepository
    {
        Task<SupplierEnvelop> GetSuppliersAsync(int? limit, int? offset, string name, string city, string contactPerson);
        Task<GetSupplierDto> GetSupplierAsync(long id);
        Task<GetSupplierDto> CreateSupplierAsync(CreateSupplierDto supplierDto);
        Task<GetSupplierDto> UpdateSupplierAsync(long id, EditSupplierDto supplierDto);
        Task DeleteSupplierAsync(long id);
    }
}
