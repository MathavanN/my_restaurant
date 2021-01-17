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
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).UseIdentityColumn();
            builder.Property(t => t.PurchaseOrderId).IsRequired();
            builder.Property(t => t.ItemId).IsRequired();
            builder.Property(t => t.ItemUnitPrice).HasColumnType("decimal(18, 2)");
            builder.Property(t => t.Quantity).IsRequired();
            builder.Property(t => t.Discount).HasColumnType("decimal(18, 2)");
            builder.ToTable("PurchaseOrderItems");

            builder.HasOne(d => d.Item)
                    .WithMany(d => d.PurchaseOrderItems)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientCascade)
                    .HasConstraintName("FK_PurchaseOrderItems_StockItems");

            builder.HasOne(d => d.PurchaseOrder)
                .WithMany(d => d.PurchaseOrderItems)
                .HasForeignKey(d => d.PurchaseOrderId)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_PurchaseOrderItems_PurchaseOrders");

            builder.Navigation(d => d.Item)
                .UsePropertyAccessMode(PropertyAccessMode.Property);

            base.Configure(builder);
        }
    }
}
