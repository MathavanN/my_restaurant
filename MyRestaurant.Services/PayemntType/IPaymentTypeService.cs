using MyRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyRestaurant.Services
{
    public interface IPaymentTypeService
    {
        Task<IEnumerable<PaymentType>> GetPaymentTypesAsync();
        Task<PaymentType> GetPaymentTypeAsync(Expression<Func<PaymentType, bool>> expression);
        Task AddPaymentTypeAsync(PaymentType paymentType);
        Task UpdatePaymentTypeAsync(PaymentType paymentType);
        Task DeletePaymentTypeAsync(PaymentType paymentType);
    }
}
