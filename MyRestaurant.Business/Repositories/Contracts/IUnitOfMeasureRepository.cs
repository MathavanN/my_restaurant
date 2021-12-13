using MyRestaurant.Business.Dtos.V1;

namespace MyRestaurant.Business.Repositories.Contracts
{
    public interface IUnitOfMeasureRepository
    {
        Task<IEnumerable<GetUnitOfMeasureDto>> GetUnitOfMeasuresAsync();
        Task<GetUnitOfMeasureDto> GetUnitOfMeasureAsync(int id);
        Task<GetUnitOfMeasureDto> CreateUnitOfMeasureAsync(CreateUnitOfMeasureDto unitOfMeasureDto);
        Task<GetUnitOfMeasureDto> UpdateUnitOfMeasureAsync(int id, EditUnitOfMeasureDto unitOfMeasureDto);
        Task DeleteUnitOfMeasureAsync(int id);
    }
}
