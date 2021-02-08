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
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).UseIdentityColumn();
            builder.Property(e => e.Name).HasColumnType("varchar(256)").IsRequired();
            builder.Property(e => e.Address1).HasColumnType("varchar(256)").IsRequired();
            builder.Property(e => e.Address2).HasColumnType("varchar(256)");
            builder.Property(e => e.City).HasColumnType("varchar(100)").IsRequired();
            builder.Property(e => e.Country).HasColumnType("varchar(100)").IsRequired();
            builder.Property(e => e.Telephone1).HasColumnType("varchar(20)");
            builder.Property(e => e.Telephone2).HasColumnType("varchar(20)");
            builder.Property(e => e.Fax).HasColumnType("varchar(20)");
            builder.Property(e => e.Email).HasColumnType("varchar(256)");
            builder.Property(e => e.ContactPerson).HasColumnType("varchar(256)");
            builder.ToTable("Suppliers");

            base.Configure(builder);
        }
    }
}
