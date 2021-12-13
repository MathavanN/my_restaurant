using MyRestaurant.Business.Dtos.V1;

namespace MyRestaurant.Business.Repositories.Contracts
{
    public interface IServiceTypeRepository
    {
        Task<IEnumerable<GetServiceTypeDto>> GetServiceTypesAsync();
        Task<GetServiceTypeDto> GetServiceTypeAsync(int id);
        Task<GetServiceTypeDto> CreateServiceTypeAsync(CreateServiceTypeDto serviceTypeDto);
        Task<GetServiceTypeDto> UpdateServiceTypeAsync(int id, EditServiceTypeDto serviceTypeDto);
        Task DeleteServiceTypeAsync(int id);
    }
}
