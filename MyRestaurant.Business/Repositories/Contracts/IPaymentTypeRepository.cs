using MyRestaurant.Business.Dtos.V1;

namespace MyRestaurant.Business.Repositories.Contracts
{
    public interface IPaymentTypeRepository
    {
        Task<IEnumerable<GetPaymentTypeDto>> GetPaymentTypesAsync();
        Task<GetPaymentTypeDto> GetPaymentTypeAsync(int id);
        Task<GetPaymentTypeDto> CreatePaymentTypeAsync(CreatePaymentTypeDto paymentTypeDto);
        Task<GetPaymentTypeDto> UpdatePaymentTypeAsync(int id, EditPaymentTypeDto paymentTypeDto);
        Task DeletePaymentTypeAsync(int id);
    }
}
