using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRestaurant.Core.Configurations.Base;
using MyRestaurant.Models;

namespace MyRestaurant.Core.Configurations.Mapping
{
    internal class SupplierMapping : SupplierMappingBase
    {
        public override void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).UseIdentityColumn();
            builder.Property(t => t.Name).HasColumnType("varchar(256)").IsRequired();
            builder.Property(t => t.Address1).HasColumnType("varchar(256)").IsRequired();
            builder.Property(t => t.Address2).HasColumnType("varchar(256)");
            builder.Property(t => t.City).HasColumnType("varchar(100)").IsRequired();
            builder.Property(t => t.Country).HasColumnType("varchar(100)").IsRequired();
            builder.Property(t => t.Telephone1).HasColumnType("varchar(20)");
            builder.Property(t => t.Telephone2).HasColumnType("varchar(20)");
            builder.Property(t => t.Fax).HasColumnType("varchar(20)");
            builder.Property(t => t.Email).HasColumnType("varchar(256)");
            builder.Property(t => t.ContactPerson).HasColumnType("varchar(256)");
            builder.ToTable("Suppliers");

            base.Configure(builder);
        }
    }
}
