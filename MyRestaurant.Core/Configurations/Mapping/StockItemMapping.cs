using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRestaurant.Core.Configurations.Base;
using MyRestaurant.Models;

namespace MyRestaurant.Core.Configurations.Mapping
{
    internal class StockItemMapping : StockItemMappingBase
    {
        public override void Configure(EntityTypeBuilder<StockItem> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).UseIdentityColumn();
            builder.Property(e => e.TypeId).IsRequired();
            builder.Property(e => e.UnitOfMeasureId).IsRequired();
            builder.Property(e => e.ItemUnit).HasColumnType("decimal(18, 4)").IsRequired();
            builder.Property(e => e.Name).HasColumnType("varchar(250)").IsRequired();
            builder.HasIndex(e => new { e.Name, e.TypeId, e.UnitOfMeasureId, e.ItemUnit }, "IX_StockItems").IsUnique();
            builder.Property(e => e.Description).HasColumnType("varchar(500)");
            builder.ToTable("StockItems");

            builder.HasOne(d => d.Type)
                .WithMany(p => p.StockItems)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_StockItems_StockTypes");

            builder.HasOne(d => d.UnitOfMeasure)
                .WithMany(p => p.StockItems)
                .HasForeignKey(d => d.UnitOfMeasureId)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_StockItems_UnitOfMeasures");

            builder.Navigation(d => d.Type).UsePropertyAccessMode(PropertyAccessMode.Property);
            builder.Navigation(d => d.UnitOfMeasure).UsePropertyAccessMode(PropertyAccessMode.Property);

            base.Configure(builder);
        }
    }
}
