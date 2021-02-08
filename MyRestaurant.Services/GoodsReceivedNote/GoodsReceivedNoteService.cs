using MyRestaurant.Core;
using MyRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyRestaurant.Services
{
    public class GoodsReceivedNoteService : IGoodsReceivedNoteService
    {
        private readonly IMyRestaurantContext _context;
        public GoodsReceivedNoteService(IMyRestaurantContext context)
        {
            _context = context;
        }

        public async Task AddGoodsReceivedNoteAsync(GoodsReceivedNote goodsReceivedNote)
        {
            await _context.InsertAsync(goodsReceivedNote);
            await _context.CommitAsync();
        }

        public async Task DeleteGoodsReceivedNoteAsync(GoodsReceivedNote goodsReceivedNote)
        {
            _context.Delete(goodsReceivedNote);
            await _context.CommitAsync();
        }

        public async Task<GoodsReceivedNote> GetGoodsReceivedNoteAsync(Expression<Func<GoodsReceivedNote, bool>> expression) => await _context.GetFirstOrDefaultAsync(expression);

        public async Task<IEnumerable<GoodsReceivedNote>> GetGoodsReceivedNotesAsync() => await _context.GetAllAsync<GoodsReceivedNote>();

        public async Task UpdateGoodsReceivedNoteAsync(GoodsReceivedNote goodsReceivedNote)
        {
            _context.Modify(goodsReceivedNote);
            await _context.CommitAsync();
        }
    }
}
