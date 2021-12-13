using MyRestaurant.Models;
using MyRestaurant.Services.Common;
using System.Linq.Expressions;

namespace MyRestaurant.Services
{
    public interface IStockItemService
    {
        Task<IEnumerable<StockItem>> GetStockItemsAsync();
        Task<CollectionEnvelop<StockItem>> GetStockItemsAsync(Expression<Func<StockItem, bool>> expression, int page, int itemsPerPage);
        Task<StockItem?> GetStockItemAsync(Expression<Func<StockItem, bool>> expression);
        Task<StockItem> AddStockItemAsync(StockItem stockItem);
        Task UpdateStockItemAsync(StockItem stockItem);
        Task DeleteStockItemAsync(StockItem stockItem);
    }
}
