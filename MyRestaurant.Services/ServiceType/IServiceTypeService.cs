using MyRestaurant.Models;
using System.Linq.Expressions;

namespace MyRestaurant.Services
{
    public interface IServiceTypeService
    {
        Task<IEnumerable<ServiceType>> GetServiceTypesAsync();
        Task<ServiceType?> GetServiceTypeAsync(Expression<Func<ServiceType, bool>> expression);
        Task<ServiceType> AddServiceTypeAsync(ServiceType serviceType);
        Task UpdateServiceTypeAsync(ServiceType serviceType);
        Task DeleteServiceTypeAsync(ServiceType serviceType);
    }
}
