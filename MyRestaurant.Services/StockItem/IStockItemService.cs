using MyRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyRestaurant.Services
{
    public interface IStockItemService
    {
        Task<IEnumerable<StockItem>> GetStockItemsAsync();
        IQueryable<StockItem> GetStockItemsAsync(Expression<Func<StockItem, bool>> expression);
        Task<StockItem> GetStockItemAsync(Expression<Func<StockItem, bool>> expression);
        Task<StockItem> AddStockItemAsync(StockItem stockItem);
        Task UpdateStockItemAsync(StockItem stockItem);
        Task DeleteStockItemAsync(StockItem stockItem);
    }
}
