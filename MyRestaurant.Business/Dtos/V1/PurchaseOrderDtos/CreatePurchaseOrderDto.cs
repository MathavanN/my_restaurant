namespace MyRestaurant.Business.Dtos.V1
{
    public class CreatePurchaseOrderDto
    {
        public long SupplierId { get; set; }
        public decimal Discount { get; set; }
        public string Description { get; set; }
    }
}
