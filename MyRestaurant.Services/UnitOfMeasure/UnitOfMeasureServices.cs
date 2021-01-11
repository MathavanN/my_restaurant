using MyRestaurant.Core;
using MyRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyRestaurant.Services
{
    public class UnitOfMeasureServices : IUnitOfMeasureServices
    {
        private readonly IMyRestaurantContext _context;
        public UnitOfMeasureServices(IMyRestaurantContext context)
        {
            _context = context;
        }

        public async Task AddUnitOfMeasureAsync(UnitOfMeasure unitOfMeasure)
        {
            await _context.InsertAsync(unitOfMeasure);
            await _context.CommitAsync();
        }

        public async Task DeleteUnitOfMeasureAsync(UnitOfMeasure unitOfMeasure)
        {
            _context.Delete(unitOfMeasure);
            await _context.CommitAsync();
        }

        public async Task<UnitOfMeasure> GetUnitOfMeasureAsync(Expression<Func<UnitOfMeasure, bool>> expression) => await _context.GetFirstOrDefaultAsync(expression);

        public async Task<IEnumerable<UnitOfMeasure>> GetUnitOfMeasuresAsync() => await _context.GetAllAsync<UnitOfMeasure>();

        public async Task UpdateUnitOfMeasureAsync(UnitOfMeasure unitOfMeasure)
        {
            _context.Modify(unitOfMeasure);
            await _context.CommitAsync();
        }
    }
}
