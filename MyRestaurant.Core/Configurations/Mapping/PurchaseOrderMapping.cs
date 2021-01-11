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
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).UseIdentityColumn();
            builder.Property(t => t.OrderNumber).HasColumnType("varchar(50)").IsRequired();
            builder.Property(t => t.SupplierId).IsRequired();
            builder.Property(t => t.RequestedBy).IsRequired();
            builder.Property(t => t.RequestedDate).HasColumnType("datetime").IsRequired();
            builder.Property(t => t.ApprovalStatus).IsRequired();
            builder.Property(t => t.ApprovedBy);
            builder.Property(t => t.ApprovalReason).HasColumnType("varchar(500)");
            builder.Property(t => t.ApprovedDate).HasColumnType("datetime");
            builder.Property(t => t.Discount).HasColumnType("decimal(18, 2)");
            builder.Property(t => t.Description).HasColumnType("varchar(500)").HasMaxLength(500);
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
