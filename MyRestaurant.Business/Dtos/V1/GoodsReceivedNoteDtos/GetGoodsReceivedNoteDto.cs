namespace MyRestaurant.Business.Dtos.V1
{
    public class GetGoodsReceivedNoteDto : GoodsReceivedNoteDto
    {
        public long Id { get; set; }
        public string PurchaseOrderNumber { get; set; } = default!;
        public string PaymentTypeName { get; set; } = default!;
        public string ReceivedUserName { get; set; } = default!;
        public string ApprovalStatus { get; set; } = default!;
        public Guid ApprovedBy { get; set; }
        public string? ApprovedUserName { get; set; }
        public DateTime ApprovedDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
