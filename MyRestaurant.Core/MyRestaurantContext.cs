using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyRestaurant.Core.Configurations.Mapping;
using MyRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace MyRestaurant.Core
{
    public class MyRestaurantContext : IdentityDbContext<User, Role, Guid>, IMyRestaurantContext
    {
        public MyRestaurantContext(DbContextOptions<MyRestaurantContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new ServiceTypeMapping());
        }

        public DbSet<ServiceType> ServiceTypes { get; set; }

        public async Task<TEntity> InsertAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : MyRestaurantObject
        {
            var savedEntity = await Set<TEntity>().AddAsync(entity, cancellationToken);
            return savedEntity.Entity;
        }

        public TEntity Modify<TEntity>(TEntity entity) where TEntity : MyRestaurantObject
        {
            return Set<TEntity>().Update(entity).Entity;
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : MyRestaurantObject
        {
            Set<TEntity>().Remove(entity);
        }

        public void DeleteRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : MyRestaurantObject
        {
            Set<TEntity>().RemoveRange(entities);
        }

        public async Task<TEntity> GetFirstOrDefaultAsync<TEntity>(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default) where TEntity : MyRestaurantObject
        {
            return await Set<TEntity>().Where(expression).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(Expression<Func<TEntity, bool>> expression = null, CancellationToken cancellationToken = default) where TEntity : MyRestaurantObject
        {
            return expression == null ?
                await Set<TEntity>().ToListAsync(cancellationToken) :
                await Set<TEntity>().Where(expression).ToListAsync(cancellationToken);
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            await SaveChangesAsync(cancellationToken);
        }
    }
}
