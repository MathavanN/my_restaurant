using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRestaurant.Core.Configurations.Base;
using MyRestaurant.Models;

namespace MyRestaurant.Core.Configurations.Mapping
{
    internal class RestaurantInfoMapping : RestaurantInfoMappingBase
    {
        public override void Configure(EntityTypeBuilder<RestaurantInfo> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).UseIdentityColumn();
            builder.Property(t => t.Name).HasColumnType("varchar(256)").IsRequired();
            builder.Property(t => t.Address).HasColumnType("varchar(256)").IsRequired();
            builder.Property(t => t.City).HasColumnType("varchar(100)").IsRequired();
            builder.Property(t => t.Country).HasColumnType("varchar(100)").IsRequired();
            builder.Property(t => t.LandLine).HasColumnType("varchar(20)");
            builder.Property(t => t.Mobile).HasColumnType("varchar(20)");
            builder.Property(t => t.Email).HasColumnType("varchar(256)");
            builder.ToTable("RestaurantInfos");
            base.Configure(builder);
        }
    }
}
