namespace MyRestaurant.Business.Dtos.V1
{
    public class CreateGoodsReceivedNoteFreeItemDto
    {
        public long GoodsReceivedNoteId { get; set; }
        public long ItemId { get; set; }
        public decimal ItemUnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Nbt { get; set; }
        public decimal Vat { get; set; }
        public decimal Discount { get; set; }
    }
}
