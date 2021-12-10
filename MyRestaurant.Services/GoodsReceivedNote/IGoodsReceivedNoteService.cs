using MyRestaurant.Models;
using System.Linq.Expressions;

namespace MyRestaurant.Services
{
    public interface IGoodsReceivedNoteService
    {
        Task<IEnumerable<GoodsReceivedNote>> GetGoodsReceivedNotesAsync();
        Task<GoodsReceivedNote> GetGoodsReceivedNoteAsync(Expression<Func<GoodsReceivedNote, bool>> expression);
        Task<GoodsReceivedNote> AddGoodsReceivedNoteAsync(GoodsReceivedNote goodsReceivedNote);
        Task UpdateGoodsReceivedNoteAsync(GoodsReceivedNote goodsReceivedNote);
        Task DeleteGoodsReceivedNoteAsync(GoodsReceivedNote goodsReceivedNote);
    }
}
