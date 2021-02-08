using MyRestaurant.Business.Dtos.V1;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyRestaurant.Business.Repositories.Contracts
{
    public interface IPaymentTypeRepository
    {
        Task<IEnumerable<GetPaymentTypeDto>> GetPaymentTypesAsync();
        Task<GetPaymentTypeDto> GetPaymentTypeAsync(int id);
        Task<GetPaymentTypeDto> CreatePaymentTypeAsync(CreatePaymentTypeDto paymentTypeDto);
        Task UpdatePaymentTypeAsync(int id, EditPaymentTypeDto paymentTypeDto);
        Task DeletePaymentTypeAsync(int id);
    }
}
