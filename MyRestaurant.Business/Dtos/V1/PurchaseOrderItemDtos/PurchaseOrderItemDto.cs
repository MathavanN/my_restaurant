namespace MyRestaurant.Business.Dtos.V1
{
    public class PurchaseOrderItemDto
    {
        public long PurchaseOrderId { get; set; }
        public long ItemId { get; set; }
        public decimal ItemUnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
