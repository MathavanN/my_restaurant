namespace MyRestaurant.Business.Dtos.V1
{
    public class ApprovalPurchaseOrderDto
    {
        public string ApprovalStatus { get; set; } = default!;
        public string ApprovalReason { get; set; } = default!;
    }
}
