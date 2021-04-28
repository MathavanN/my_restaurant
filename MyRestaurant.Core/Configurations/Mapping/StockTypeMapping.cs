using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRestaurant.Core.Configurations.Base;
using MyRestaurant.Models;

namespace MyRestaurant.Core.Configurations.Mapping
{
    internal class StockTypeMapping : StockTypeMappingBase
    {
        public override void Configure(EntityTypeBuilder<StockType> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).UseIdentityColumn();
            builder.Property(e => e.Type).HasColumnType("varchar(50)").IsRequired();
            builder.HasIndex(e => e.Type, "IX_StockTypes").IsUnique();
            builder.Property(e => e.Description).HasColumnType("varchar(100)");
            builder.ToTable("StockTypes");

            base.Configure(builder);
        }
    }
}
