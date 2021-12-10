using MyRestaurant.Models;
using System.Linq.Expressions;

namespace MyRestaurant.Services
{
    public interface IGoodsReceivedNoteItemService
    {
        Task<IEnumerable<GoodsReceivedNoteItem>> GetGoodsReceivedNoteItemsAsync(Expression<Func<GoodsReceivedNoteItem, bool>> expression);
        Task<GoodsReceivedNoteItem> GetGoodsReceivedNoteItemAsync(Expression<Func<GoodsReceivedNoteItem, bool>> expression);
        Task<GoodsReceivedNoteItem> AddGoodsReceivedNoteItemAsync(GoodsReceivedNoteItem goodsReceivedNoteItem);
        Task UpdateGoodsReceivedNoteItemAsync(GoodsReceivedNoteItem goodsReceivedNoteItem);
        Task DeleteGoodsReceivedNoteItemAsync(GoodsReceivedNoteItem goodsReceivedNoteItem);
    }
}
