using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MyRestaurant.Core.Configurations.Mapping;
using MyRestaurant.Models;
using Newtonsoft.Json;
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

            builder.ApplyConfiguration(new AuditMapping());
            builder.ApplyConfiguration(new ServiceTypeMapping());
            builder.ApplyConfiguration(new RestaurantInfoMapping());
        }

        public DbSet<ServiceType> ServiceTypes { get; set; }
        public DbSet<RestaurantInfo> RestaurantInfos { get; set; }
        public DbSet<Audit> Audits { get; set; }
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

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var auditEntries = await OnBeforeSaveChanges();
            var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            await OnAfterSaveChanges(auditEntries);
            return result;
        }

        async Task<IEnumerable<Tuple<EntityEntry, Audit>>> OnBeforeSaveChanges()
        {
            ChangeTracker.DetectChanges();
            var entitiesToTrack = ChangeTracker.Entries().Where(e => !(e.Entity is Audit) && e.State != EntityState.Detached && e.State != EntityState.Unchanged);

            await Audits.AddRangeAsync(
                entitiesToTrack.Where(e => !e.Properties.Any(p => p.IsTemporary)).Select(e => new Audit()
                {
                    TableName = e.Metadata.GetTableName(),
                    Action = Enum.GetName(typeof(EntityState), e.State),
                    DateTime = DateTime.Now.ToUniversalTime(),
                    Username = "Default-APP", //this.httpContextAccessor?.HttpContext?.User?.Identity?.Name,
                    KeyValues = JsonConvert.SerializeObject(e.Properties.Where(p => p.Metadata.IsPrimaryKey()).ToDictionary(p => p.Metadata.Name, p => p.CurrentValue).NullIfEmpty()),
                    NewValues = JsonConvert.SerializeObject(e.Properties.Where(p => e.State == EntityState.Added || e.State == EntityState.Modified).ToDictionary(p => p.Metadata.Name, p => p.CurrentValue).NullIfEmpty()),
                    OldValues = JsonConvert.SerializeObject(e.Properties.Where(p => e.State == EntityState.Deleted || e.State == EntityState.Modified).ToDictionary(p => p.Metadata.Name, p => p.OriginalValue).NullIfEmpty())
                }).ToList()
            );

            //Return list of pairs of EntityEntry and ToolAudit
            return entitiesToTrack.Where(e => e.Properties.Any(p => p.IsTemporary))
                 .Select(e => new Tuple<EntityEntry, Audit>(e, new Audit()
                 {
                     TableName = e.Metadata.GetTableName(),
                     Action = Enum.GetName(typeof(EntityState), e.State),
                     DateTime = DateTime.Now.ToUniversalTime(),
                     Username = "Default-APP",  ///this.httpContextAccessor?.HttpContext?.User?.Identity?.Name,
                     NewValues = JsonConvert.SerializeObject(e.Properties.Where(p => !p.Metadata.IsPrimaryKey()).ToDictionary(p => p.Metadata.Name, p => p.CurrentValue).NullIfEmpty())
                 }
                 )).ToList();
        }
        async Task OnAfterSaveChanges(IEnumerable<Tuple<EntityEntry, Audit>> temporatyEntities)
        {
            if (temporatyEntities != null && temporatyEntities.Any())
            {
                await Audits.AddRangeAsync(
                    temporatyEntities.ForEach(t => t.Item2.KeyValues = JsonConvert.SerializeObject(t.Item1.Properties.Where(p => p.Metadata.IsPrimaryKey()).ToDictionary(p => p.Metadata.Name, p => p.CurrentValue).NullIfEmpty()))
                    .Select(t => t.Item2)
                );
                await SaveChangesAsync();
            }
            await Task.CompletedTask;
        }
    }
}