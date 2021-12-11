using Microsoft.EntityFrameworkCore;
using MyRestaurant.Core;
using MyRestaurant.Models;
using System.Linq.Expressions;

namespace MyRestaurant.Services
{
    public class GoodsReceivedNoteService : IGoodsReceivedNoteService
    {
        private readonly IMyRestaurantContext _context;
        public GoodsReceivedNoteService(IMyRestaurantContext context)
        {
            _context = context;
        }

        public async Task<GoodsReceivedNote> AddGoodsReceivedNoteAsync(GoodsReceivedNote goodsReceivedNote)
        {
            _context.Create(goodsReceivedNote);
            await _context.CommitAsync();

            return await _context.GoodsReceivedNotes
                .Include(p => p.PaymentType)
                .Include(p => p.PurchaseOrder)
                .Include(p => p.ReceivedUser)
                .FirstAsync(e => e.Id == goodsReceivedNote.Id);
        }

        public async Task DeleteGoodsReceivedNoteAsync(GoodsReceivedNote goodsReceivedNote)
        {
            _context.Delete(goodsReceivedNote);
            await _context.CommitAsync();
        }

        public async Task<GoodsReceivedNote?> GetGoodsReceivedNoteAsync(Expression<Func<GoodsReceivedNote, bool>> expression) => await _context.GetFirstOrDefaultAsync(expression);

        public async Task<IEnumerable<GoodsReceivedNote>> GetGoodsReceivedNotesAsync() => await _context.GetAllAsync<GoodsReceivedNote>();

        public async Task UpdateGoodsReceivedNoteAsync(GoodsReceivedNote goodsReceivedNote)
        {
            _context.Modify(goodsReceivedNote);
            await _context.CommitAsync();
        }
    }
}
