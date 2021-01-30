using MyRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyRestaurant.Services
{
    public interface IGoodsReceivedNoteService
    {
        Task<IEnumerable<GoodsReceivedNote>> GetGoodsReceivedNotesAsync();
        Task<GoodsReceivedNote> GetGoodsReceivedNoteAsync(Expression<Func<GoodsReceivedNote, bool>> expression);
        Task AddGoodsReceivedNoteAsync(GoodsReceivedNote goodsReceivedNote);
        Task UpdateGoodsReceivedNoteAsync(GoodsReceivedNote goodsReceivedNote);
        Task DeleteGoodsReceivedNoteAsync(GoodsReceivedNote goodsReceivedNote);
    }
}
