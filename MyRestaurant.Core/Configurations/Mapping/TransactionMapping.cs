using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRestaurant.Core.Configurations.Base;
using MyRestaurant.Models;

namespace MyRestaurant.Core.Configurations.Mapping
{
    internal class TransactionMapping : TransactionMappingBase
    {
        public override void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).UseIdentityColumn();
            builder.Property(e => e.TransactionTypeId).IsRequired();
            builder.Property(e => e.PaymentTypeId).IsRequired();
            builder.Property(e => e.Date).HasColumnType("datetime").IsRequired();
            builder.Property(e => e.Amount).HasColumnType("decimal(18, 2)").IsRequired();
            builder.Property(e => e.Description).HasColumnType("varchar(500)").IsRequired().HasMaxLength(500);

            builder.HasOne(d => d.PaymentType)
                .WithMany(p => p.Transactions)
                .HasForeignKey(d => d.PaymentTypeId)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_Transactions_PaymentTypes");

            builder.HasOne(d => d.TransactionType)
                .WithMany(p => p.Transactions)
                .HasForeignKey(d => d.TransactionTypeId)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_Transactions_TransactionTypes");

            builder.ToTable("Transactions");
            base.Configure(builder);
        }
    }
}
