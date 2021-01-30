using System;
using System.Collections.Generic;

namespace MyRestaurant.Models
{
    public class PurchaseOrder : MyRestaurantObject
    {
        public PurchaseOrder()
        {
            PurchaseOrderItems = new HashSet<PurchaseOrderItem>();
        }
        public long Id { get; set; }
        public string OrderNumber { get; set; }
        public long SupplierId { get; set; }
        public Guid RequestedBy { get; set; }
        public DateTime RequestedDate { get; set; }
        public PurchaseOrderStatus ApprovalStatus { get; set; }
        public Guid? ApprovedBy { get; set; }
        public string ApprovalReason { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string Description { get; set; }

        public virtual Supplier Supplier { get; set; }
        public virtual User RequestedUser { get; set; }
        public virtual User ApprovedUser { get; set; }
        public virtual GoodsReceivedNote GoodsReceivedNote { get; set; }
        public virtual ICollection<PurchaseOrderItem> PurchaseOrderItems { get; set; }
    }

    public enum PurchaseOrderStatus
    {
        Pending,
        Approved,
        Rejected,
        Cancelled,
    }
}
