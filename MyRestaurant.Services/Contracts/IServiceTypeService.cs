using MyRestaurant.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyRestaurant.Services.Contracts
{
    public interface IServiceTypeService
    {
        Task<IEnumerable<ServiceType>> GetServiceTypesAsync();
    }
}
