namespace MyRestaurant.Models
{
    public class GoodsReceivedNoteItem : MyRestaurantObject
    {
        public long Id { get; set; }
        public long GoodsReceivedNoteId { get; set; }
        public long ItemId { get; set; }
        public decimal ItemUnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Nbt { get; set; }
        public decimal Vat { get; set; }
        public decimal Discount { get; set; }

        public virtual GoodsReceivedNote GoodsReceivedNote { get; set; }
        public virtual StockItem Item { get; set; }
    }
}
