using System;

namespace MyRestaurant.Business.Dtos.V1
{
    public class GetGoodsReceivedNoteDto
    {
        public long Id { get; set; }
        public long PurchaseOrderId { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public string InvoiceNumber { get; set; }
        public int PaymentTypeId { get; set; }
        public string PaymentTypeName { get; set; }
        public decimal Nbt { get; set; }
        public decimal Vat { get; set; }
        public decimal Discount { get; set; }
        public Guid ReceivedBy { get; set; }
        public string ReceivedUserName { get; set; }
        public DateTime ReceivedDate { get; set; }
        public string ApprovalStatus { get; set; }
        public Guid ApprovedBy { get; set; }
        public string ApprovedUserName { get; set; }
        public DateTime ApprovedDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
