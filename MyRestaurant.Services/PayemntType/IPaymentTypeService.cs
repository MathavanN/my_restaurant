using MyRestaurant.Models;
using System.Linq.Expressions;

namespace MyRestaurant.Services
{
    public interface IPaymentTypeService
    {
        Task<IEnumerable<PaymentType>> GetPaymentTypesAsync();
        Task<PaymentType> GetPaymentTypeAsync(Expression<Func<PaymentType, bool>> expression);
        Task<PaymentType> AddPaymentTypeAsync(PaymentType paymentType);
        Task UpdatePaymentTypeAsync(PaymentType paymentType);
        Task DeletePaymentTypeAsync(PaymentType paymentType);
    }
}
