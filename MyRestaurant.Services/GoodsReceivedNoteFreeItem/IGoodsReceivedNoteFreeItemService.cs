using MyRestaurant.Models;
using System.Linq.Expressions;

namespace MyRestaurant.Services
{
    public interface IGoodsReceivedNoteFreeItemService
    {
        Task<IEnumerable<GoodsReceivedNoteFreeItem>> GetGoodsReceivedNoteFreeItemsAsync(Expression<Func<GoodsReceivedNoteFreeItem, bool>> expression);
        Task<GoodsReceivedNoteFreeItem> GetGoodsReceivedNoteFreeItemAsync(Expression<Func<GoodsReceivedNoteFreeItem, bool>> expression);
        Task<GoodsReceivedNoteFreeItem> AddGoodsReceivedNoteFreeItemAsync(GoodsReceivedNoteFreeItem goodsReceivedNoteFreeItem);
        Task UpdateGoodsReceivedNoteFreeItemAsync(GoodsReceivedNoteFreeItem goodsReceivedNoteFreeItem);
        Task DeleteGoodsReceivedNoteFreeItemAsync(GoodsReceivedNoteFreeItem goodsReceivedNoteFreeItem);
    }
}
