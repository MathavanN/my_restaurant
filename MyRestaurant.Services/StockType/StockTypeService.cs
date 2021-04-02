using MyRestaurant.Core;
using MyRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyRestaurant.Services
{
    public class StockTypeService : IStockTypeService
    {
        private readonly IMyRestaurantContext _context;
        public StockTypeService(IMyRestaurantContext context)
        {
            _context = context;
        }

        public async Task<StockType> AddStockTypeAsync(StockType stockType)
        {
            _context.Create(stockType);
            await _context.CommitAsync();
            return stockType;
        }

        public async Task DeleteStockTypeAsync(StockType stockType)
        {
            _context.Delete(stockType);
            await _context.CommitAsync();
        }

        public async Task<StockType> GetStockTypeAsync(Expression<Func<StockType, bool>> expression) => await _context.GetFirstOrDefaultAsync(expression);

        public async Task<IEnumerable<StockType>> GetStockTypesAsync() => await _context.GetAllAsync<StockType>();

        public async Task UpdateStockTypeAsync(StockType stockType)
        {
            _context.Modify(stockType);
            await _context.CommitAsync();
        }
    }
}
