using MyRestaurant.Core;
using MyRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyRestaurant.Services
{
    public class TransactionTypeService : ITransactionTypeService
    {
        private readonly IMyRestaurantContext _context;
        public TransactionTypeService(IMyRestaurantContext context)
        {
            _context = context;
        }

        public async Task<TransactionType> AddTransactionTypeAsync(TransactionType transactionType)
        {
            _context.Create(transactionType);
            await _context.CommitAsync();
            return transactionType;
        }

        public async Task DeleteTransactionTypeAsync(TransactionType transactionType)
        {
            _context.Delete(transactionType);
            await _context.CommitAsync();
        }

        public async Task<TransactionType> GetTransactionTypeAsync(Expression<Func<TransactionType, bool>> expression) => await _context.GetFirstOrDefaultAsync(expression);

        public async Task<IEnumerable<TransactionType>> GetTransactionTypesAsync() => await _context.GetAllAsync<TransactionType>();

        public async Task UpdateTransactionTypeAsync(TransactionType transactionType)
        {
            _context.Modify(transactionType);
            await _context.CommitAsync();
        }
    }
}
