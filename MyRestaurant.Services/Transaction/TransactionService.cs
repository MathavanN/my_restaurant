using Microsoft.EntityFrameworkCore;
using MyRestaurant.Core;
using MyRestaurant.Models;
using System.Linq.Expressions;

namespace MyRestaurant.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IMyRestaurantContext _context;
        public TransactionService(IMyRestaurantContext context)
        {
            _context = context;
        }

        public async Task<Transaction> AddTransactionAsync(Transaction transaction)
        {
            _context.Create(transaction);
            await _context.CommitAsync();

            return await _context.Transactions
                .Include(p => p.TransactionType)
                .Include(p => p.PaymentType)
                .FirstOrDefaultAsync(e => e.Id == transaction.Id);
        }

        public async Task DeleteTransactionAsync(Transaction transaction)
        {
            _context.Delete(transaction);
            await _context.CommitAsync();
        }

        public async Task<Transaction> GetTransactionAsync(Expression<Func<Transaction, bool>> expression) => await _context.GetFirstOrDefaultAsync(expression);

        public async Task<IEnumerable<Transaction>> GetTransactionsAsync() => await _context.GetAllAsync<Transaction>();

        public async Task UpdateTransactionAsync(Transaction transaction)
        {
            _context.Modify(transaction);
            await _context.CommitAsync();
        }
    }
}
