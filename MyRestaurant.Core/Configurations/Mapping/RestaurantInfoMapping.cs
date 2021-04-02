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
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).UseIdentityColumn();
            builder.Property(e => e.Name).HasColumnType("varchar(256)").IsRequired();
            builder.Property(e => e.Address).HasColumnType("varchar(256)").IsRequired();
            builder.Property(e => e.City).HasColumnType("varchar(100)").IsRequired();
            builder.Property(e => e.Country).HasColumnType("varchar(100)").IsRequired();
            builder.Property(e => e.LandLine).HasColumnType("varchar(20)");
            builder.Property(e => e.Mobile).HasColumnType("varchar(20)");
            builder.Property(e => e.Email).HasColumnType("varchar(256)");
            builder.ToTable("RestaurantInfos");
            base.Configure(builder);
        }
    }
}
