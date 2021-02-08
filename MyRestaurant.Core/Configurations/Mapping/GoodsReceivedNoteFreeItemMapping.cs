using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRestaurant.Core.Configurations.Base;
using MyRestaurant.Models;

namespace MyRestaurant.Core.Configurations.Mapping
{
    internal class GoodsReceivedNoteFreeItemMapping : GoodsReceivedNoteFreeItemMappingBase
    {
        public override void Configure(EntityTypeBuilder<GoodsReceivedNoteFreeItem> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).UseIdentityColumn();
            builder.Property(e => e.GoodsReceivedNoteId).IsRequired();
            builder.Property(e => e.ItemId).IsRequired();
            builder.Property(e => e.ItemUnitPrice).HasColumnType("decimal(18, 2)");
            builder.Property(e => e.Quantity).IsRequired();
            builder.Property(e => e.Nbt).HasColumnType("decimal(18, 2)");
            builder.Property(e => e.Vat).HasColumnType("decimal(18, 2)");
            builder.Property(e => e.Discount).HasColumnType("decimal(18, 2)");
            builder.ToTable("GoodsReceivedNoteFreeItems");

            builder.HasOne(d => d.GoodsReceivedNote)
                .WithMany(p => p.GoodsReceivedNoteFreeItems)
                .HasForeignKey(d => d.GoodsReceivedNoteId)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_GoodsReceivedNoteFreeItems_GoodsReceivedNotes");

            builder.HasOne(d => d.Item)
                .WithMany(p => p.GoodsReceivedNoteFreeItems)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_GoodsReceivedNoteFreeItems_StockItems");

            base.Configure(builder);
        }
    }
}
