using MyRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyRestaurant.Services
{
    public interface IStockTypeServices
    {
        Task<IEnumerable<StockType>> GetStockTypesAsync();
        Task<StockType> GetStockTypeAsync(Expression<Func<StockType, bool>> expression);
        Task AddStockTypeAsync(StockType stockType);
        Task UpdateStockTypeAsync(StockType stockType);
        Task DeleteStockTypeAsync(StockType stockType);
    }
}
