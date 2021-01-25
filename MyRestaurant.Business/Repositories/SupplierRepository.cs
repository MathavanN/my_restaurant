using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Errors;
using MyRestaurant.Business.Repositories.Contracts;
using MyRestaurant.Models;
using MyRestaurant.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var queryable = _supplier.GetSuppliersAsync()
                                     .OrderBy(d => d.Name)
                                     .AsQueryable()
                                     .AsAsyncEnumerable();

            if (!string.IsNullOrWhiteSpace(name))
                queryable = queryable.Where(d => d.Name.Contains(name, StringComparison.InvariantCultureIgnoreCase));

            if (!string.IsNullOrWhiteSpace(city))
                queryable = queryable.Where(d => d.City.Equals(city, StringComparison.InvariantCultureIgnoreCase));

            if (!string.IsNullOrWhiteSpace(contactPerson))
                queryable = queryable.Where(d => d.ContactPerson.Equals(contactPerson, StringComparison.InvariantCultureIgnoreCase));

            var suppliers = await queryable
                                    .Skip(offset ?? 0)
                                    .Take(limit ?? int.MaxValue)
                                    .ToListAsync();

            return new SupplierEnvelop
            {
                Suppliers = _mapper.Map<IEnumerable<GetSupplierDto>>(suppliers),
                SupplierCount = await queryable.CountAsync()
            };
        }

        public async Task UpdateSupplierAsync(long id, EditSupplierDto supplierDto)
        {
            var supplier = await GetSupplierId(id);

            supplier = _mapper.Map(supplierDto, supplier);

            await _supplier.UpdateSupplierAsync(supplier);
        }
    }
}
