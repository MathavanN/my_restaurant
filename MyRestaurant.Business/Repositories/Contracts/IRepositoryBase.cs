using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace MyRestaurant.Business.Repositories.Contracts
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default);
        TEntity Modify(TEntity entity);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression = null, CancellationToken cancellationToken = default);
    }
}
