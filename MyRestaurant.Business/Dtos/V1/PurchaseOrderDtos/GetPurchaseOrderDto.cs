namespace MyRestaurant.Business.Dtos.V1
{
    public class GetPurchaseOrderDto
    {
        public long Id { get; set; }
        public string OrderNumber { get; set; } = default!;
        public long SupplierId { get; set; }
        public string SupplierName { get; set; } = default!;
        public Guid RequestedUserId { get; set; }
        public string RequestedUserName { get; set; } = default!;
        public DateTime RequestedDate { get; set; }
        public string ApprovalStatus { get; set; } = default!;
        public Guid ApprovedUserId { get; set; }
        public string ApprovedUserName { get; set; } = default!;
        public DateTime ApprovedDate { get; set; }
        public string Description { get; set; } = default!;
    }
}
