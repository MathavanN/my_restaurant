using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRestaurant.Core.Configurations.Base;
using MyRestaurant.Models;

namespace MyRestaurant.Core.Configurations.Mapping
{
    internal class ServiceTypeMapping : ServiceTypeMappingBase
    {
        public override void Configure(EntityTypeBuilder<ServiceType> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).UseIdentityColumn();
            builder.Property(e => e.Type).HasColumnType("varchar(20)").IsRequired().HasMaxLength(20);
            builder.ToTable("ServiceTypes");
            base.Configure(builder);
        }
    }
}
