namespace MyRestaurant.Models
{
    public class PurchaseOrder : MyRestaurantObject
    {
        public PurchaseOrder()
        {
            GoodsReceivedNotes = new HashSet<GoodsReceivedNote>();
            PurchaseOrderItems = new HashSet<PurchaseOrderItem>();
            Supplier = default!;
            RequestedUser = default!;
            ApprovedUser = default!;
            OrderNumber = default!;
        }

        public long Id { get; set; }
        public string OrderNumber { get; set; }
        public long SupplierId { get; set; }
        public Guid RequestedBy { get; set; }
        public DateTime RequestedDate { get; set; }
        public Status ApprovalStatus { get; set; }
        public Guid? ApprovedBy { get; set; }
        public string? ApprovalReason { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string? Description { get; set; }

        public virtual Supplier Supplier { get; set; }
        public virtual User RequestedUser { get; set; }
        public virtual User ApprovedUser { get; set; }
        public virtual ICollection<GoodsReceivedNote> GoodsReceivedNotes { get; set; }
        public virtual ICollection<PurchaseOrderItem> PurchaseOrderItems { get; set; }
    }

    public enum Status
    {
        Pending = 0,
        Approved = 1,
        Rejected = 2,
        Cancelled = 3,
    }
}
