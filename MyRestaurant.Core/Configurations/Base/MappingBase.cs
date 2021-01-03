using MyRestaurant.Models;

namespace MyRestaurant.Core.Configurations.Base
{
    public abstract class AuditMappingBase : BaseEntityTypeConfiguration<Audit> { }
    public abstract class RefreshTokenMappingBase : BaseEntityTypeConfiguration<RefreshToken> { }
    public abstract class ServiceTypeMappingBase : BaseEntityTypeConfiguration<ServiceType> { }
    public abstract class RestaurantInfoMappingBase : BaseEntityTypeConfiguration<RestaurantInfo> { }
    public abstract class SupplierMappingBase : BaseEntityTypeConfiguration<Supplier> { }
    public abstract class UnitOfMeasureMappingBase : BaseEntityTypeConfiguration<UnitOfMeasure> { }
    public abstract class StockTypeMappingBase : BaseEntityTypeConfiguration<StockType> { }
    public abstract class StockItemMappingBase : BaseEntityTypeConfiguration<StockItem> { }
}
