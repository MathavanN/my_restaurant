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

        private async Task CheckSupplierAsync(long id, string name)
        {
            var dbSupplier = await _supplier.GetSupplierAsync(d => d.Name == name && d.Id != id);
            if (dbSupplier != null)
                throw new RestException(HttpStatusCode.Conflict, $"Supplier {name} is already available.");
        }

        public async Task<GetSupplierDto> CreateSupplierAsync(CreateSupplierDto supplierDto)
        {
            await CheckSupplierAsync(0, supplierDto.Name);

            var supplier = _mapper.Map<Supplier>(supplierDto);
            supplier = await _supplier.AddSupplierAsync(supplier);

            return _mapper.Map<GetSupplierDto>(supplier);
        }

        private async Task<Supplier> GetSupplierId(long id)
        {
            var supplier = await _supplier.GetSupplierAsync(d => d.Id == id);

            if (supplier == null)
                throw new RestException(HttpStatusCode.NotFound, "Supplier not found.");

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

        public async Task<SupplierEnvelop> GetSuppliersAsync(int? limit, int? offset, string name, string city, string contactPerson)
        {
            var collectionEnvelop = await _supplier.GetSuppliersAsync(name, city, contactPerson, offset ?? 0, limit ?? 10);

            return new SupplierEnvelop
            {
                Suppliers = _mapper.Map<IEnumerable<GetSupplierDto>>(collectionEnvelop.Items),
                SupplierCount = collectionEnvelop.TotalItems,
                ItemsPerPage = collectionEnvelop.ItemsPerPage,
                TotalPages = collectionEnvelop.TotalPages()
            };
        }

        public async Task<GetSupplierDto> UpdateSupplierAsync(long id, EditSupplierDto supplierDto)
        {
            await CheckSupplierAsync(id, supplierDto.Name);

            var supplier = await GetSupplierId(id);

            supplier = _mapper.Map(supplierDto, supplier);

            await _supplier.UpdateSupplierAsync(supplier);

            return _mapper.Map<GetSupplierDto>(supplier);
        }
    }
}
