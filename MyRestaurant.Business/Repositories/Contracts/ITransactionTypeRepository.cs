using MyRestaurant.Business.Dtos.V1;

namespace MyRestaurant.Business.Repositories.Contracts
{
    public interface ITransactionTypeRepository
    {
        Task<IEnumerable<GetTransactionTypeDto>> GetTransactionTypesAsync();
        Task<GetTransactionTypeDto> GetTransactionTypeAsync(int id);
        Task<GetTransactionTypeDto> CreateTransactionTypeAsync(CreateTransactionTypeDto transactionTypeDto);
        Task<GetTransactionTypeDto> UpdateTransactionTypeAsync(int id, EditTransactionTypeDto transactionTypeDto);
        Task DeleteTransactionTypeAsync(int id);
    }
}
