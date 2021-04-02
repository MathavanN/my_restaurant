using MyRestaurant.Core;
using MyRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyRestaurant.Services
{
    public class PaymentTypeService : IPaymentTypeService
    {
        private readonly IMyRestaurantContext _context;
        public PaymentTypeService(IMyRestaurantContext context)
        {
            _context = context;
        }

        public async Task<PaymentType> AddPaymentTypeAsync(PaymentType paymentType)
        {
            _context.Create(paymentType);
            await _context.CommitAsync();
            return paymentType;
        }

        public async Task DeletePaymentTypeAsync(PaymentType paymentType)
        {
            _context.Delete(paymentType);
            await _context.CommitAsync();
        }

        public async Task<PaymentType> GetPaymentTypeAsync(Expression<Func<PaymentType, bool>> expression) => await _context.GetFirstOrDefaultAsync(expression);

        public async Task<IEnumerable<PaymentType>> GetPaymentTypesAsync() => await _context.GetAllAsync<PaymentType>();

        public async Task UpdatePaymentTypeAsync(PaymentType paymentType)
        {
            _context.Modify(paymentType);
            await _context.CommitAsync();
        }
    }
}
