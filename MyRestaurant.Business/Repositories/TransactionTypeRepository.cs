using AutoMapper;
using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Errors;
using MyRestaurant.Business.Repositories.Contracts;
using MyRestaurant.Models;
using MyRestaurant.Services;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MyRestaurant.Business.Repositories
{
    public class TransactionTypeRepository : ITransactionTypeRepository
    {
        private readonly IMapper _mapper;
        private readonly ITransactionTypeService _transactionType;
        public TransactionTypeRepository(IMapper mapper, ITransactionTypeService transactionType)
        {
            _mapper = mapper;
            _transactionType = transactionType;
        }

        private async Task CheckTransactionTypeAsync(int id, string type)
        {
            var dbTransactionType = await _transactionType.GetTransactionTypeAsync(d => d.Type == type && d.Id != id);
            if (dbTransactionType != null)
                throw new RestException(HttpStatusCode.Conflict, $"Transaction type \"{type}\" is already available.");
        }

        public async Task<GetTransactionTypeDto> CreateTransactionTypeAsync(CreateTransactionTypeDto transactionTypeDto)
        {
            await CheckTransactionTypeAsync(0, transactionTypeDto.Type);

            var transactionType = _mapper.Map<TransactionType>(transactionTypeDto);
            transactionType = await _transactionType.AddTransactionTypeAsync(transactionType);

            return _mapper.Map<GetTransactionTypeDto>(transactionType);
        }

        public async Task DeleteTransactionTypeAsync(int id)
        {
            var transactionType = await GetTransactionTypeById(id);

            await _transactionType.DeleteTransactionTypeAsync(transactionType);
        }

        public async Task<GetTransactionTypeDto> GetTransactionTypeAsync(int id)
        {
            var transactionType = await GetTransactionTypeById(id);

            return _mapper.Map<GetTransactionTypeDto>(transactionType);
        }

        public async Task<IEnumerable<GetTransactionTypeDto>> GetTransactionTypesAsync()
        {
            var transactionTypes = await _transactionType.GetTransactionTypesAsync();

            return _mapper.Map<IEnumerable<GetTransactionTypeDto>>(transactionTypes.OrderBy(d => d.Type));
        }
        private async Task<TransactionType> GetTransactionTypeById(int id)
        {
            var transactionType = await _transactionType.GetTransactionTypeAsync(d => d.Id == id);

            if (transactionType == null)
                throw new RestException(HttpStatusCode.NotFound, "Transaction type not found.");

            return transactionType;
        }
        public async Task<GetTransactionTypeDto> UpdateTransactionTypeAsync(int id, EditTransactionTypeDto transactionTypeDto)
        {
            var transactionType = await GetTransactionTypeById(id);

            await CheckTransactionTypeAsync(id, transactionTypeDto.Type);

            transactionType = _mapper.Map(transactionTypeDto, transactionType);

            await _transactionType.UpdateTransactionTypeAsync(transactionType);

            return _mapper.Map<GetTransactionTypeDto>(transactionType);
        }
    }
}
