using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRestaurant.Core.Configurations.Base;
using MyRestaurant.Models;

namespace MyRestaurant.Core.Configurations.Mapping
{
    internal class AuditMapping : AuditMappingBase
    {
        public override void Configure(EntityTypeBuilder<Audit> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Action).HasColumnType("varchar(20)").IsRequired().HasMaxLength(20);
            builder.Property(t => t.TableName).HasColumnType("varchar(256)").IsRequired();
            builder.Property(t => t.Username).HasColumnType("varchar(256)").IsRequired();
            builder.ToTable("Audits");
            base.Configure(builder);
        }
    }
}
