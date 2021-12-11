namespace MyRestaurant.Business.Dtos.V1
{
    public class GetPurchaseOrderItemDto
    {
        public long Id { get; set; }
        public long PurchaseOrderId { get; set; }
        public int ItemTypeId { get; set; }
        public string ItemTypeName { get; set; } = default!;
        public long ItemId { get; set; }
        public string ItemName { get; set; } = default!;
        public decimal ItemUnit { get; set; }
        public string UnitOfMeasureCode { get; set; } = default!;
        public decimal ItemUnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
