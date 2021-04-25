using Microsoft.EntityFrameworkCore;
using MyRestaurant.Core;
using MyRestaurant.Models;
using MyRestaurant.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyRestaurant.Services
{
    public class StockItemService : IStockItemService
    {
        private readonly IMyRestaurantContext _context;
        public StockItemService(IMyRestaurantContext context)
        {
            _context = context;
        }

        public async Task<StockItem> AddStockItemAsync(StockItem stockItem)
        {
            _context.Create(stockItem);
            await _context.CommitAsync();
            
            return await _context.StockItems
                .Include(p => p.Type)
                .Include(p => p.UnitOfMeasure)
                .FirstOrDefaultAsync(e => e.Id == stockItem.Id);
        }

        public async Task DeleteStockItemAsync(StockItem stockItem)
        {
            _context.Delete(stockItem);
            await _context.CommitAsync();
        }

        public async Task<StockItem> GetStockItemAsync(Expression<Func<StockItem, bool>> expression) => await _context.GetFirstOrDefaultAsync(expression);

        public async Task<IEnumerable<StockItem>> GetStockItemsAsync() => await _context.GetAllAsync<StockItem>();

        public async Task<CollectionEnvelop<StockItem>> GetStockItemsAsync(Expression<Func<StockItem, bool>> expression, int page, int itemsPerPage)
        {
            var stockItems = await _context.GetAllAsync(expression);
            var toSkip = page * itemsPerPage;

            return new CollectionEnvelop<StockItem>
            {
                TotalItems = stockItems.Count(),
                ItemsPerPage = itemsPerPage,
                Items = page == 0 ? stockItems.OrderBy(d => d.Name).AsQueryable().Take(itemsPerPage) : 
                                    stockItems.OrderBy(d => d.Name).AsQueryable().Skip(toSkip).Take(itemsPerPage),
            };
        }

        public async Task UpdateStockItemAsync(StockItem stockItem)
        {
            _context.Modify(stockItem);
            await _context.CommitAsync();
        }
    }
}
