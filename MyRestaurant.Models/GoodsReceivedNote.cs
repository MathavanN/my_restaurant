using System;
using System.Collections.Generic;

namespace MyRestaurant.Models
{
    public class GoodsReceivedNote : MyRestaurantObject
    {
        public GoodsReceivedNote()
        {
            GoodsReceivedNoteFreeItems = new HashSet<GoodsReceivedNoteFreeItem>();
            GoodsReceivedNoteItems = new HashSet<GoodsReceivedNoteItem>();
        }

        public long Id { get; set; }
        public long PurchaseOrderId { get; set; }
        public string InvoiceNumber { get; set; }
        public int PaymentTypeId { get; set; }
        public decimal Nbt { get; set; }
        public decimal Vat { get; set; }
        public decimal Discount { get; set; }
        public DateTime ReceivedDate { get; set; }
        public Guid ReceivedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedBy { get; set; }

        public virtual User ReceivedUser { get; set; }
        public virtual User CreatedUser { get; set; }
        public virtual PaymentType PaymentType { get; set; }
        public virtual PurchaseOrder PurchaseOrder { get; set; }
        public virtual ICollection<GoodsReceivedNoteFreeItem> GoodsReceivedNoteFreeItems { get; set; }
        public virtual ICollection<GoodsReceivedNoteItem> GoodsReceivedNoteItems { get; set; }
    }
}
