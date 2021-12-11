namespace MyRestaurant.Models
{
    public class GoodsReceivedNote : MyRestaurantObject
    {
        public GoodsReceivedNote()
        {
            GoodsReceivedNoteFreeItems = new HashSet<GoodsReceivedNoteFreeItem>();
            GoodsReceivedNoteItems = new HashSet<GoodsReceivedNoteItem>();
            ReceivedUser = default!;
            ApprovedUser = default!;
            PaymentType = default!;
            PurchaseOrder = default!;
        }

        public long Id { get; set; }
        public long PurchaseOrderId { get; set; }
        public string? InvoiceNumber { get; set; }
        public int PaymentTypeId { get; set; }
        public decimal Nbt { get; set; }
        public decimal Vat { get; set; }
        public decimal Discount { get; set; }
        public Guid ReceivedBy { get; set; }
        public DateTime ReceivedDate { get; set; }
        public Guid? ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public Status ApprovalStatus { get; set; }
        public string? ApprovalReason { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual User ReceivedUser { get; set; }
        public virtual User ApprovedUser { get; set; }
        public virtual PaymentType PaymentType { get; set; }
        public virtual PurchaseOrder PurchaseOrder { get; set; }
        public virtual ICollection<GoodsReceivedNoteFreeItem> GoodsReceivedNoteFreeItems { get; set; }
        public virtual ICollection<GoodsReceivedNoteItem> GoodsReceivedNoteItems { get; set; }
    }
}
