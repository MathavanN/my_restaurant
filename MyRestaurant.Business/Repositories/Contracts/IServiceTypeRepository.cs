using MyRestaurant.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyRestaurant.Business.Repositories.Contracts
{
    public interface IServiceTypeRepository
    {
        Task<IEnumerable<ServiceType>> GetServicesTypeAsync();
    }
}
