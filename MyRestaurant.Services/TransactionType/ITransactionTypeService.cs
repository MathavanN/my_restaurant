using MyRestaurant.Models;
using System.Linq.Expressions;

namespace MyRestaurant.Services
{
    public interface ITransactionTypeService
    {
        Task<IEnumerable<TransactionType>> GetTransactionTypesAsync();
        Task<TransactionType> GetTransactionTypeAsync(Expression<Func<TransactionType, bool>> expression);
        Task<TransactionType> AddTransactionTypeAsync(TransactionType transactionType);
        Task UpdateTransactionTypeAsync(TransactionType transactionType);
        Task DeleteTransactionTypeAsync(TransactionType transactionType);
    }
}
