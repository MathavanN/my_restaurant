using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRestaurant.Core.Configurations.Base;
using MyRestaurant.Models;

namespace MyRestaurant.Core.Configurations.Mapping
{
    internal class PurchaseOrderMapping : PurchaseOrderMappingBase
    {
        public override void Configure(EntityTypeBuilder<PurchaseOrder> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).UseIdentityColumn();
            builder.Property(e => e.OrderNumber).HasColumnType("varchar(50)").IsRequired();
            builder.Property(e => e.SupplierId).IsRequired();
            builder.Property(e => e.RequestedBy).IsRequired();
            builder.Property(e => e.RequestedDate).HasColumnType("datetime").IsRequired();
            builder.Property(e => e.ApprovalStatus).IsRequired();
            builder.Property(e => e.ApprovedBy);
            builder.Property(e => e.ApprovalReason).HasColumnType("varchar(500)");
            builder.Property(e => e.ApprovedDate).HasColumnType("datetime");
            builder.Property(e => e.Description).HasColumnType("varchar(500)").HasMaxLength(500);
            builder.ToTable("PurchaseOrders");

            builder.HasOne(d => d.Supplier)
                .WithMany(p => p.PurchaseOrders)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_PurchaseOrders_Suppliers");

            builder.HasOne(d => d.RequestedUser)
                .WithMany(p => p.PurchaseOrderRequests)
                .HasForeignKey(d => d.RequestedBy)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_PurchaseOrders_RequestedBy");

            builder.HasOne(d => d.ApprovedUser)
                .WithMany(p => p.PurchaseOrderApprovals)
                .HasForeignKey(d => d.ApprovedBy)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_PurchaseOrders_ApprovedBy");

            base.Configure(builder);
        }
    }
}
