using Microsoft.EntityFrameworkCore;
using MyRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace MyRestaurant.Core
{
    public interface IMyRestaurantContext
    {
        DbSet<RefreshToken> RefreshTokens { get; set; }
        DbSet<ServiceType> ServiceTypes { get; set; }
        DbSet<RestaurantInfo> RestaurantInfos { get; set; }
        DbSet<Supplier> Suppliers { get; set; }

        DbSet<UnitOfMeasure> UnitOfMeasures { get; set; }
        DbSet<StockType> StockTypes { get; set; }
        DbSet<StockItem> StockItems { get; set; }
        Task<TEntity> InsertAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : MyRestaurantObject;
        TEntity Modify<TEntity>(TEntity entity) where TEntity : MyRestaurantObject;
        void Delete<TEntity>(TEntity entity) where TEntity : MyRestaurantObject;
        void DeleteRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : MyRestaurantObject;
        Task<TEntity> GetFirstOrDefaultAsync<TEntity>(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default) where TEntity : MyRestaurantObject;
        Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(Expression<Func<TEntity, bool>> expression = null, CancellationToken cancellationToken = default) where TEntity : MyRestaurantObject;
        Task CommitAsync(CancellationToken cancellationToken = default);
    }
}
