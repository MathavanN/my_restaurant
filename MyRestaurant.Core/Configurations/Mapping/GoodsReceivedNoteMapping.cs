using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRestaurant.Core.Configurations.Base;
using MyRestaurant.Models;

namespace MyRestaurant.Core.Configurations.Mapping
{
    internal class GoodsReceivedNoteMapping : GoodsReceivedNoteMappingBase
    {
        public override void Configure(EntityTypeBuilder<GoodsReceivedNote> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).UseIdentityColumn();
            builder.Property(e => e.PurchaseOrderId).IsRequired();
            builder.Property(e => e.InvoiceNumber).HasColumnType("varchar(30)");
            builder.Property(e => e.PaymentTypeId).IsRequired();
            builder.Property(e => e.Nbt).HasColumnType("decimal(18, 2)");
            builder.Property(e => e.Vat).HasColumnType("decimal(18, 2)");
            builder.Property(e => e.Discount).HasColumnType("decimal(18, 2)");
            builder.Property(e => e.ReceivedBy).IsRequired();
            builder.Property(e => e.ReceivedDate).HasColumnType("datetime").IsRequired();
            builder.Property(e => e.CreatedBy).IsRequired();
            builder.Property(e => e.CreatedDate).HasColumnType("datetime").IsRequired();
            builder.Property(e => e.ApprovalStatus).IsRequired();
            builder.Property(e => e.ApprovedBy);
            builder.Property(e => e.ApprovedDate).HasColumnType("datetime");
            builder.Property(e => e.ApprovalReason).HasColumnType("varchar(500)");
            builder.ToTable("GoodsReceivedNotes");

            builder.HasOne(d => d.ReceivedUser)
                .WithMany(p => p.GRNReceives)
                .HasForeignKey(d => d.ReceivedBy)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_GoodsReceivedNotes_ReceivedBy");

            builder.HasOne(d => d.ApprovedUser)
                .WithMany(p => p.GRNApprovals)
                .HasForeignKey(d => d.ApprovedBy)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_GoodsReceivedNotes_ApprovedBy");

            builder.HasOne(d => d.PaymentType)
                .WithMany(p => p.GoodsReceivedNotes)
                .HasForeignKey(d => d.PaymentTypeId)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_GoodsReceivedNotes_PaymentTypes");

            builder.HasOne(d => d.PurchaseOrder)
                .WithMany(p => p.GoodsReceivedNotes)
                .HasForeignKey(d => d.PurchaseOrderId)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_GoodsReceivedNotes_PurchaseOrders");

            base.Configure(builder);
        }
    }
}
