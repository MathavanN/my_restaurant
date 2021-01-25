﻿using MyRestaurant.Core;
using MyRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyRestaurant.Services
{
    public class StockItemServices : IStockItemServices
    {
        private readonly IMyRestaurantContext _context;
        public StockItemServices(IMyRestaurantContext context)
        {
            _context = context;
        }

        public async Task AddStockItemAsync(StockItem stockItem)
        {
            await _context.InsertAsync(stockItem);
            await _context.CommitAsync();
        }

        public async Task DeleteStockItemAsync(StockItem stockItem)
        {
            _context.Delete(stockItem);
            await _context.CommitAsync();
        }

        public async Task<StockItem> GetStockItemAsync(Expression<Func<StockItem, bool>> expression) => await _context.GetFirstOrDefaultAsync(expression);

        public async Task<IEnumerable<StockItem>> GetStockItemsAsync() => await _context.GetAllAsync<StockItem>();

        public IQueryable<StockItem> GetStockItemsAsync(Expression<Func<StockItem, bool>> expression) => _context.GetAllQueryable(expression);

        public async Task UpdateStockItemAsync(StockItem stockItem)
        {
            _context.Modify(stockItem);
            await _context.CommitAsync();
        }
    }
}
