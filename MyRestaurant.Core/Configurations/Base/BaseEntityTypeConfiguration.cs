using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRestaurant.Models;

namespace MyRestaurant.Core.Configurations.Base
{
    public abstract class BaseEntityTypeConfiguration<TBase> : IEntityTypeConfiguration<TBase> where TBase : MyRestaurantObject
    {
        public virtual void Configure(EntityTypeBuilder<TBase> builder)
        {
        }
    }
}
