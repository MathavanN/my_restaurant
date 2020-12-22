using MyRestaurant.Business.Repositories.Contracts;
using MyRestaurant.Core;
using MyRestaurant.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyRestaurant.Business.Repositories
{
    public class ServiceTypeRepository : RepositoryBase<ServiceType>, IServiceTypeRepository
    {
        public ServiceTypeRepository(MyRestaurantContext context) : base(context)
        {
        }
        public async Task<IEnumerable<ServiceType>> GetServicesTypeAsync()
        {
            return await GetAllAsync(null);
        }
    }
}
