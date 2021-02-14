using MyRestaurant.Core;
using MyRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyRestaurant.Services
{

    public class StockTypeServices : IStockTypeServices
    {
        private readonly IMyRestaurantContext _context;
        public StockTypeServices(IMyRestaurantContext context)
        {
            _context = context;
        }

        public async Task AddStockTypeAsync(StockType stockType)
        {
            _context.Create(stockType);
            await _context.CommitAsync();
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
