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
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).UseIdentityColumn();
            builder.Property(t => t.Code).HasColumnType("varchar(20)").IsRequired();
            builder.HasIndex(t => t.Code, "IX_UnitOfMeasures").IsUnique();
            builder.Property(t => t.Description).HasColumnType("varchar(50)");
            builder.ToTable("UnitOfMeasures");

            base.Configure(builder);
        }
    }
}
