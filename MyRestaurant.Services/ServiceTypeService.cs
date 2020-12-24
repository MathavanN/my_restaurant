using MyRestaurant.Core;
using MyRestaurant.Models;
using MyRestaurant.Services.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyRestaurant.Services
{
    public class ServiceTypeService : IServiceTypeService
    {
        private readonly IMyRestaurantContext _context;
        public ServiceTypeService(IMyRestaurantContext context)
        {
            _context = context;
        }

        public async Task<ServiceType> GetServiceTypeAsync(int id)
        {
            return await _context.GetFirstOrDefaultAsync<ServiceType>(d => d.Id == id);
        }
        public async Task AddServiceTypeAsync(ServiceType serviceType)
        {
            await _context.InsertAsync(serviceType);
            await _context.CommitAsync();
        }

        public async Task DeleteServiceTypeAsync(ServiceType serviceType)
        {
            _context.Delete(serviceType);
            await _context.CommitAsync();
        }

        public async Task<IEnumerable<ServiceType>> GetServiceTypesAsync()
        {
            return await _context.GetAllAsync<ServiceType>();
        }

        public async Task UpdateServiceTypeAsync(ServiceType serviceType)
        {
            _context.Modify(serviceType);
            await _context.CommitAsync();
        }
    }
}
