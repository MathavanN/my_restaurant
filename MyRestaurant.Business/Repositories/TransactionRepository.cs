using AutoMapper;
using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Errors;
using MyRestaurant.Business.Repositories.Contracts;
using MyRestaurant.Models;
using MyRestaurant.Services;
using System.Net;

namespace MyRestaurant.Business.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly IMapper _mapper;
        private readonly ITransactionService _transaction;
        public TransactionRepository(IMapper mapper, ITransactionService transaction)
        {
            _mapper = mapper;
            _transaction = transaction;
        }

        public async Task<GetTransactionDto> CreateTransactionAsync(CreateTransactionDto transactionDto)
        {
            var transaction = _mapper.Map<Transaction>(transactionDto);
            transaction.CreatedAt = DateTime.Now;
            transaction = await _transaction.AddTransactionAsync(transaction);

            return _mapper.Map<GetTransactionDto>(transaction);
        }

        public async Task DeleteTransactionAsync(int id)
        {
            var transaction = await GetTransactionById(id);

            await _transaction.DeleteTransactionAsync(transaction);
        }

        public async Task<GetTransactionDto> GetTransactionAsync(int id)
        {
            var transaction = await GetTransactionById(id);

            return _mapper.Map<GetTransactionDto>(transaction);
        }

        public async Task<IEnumerable<GetTransactionDto>> GetTransactionsAsync()
        {
            var transactions = await _transaction.GetTransactionsAsync();

            return _mapper.Map<IEnumerable<GetTransactionDto>>(transactions.OrderBy(d => d.Date));
        }
        private async Task<Transaction> GetTransactionById(int id)
        {
            var transaction = await _transaction.GetTransactionAsync(d => d.Id == id);

            if (transaction == null)
                throw new RestException(HttpStatusCode.NotFound, "Transaction not found.");

            return transaction;
        }
        public async Task<GetTransactionDto> UpdateTransactionAsync(int id, EditTransactionDto transactionDto)
        {
            var transaction = await GetTransactionById(id);

            transaction = _mapper.Map(transactionDto, transaction);

            await _transaction.UpdateTransactionAsync(transaction);

            return _mapper.Map<GetTransactionDto>(transaction);
        }
    }
}
