using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRestaurant.Core.Configurations.Base;
using MyRestaurant.Models;

namespace MyRestaurant.Core.Configurations.Mapping
{
    internal class TransactionTypeMapping : TransactionTypeMappingBase
    {
        public override void Configure(EntityTypeBuilder<TransactionType> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).UseIdentityColumn();
            builder.Property(e => e.Type).HasColumnType("varchar(50)").IsRequired().HasMaxLength(50);
            builder.ToTable("TransactionTypes");
            base.Configure(builder);
        }
    }
}
