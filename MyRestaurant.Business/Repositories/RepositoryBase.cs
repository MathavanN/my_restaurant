using Microsoft.EntityFrameworkCore;
using MyRestaurant.Business.Repositories.Contracts;
using MyRestaurant.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyRestaurant.Business.Repositories
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        private readonly MyRestaurantContext _context;

        protected RepositoryBase(MyRestaurantContext context) => _context = context;

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression = null, CancellationToken cancellationToken = default)
        {
            return expression == null ?
                await _context.Set<TEntity>().ToListAsync(cancellationToken) :
                await _context.Set<TEntity>().Where(expression).ToListAsync(cancellationToken);
        }

        public async Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
        {
            return await _context.Set<TEntity>().Where(expression).FirstOrDefaultAsync();
        }

        public async Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var savedEntity = await _context.Set<TEntity>().AddAsync(entity, cancellationToken);
            return savedEntity.Entity;
        }

        public TEntity Modify(TEntity entity)
        {
            return _context.Set<TEntity>().Update(entity).Entity;
        }

        public void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
        }
    }
}
