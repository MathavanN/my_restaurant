using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRestaurant.Core.Models;

namespace MyRestaurant.Core.Configurations.Base
{
    public abstract class BaseEntityTypeConfiguration<TBase> : IEntityTypeConfiguration<TBase> where TBase : MyRestaurantBase
    {
        public virtual void Configure(EntityTypeBuilder<TBase> builder)
        {
        }
    }
}
