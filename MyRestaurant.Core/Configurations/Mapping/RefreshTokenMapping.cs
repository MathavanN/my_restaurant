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
            builder.HasKey(e => e.Id);
            builder.Property(e => e.UserId).IsRequired();
            builder.Property(e => e.Token).IsRequired();
            builder.Property(e => e.Expires).IsRequired();
            builder.Property(e => e.Created).IsRequired();
            builder.Property(e => e.CreatedByIp).HasColumnType("varchar(100)").IsRequired();
            builder.Property(e => e.Revoked);
            builder.Property(e => e.RevokedByIp).HasColumnType("varchar(100)");
            builder.Property(e => e.ReplacedByToken);
            builder.ToTable("RefreshTokens");

            builder.HasOne(d => d.User)
                .WithMany(p => p.RefreshTokens)
                .HasForeignKey(d => d.UserId);

            base.Configure(builder);
        }
    }
}
