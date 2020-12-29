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
            builder.Property(t => t.Username).HasColumnType("nvarchar(256)").IsRequired();
            builder.ToTable("Audits");
            base.Configure(builder);
        }
    }

    internal class RefreshTokenMapping : RefreshTokenMappingBase
    {
        public override void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.UserId).IsRequired();
            builder.Property(t => t.Token).IsRequired();
            builder.Property(t => t.Expires).IsRequired();
            builder.Property(t => t.IsExpired).IsRequired();
            builder.Property(t => t.Created).IsRequired();
            builder.Property(t => t.CreatedByIp).HasColumnType("varchar(100)").IsRequired();
            builder.Property(t => t.Revoked);
            builder.Property(t => t.RevokedByIp).HasColumnType("varchar(100)");
            builder.Property(t => t.ReplacedByToken).HasColumnType("varchar(100)");
            builder.ToTable("RefreshTokens");

            builder.HasOne(t => t.User).WithMany(r => r.RefreshTokens).HasForeignKey(f => f.UserId);
            base.Configure(builder);
        }
    }
}
