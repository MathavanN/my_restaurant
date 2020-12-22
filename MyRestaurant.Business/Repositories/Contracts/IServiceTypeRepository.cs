using MyRestaurant.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyRestaurant.Business.Repositories.Contracts
{
    public interface IServiceTypeRepository : IRepositoryBase<ServiceType>
    {
        Task<IEnumerable<ServiceType>> GetServicesTypeAsync();
    }
}
