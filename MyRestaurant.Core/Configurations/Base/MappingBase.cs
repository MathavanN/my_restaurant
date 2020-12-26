using MyRestaurant.Models;

namespace MyRestaurant.Core.Configurations.Base
{
    public abstract class AuditMappingBase : BaseEntityTypeConfiguration<Audit> { }
    public abstract class ServiceTypeMappingBase : BaseEntityTypeConfiguration<ServiceType> { }
}
