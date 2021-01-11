using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRestaurant.Core.Configurations.Base;
using MyRestaurant.Models;

namespace MyRestaurant.Core.Configurations.Mapping
{
    internal class StockTypeMapping : StockTypeMappingBase {
        public override void Configure(EntityTypeBuilder<StockType> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).UseIdentityColumn();
            builder.Property(t => t.Type).HasColumnType("varchar(50)").IsRequired();
            builder.HasIndex(t => t.Type, "IX_StockTypes").IsUnique();
            builder.Property(t => t.Description).HasColumnType("varchar(100)");
            builder.ToTable("StockTypes");

            base.Configure(builder);
        }
    }
}
