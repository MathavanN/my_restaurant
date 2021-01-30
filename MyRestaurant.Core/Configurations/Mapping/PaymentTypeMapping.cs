using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRestaurant.Core.Configurations.Base;
using MyRestaurant.Models;

namespace MyRestaurant.Core.Configurations.Mapping
{
    internal class PaymentTypeMapping : PaymentTypeMappingBase
    {
        public override void Configure(EntityTypeBuilder<PaymentType> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).UseIdentityColumn();
            builder.Property(e => e.Name).HasColumnType("varchar(30)").IsRequired();
            builder.HasIndex(e => e.Name, "IX_PaymentTypes").IsUnique();
            builder.Property(e => e.CreditPeriod);
            builder.ToTable("PaymentTypes");

            base.Configure(builder);
        }
    }
}
