using AutoMapper;
using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Errors;
using MyRestaurant.Business.Repositories.Contracts;
using MyRestaurant.Models;
using MyRestaurant.Services;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace MyRestaurant.Business.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly IMapper _mapper;
        private readonly ISupplierService _supplier;
        public SupplierRepository(IMapper mapper, ISupplierService supplier)
        {
            _mapper = mapper;
            _supplier = supplier;
        }

        public async Task<GetSupplierDto> CreateSupplierAsync(CreateSupplierDto supplierDto)
        {
            var dbSupplier = await _supplier.GetSupplierAsync(d => d.Name == supplierDto.Name);
            if (dbSupplier != null)
                throw new RestException(HttpStatusCode.Conflict, $"Supplier {supplierDto.Name} is already available.");

            var supplier = _mapper.Map<Supplier>(supplierDto);
            await _supplier.AddSupplierAsync(supplier);

            return _mapper.Map<GetSupplierDto>(supplier);
        }

        private async Task<Supplier> GetSupplierId(long id)
        {
            var supplier = await _supplier.GetSupplierAsync(d => d.Id == id);

            if (supplier == null)
                throw new RestException(HttpStatusCode.NotFound, "Supplier Not Found");

            return supplier;
        }

        public async Task DeleteSupplierAsync(long id)
        {
            var supplier = await GetSupplierId(id);

            await _supplier.DeleteSupplierAsync(supplier);
        }

        public async Task<GetSupplierDto> GetSupplierAsync(long id)
        {
            var supplier = await GetSupplierId(id);

            return _mapper.Map<GetSupplierDto>(supplier);
        }

        public async Task<IEnumerable<GetSupplierDto>> GetSuppliersAsync()
        {
            var serviceTypes = await _supplier.GetSuppliersAsync();

            return _mapper.Map<IEnumerable<GetSupplierDto>>(serviceTypes);
        }

        public async Task UpdateSupplierAsync(long id, EditSupplierDto supplierDto)
        {
            var supplier = await GetSupplierId(id);

            supplier = _mapper.Map(supplierDto, supplier);

            await _supplier.UpdateSupplierAsync(supplier);
        }
    }
}
