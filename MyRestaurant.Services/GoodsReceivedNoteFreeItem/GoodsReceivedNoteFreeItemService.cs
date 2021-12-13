using Microsoft.EntityFrameworkCore;
using MyRestaurant.Core;
using MyRestaurant.Models;
using System.Linq.Expressions;

namespace MyRestaurant.Services
{
    public class GoodsReceivedNoteFreeItemService : IGoodsReceivedNoteFreeItemService
    {
        private readonly IMyRestaurantContext _context;
        public GoodsReceivedNoteFreeItemService(IMyRestaurantContext context)
        {
            _context = context;
        }

        public async Task<GoodsReceivedNoteFreeItem> AddGoodsReceivedNoteFreeItemAsync(GoodsReceivedNoteFreeItem goodsReceivedNoteFreeItem)
        {
            _context.Create(goodsReceivedNoteFreeItem);
            await _context.CommitAsync();

            return await _context.GoodsReceivedNoteFreeItems
                .Include(p => p.Item)
                .Include(p => p.GoodsReceivedNote)
                .FirstAsync(e => e.Id == goodsReceivedNoteFreeItem.Id);
        }

        public async Task<IEnumerable<GoodsReceivedNoteFreeItem>> GetGoodsReceivedNoteFreeItemsAsync(Expression<Func<GoodsReceivedNoteFreeItem, bool>> expression) => await _context.GetAllAsync(expression);

        public async Task<GoodsReceivedNoteFreeItem?> GetGoodsReceivedNoteFreeItemAsync(Expression<Func<GoodsReceivedNoteFreeItem, bool>> expression) => await _context.GetFirstOrDefaultAsync(expression);

        public async Task UpdateGoodsReceivedNoteFreeItemAsync(GoodsReceivedNoteFreeItem goodsReceivedNoteFreeItem)
        {
            _context.Modify(goodsReceivedNoteFreeItem);
            await _context.CommitAsync();
        }
        public async Task DeleteGoodsReceivedNoteFreeItemAsync(GoodsReceivedNoteFreeItem goodsReceivedNoteFreeItem)
        {
            _context.Delete(goodsReceivedNoteFreeItem);
            await _context.CommitAsync();
        }
    }
}
