namespace MyRestaurant.Models
{
    public class PurchaseOrderItem : MyRestaurantObject
    {
        public PurchaseOrderItem()
        {
            Item = default!;
            PurchaseOrder = default!;
        }

        public long Id { get; set; }
        public long PurchaseOrderId { get; set; }
        public long ItemId { get; set; }
        public decimal ItemUnitPrice { get; set; }
        public int Quantity { get; set; }

        public virtual StockItem Item { get; set; }
        public virtual PurchaseOrder PurchaseOrder { get; set; }
    }
}
