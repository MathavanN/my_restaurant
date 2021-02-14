using MyRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyRestaurant.Services
{
    public interface IGoodsReceivedNoteItemSevice
    {
        Task<IEnumerable<GoodsReceivedNoteItem>> GetGoodsReceivedNoteItemsAsync(Expression<Func<GoodsReceivedNoteItem, bool>> expression);
        Task<GoodsReceivedNoteItem> GetGoodsReceivedNoteItemAsync(Expression<Func<GoodsReceivedNoteItem, bool>> expression);
        Task<GoodsReceivedNoteItem> AddGoodsReceivedNoteItemAsync(GoodsReceivedNoteItem goodsReceivedNoteItem);
        Task UpdateGoodsReceivedNoteItemAsync(GoodsReceivedNoteItem goodsReceivedNoteItem);
        Task DeleteGoodsReceivedNoteItemAsync(GoodsReceivedNoteItem goodsReceivedNoteItem);
    }
}
