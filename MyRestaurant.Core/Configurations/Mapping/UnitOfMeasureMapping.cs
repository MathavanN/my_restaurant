using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRestaurant.Core.Configurations.Base;
using MyRestaurant.Models;

namespace MyRestaurant.Core.Configurations.Mapping
{
    internal class UnitOfMeasureMapping : UnitOfMeasureMappingBase
    {
        public override void Configure(EntityTypeBuilder<UnitOfMeasure> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).UseIdentityColumn();
            builder.Property(e => e.Code).HasColumnType("varchar(20)").IsRequired();
            builder.HasIndex(e => e.Code, "IX_UnitOfMeasures").IsUnique();
            builder.Property(e => e.Description).HasColumnType("varchar(50)");
            builder.ToTable("UnitOfMeasures");

            base.Configure(builder);
        }
    }
}
