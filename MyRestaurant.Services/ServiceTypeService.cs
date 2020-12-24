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
        public async Task<IEnumerable<ServiceType>> GetServiceTypesAsync()
        {
            return await _context.GetAllAsync<ServiceType>();
        }
    }
}
