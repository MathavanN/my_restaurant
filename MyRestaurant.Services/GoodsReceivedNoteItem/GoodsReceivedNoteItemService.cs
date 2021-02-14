using Microsoft.EntityFrameworkCore;
using MyRestaurant.Core;
using MyRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyRestaurant.Services
{
    public class GoodsReceivedNoteItemService : IGoodsReceivedNoteItemSevice
    {
        private readonly IMyRestaurantContext _context;
        public GoodsReceivedNoteItemService(IMyRestaurantContext context)
        {
            _context = context;
        }

        public async Task<GoodsReceivedNoteItem> AddGoodsReceivedNoteItemAsync(GoodsReceivedNoteItem goodsReceivedNoteItem)
        {
            _context.Create(goodsReceivedNoteItem);
            await _context.CommitAsync();

            return await _context.GoodsReceivedNoteItems
                .Include(p => p.Item)
                .Include(p => p.GoodsReceivedNote)
                .SingleOrDefaultAsync(e => e.Id == goodsReceivedNoteItem.Id);
        }

        public async Task<IEnumerable<GoodsReceivedNoteItem>> GetGoodsReceivedNoteItemsAsync(Expression<Func<GoodsReceivedNoteItem, bool>> expression) => await _context.GetAllAsync(expression);

        public async Task<GoodsReceivedNoteItem> GetGoodsReceivedNoteItemAsync(Expression<Func<GoodsReceivedNoteItem, bool>> expression) => await _context.GetFirstOrDefaultAsync(expression);

        public async Task UpdateGoodsReceivedNoteItemAsync(GoodsReceivedNoteItem goodsReceivedNoteItem)
        {
            _context.Modify(goodsReceivedNoteItem);
            await _context.CommitAsync();
        }
        public async Task DeleteGoodsReceivedNoteItemAsync(GoodsReceivedNoteItem goodsReceivedNoteItem)
        {
            _context.Delete(goodsReceivedNoteItem);
            await _context.CommitAsync();
        }
    }
}
