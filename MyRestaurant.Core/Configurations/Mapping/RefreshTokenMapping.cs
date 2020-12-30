using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRestaurant.Core.Configurations.Base;
using MyRestaurant.Models;

namespace MyRestaurant.Core.Configurations.Mapping
{
    internal class RefreshTokenMapping : RefreshTokenMappingBase
    {
        public override void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.UserId).IsRequired();
            builder.Property(t => t.Token).IsRequired();
            builder.Property(t => t.Expires).IsRequired();
            builder.Property(t => t.Created).IsRequired();
            builder.Property(t => t.CreatedByIp).HasColumnType("varchar(100)").IsRequired();
            builder.Property(t => t.Revoked);
            builder.Property(t => t.RevokedByIp).HasColumnType("varchar(100)");
            builder.Property(t => t.ReplacedByToken);
            builder.ToTable("RefreshTokens");

            builder.HasOne(t => t.User).WithMany(r => r.RefreshTokens).HasForeignKey(f => f.UserId);
            base.Configure(builder);
        }
    }
}
