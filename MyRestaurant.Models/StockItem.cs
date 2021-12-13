namespace MyRestaurant.Models
{
    public class StockItem : MyRestaurantObject
    {
        public StockItem()
        {
            GoodsReceivedNoteFreeItems = new HashSet<GoodsReceivedNoteFreeItem>();
            GoodsReceivedNoteItems = new HashSet<GoodsReceivedNoteItem>();
            PurchaseOrderItems = new HashSet<PurchaseOrderItem>();
            Type = default!;
            UnitOfMeasure = default!;
            Name = default!;
        }

        public long Id { get; set; }
        public int TypeId { get; set; }
        public string Name { get; set; }
        public int UnitOfMeasureId { get; set; }
        public decimal ItemUnit { get; set; }
        public string? Description { get; set; }

        public virtual StockType Type { get; set; }
        public virtual UnitOfMeasure UnitOfMeasure { get; set; }
        public virtual ICollection<GoodsReceivedNoteFreeItem> GoodsReceivedNoteFreeItems { get; set; }
        public virtual ICollection<GoodsReceivedNoteItem> GoodsReceivedNoteItems { get; set; }
        public virtual ICollection<PurchaseOrderItem> PurchaseOrderItems { get; set; }
    }
}
