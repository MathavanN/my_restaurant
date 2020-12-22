using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRestaurant.Core.Configurations.Base;
using MyRestaurant.Core.Models;

namespace MyRestaurant.Core.Configurations.Mapping
{
    internal class ServiceTypeMapping : ServiceTypeMappingBase
    {
        public override void Configure(EntityTypeBuilder<ServiceType> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).UseIdentityColumn();
            builder.Property(t => t.Type).HasColumnType("varchar(20)").IsRequired().HasMaxLength(20);
            builder.ToTable("ServiceTypes");
            base.Configure(builder);
        }
    }
}
