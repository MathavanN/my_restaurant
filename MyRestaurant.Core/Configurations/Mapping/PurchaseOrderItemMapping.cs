using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRestaurant.Core.Configurations.Base;
using MyRestaurant.Models;

namespace MyRestaurant.Core.Configurations.Mapping
{
    internal class PurchaseOrderItemMapping : PurchaseOrderItemMappingBase
    {
        public override void Configure(EntityTypeBuilder<PurchaseOrderItem> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).UseIdentityColumn();
            builder.Property(e => e.PurchaseOrderId).IsRequired();
            builder.Property(e => e.ItemId).IsRequired();
            builder.Property(e => e.ItemUnitPrice).HasColumnType("decimal(18, 2)");
            builder.Property(e => e.Quantity).IsRequired();
            builder.ToTable("PurchaseOrderItems");

            builder.HasOne(d => d.Item)
                .WithMany(p => p.PurchaseOrderItems)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_PurchaseOrderItems_StockItems");

            builder.HasOne(d => d.PurchaseOrder)
                .WithMany(p => p.PurchaseOrderItems)
                .HasForeignKey(d => d.PurchaseOrderId)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_PurchaseOrderItems_PurchaseOrders");

            builder.Navigation(d => d.Item)
                .UsePropertyAccessMode(PropertyAccessMode.Property);

            base.Configure(builder);
        }
    }
}
