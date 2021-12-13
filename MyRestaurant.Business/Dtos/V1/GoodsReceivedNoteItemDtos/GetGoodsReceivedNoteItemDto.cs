namespace MyRestaurant.Business.Dtos.V1
{
    public class GetGoodsReceivedNoteItemDto : GoodsReceivedNoteItemDto
    {
        public long Id { get; set; }
        public int ItemTypeId { get; set; }
        public string ItemTypeName { get; set; } = default!;
        public string ItemName { get; set; } = default!;
        public decimal ItemUnit { get; set; }
        public string UnitOfMeasureCode { get; set; } = default!;
    }
}
