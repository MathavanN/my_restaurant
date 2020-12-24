using MyRestaurant.Business.Dtos.V1.ServiceTypeDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyRestaurant.Business.Repositories.Contracts
{
    public interface IServiceTypeRepository
    {
        Task<IEnumerable<GetServiceTypeDto>> GetServicesTypesAsync();
        Task<GetServiceTypeDto> GetServiceTypeAsync(int id);
        Task<GetServiceTypeDto> CreateServiceTypeAsync(CreateServiceTypeDto serviceTypeDto);
        Task UpdateServiceTypeAsync(int id, EditServiceTypeDto serviceTypeDto);
        Task DeleteServiceTypeAsync(int id);
    }
}
