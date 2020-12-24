using MyRestaurant.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyRestaurant.Services.Contracts
{
    public interface IServiceTypeService
    {
        Task<IEnumerable<ServiceType>> GetServiceTypesAsync();
        Task<ServiceType> GetServiceTypeAsync(int id);
        Task AddServiceTypeAsync(ServiceType serviceType);
        Task UpdateServiceTypeAsync(ServiceType serviceType);
        Task DeleteServiceTypeAsync(ServiceType serviceType);
    }
}
