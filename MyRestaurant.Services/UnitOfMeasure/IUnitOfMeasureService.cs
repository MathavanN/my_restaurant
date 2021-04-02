using MyRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyRestaurant.Services
{
    public interface IUnitOfMeasureService
    {
        Task<IEnumerable<UnitOfMeasure>> GetUnitOfMeasuresAsync();
        Task<UnitOfMeasure> GetUnitOfMeasureAsync(Expression<Func<UnitOfMeasure, bool>> expression);
        Task<UnitOfMeasure> AddUnitOfMeasureAsync(UnitOfMeasure unitOfMeasure);
        Task UpdateUnitOfMeasureAsync(UnitOfMeasure unitOfMeasure);
        Task DeleteUnitOfMeasureAsync(UnitOfMeasure unitOfMeasure);
    }
}
