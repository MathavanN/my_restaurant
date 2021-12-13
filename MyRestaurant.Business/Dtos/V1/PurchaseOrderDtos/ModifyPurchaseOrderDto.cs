namespace MyRestaurant.Business.Dtos.V1
{
    public class ModifyPurchaseOrderDto
    {
        public long SupplierId { get; set; }
        public string? Description { get; set; }
    }
}
