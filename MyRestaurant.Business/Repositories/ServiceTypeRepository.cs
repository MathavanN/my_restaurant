using MyRestaurant.Business.Repositories.Contracts;
using MyRestaurant.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyRestaurant.Services.Contracts;

namespace MyRestaurant.Business.Repositories
{
    public class ServiceTypeRepository : IServiceTypeRepository
    {
        private readonly IServiceTypeService _serviceType;
        public ServiceTypeRepository(IServiceTypeService serviceType)
        {
            _serviceType = serviceType;
        }
        public async Task<IEnumerable<ServiceType>> GetServicesTypeAsync()
        {
            return await _serviceType.GetServiceTypesAsync();
        }
    }
}
