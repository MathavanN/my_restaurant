using MyRestaurant.Core;
using MyRestaurant.Models;
using System.Linq.Expressions;

namespace MyRestaurant.Services
{
    public class UnitOfMeasureService : IUnitOfMeasureService
    {
        private readonly IMyRestaurantContext _context;
        public UnitOfMeasureService(IMyRestaurantContext context)
        {
            _context = context;
        }

        public async Task<UnitOfMeasure> AddUnitOfMeasureAsync(UnitOfMeasure unitOfMeasure)
        {
            _context.Create(unitOfMeasure);
            await _context.CommitAsync();
            return unitOfMeasure;
        }

        public async Task DeleteUnitOfMeasureAsync(UnitOfMeasure unitOfMeasure)
        {
            _context.Delete(unitOfMeasure);
            await _context.CommitAsync();
        }

        public async Task<UnitOfMeasure?> GetUnitOfMeasureAsync(Expression<Func<UnitOfMeasure, bool>> expression) => await _context.GetFirstOrDefaultAsync(expression);

        public async Task<IEnumerable<UnitOfMeasure>> GetUnitOfMeasuresAsync() => await _context.GetAllAsync<UnitOfMeasure>();

        public async Task UpdateUnitOfMeasureAsync(UnitOfMeasure unitOfMeasure)
        {
            _context.Modify(unitOfMeasure);
            await _context.CommitAsync();
        }
    }
}
