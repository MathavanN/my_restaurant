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
    public abstract class PurchaseOrderMappingBase : BaseEntityTypeConfiguration<PurchaseOrder> { }
    public abstract class PurchaseOrderItemMappingBase : BaseEntityTypeConfiguration<PurchaseOrderItem> { }
    public abstract class PaymentTypeMappingBase : BaseEntityTypeConfiguration<PaymentType> { }
    public abstract class GoodsReceivedNoteMappingBase : BaseEntityTypeConfiguration<GoodsReceivedNote> { }
    public abstract class GoodsReceivedNoteItemMappingBase : BaseEntityTypeConfiguration<GoodsReceivedNoteItem> { }
    public abstract class GoodsReceivedNoteFreeItemMappingBase : BaseEntityTypeConfiguration<GoodsReceivedNoteFreeItem> { }
    public abstract class TransactionTypeMappingBase : BaseEntityTypeConfiguration<TransactionType> { }
    public abstract class TransactionMappingBase : BaseEntityTypeConfiguration<Transaction> { }
}
