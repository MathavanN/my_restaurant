namespace MyRestaurant.Business.Dtos.V1
{
    public class GetGoodsReceivedNoteFreeItemDto
    {
        public long Id { get; set; }
        public long GoodsReceivedNoteId { get; set; }
        public int ItemTypeId { get; set; }
        public string ItemTypeName { get; set; }
        public long ItemId { get; set; }
        public string ItemName { get; set; }
        public decimal ItemUnit { get; set; }
        public string UnitOfMeasureCode { get; set; }
        public decimal ItemUnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Nbt { get; set; }
        public decimal Vat { get; set; }
        public decimal Discount { get; set; }
    }
}
