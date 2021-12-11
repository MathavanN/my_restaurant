namespace MyRestaurant.Business.Dtos.V1
{
    public class GetGoodsReceivedNoteFreeItemDto
    {
        public long Id { get; set; }
        public long GoodsReceivedNoteId { get; set; }
        public int ItemTypeId { get; set; }
        public string ItemTypeName { get; set; } = default!;
        public long ItemId { get; set; }
        public string ItemName { get; set; } = default!;
        public decimal ItemUnit { get; set; }
        public string UnitOfMeasureCode { get; set; } = default!;
        public decimal ItemUnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Nbt { get; set; }
        public decimal Vat { get; set; }
        public decimal Discount { get; set; }
    }
}
