using MyRestaurant.Models;
using System.Linq.Expressions;

namespace MyRestaurant.Services
{
    public interface IStockTypeService
    {
        Task<IEnumerable<StockType>> GetStockTypesAsync();
        Task<StockType?> GetStockTypeAsync(Expression<Func<StockType, bool>> expression);
        Task<StockType> AddStockTypeAsync(StockType stockType);
        Task UpdateStockTypeAsync(StockType stockType);
        Task DeleteStockTypeAsync(StockType stockType);
    }
}
