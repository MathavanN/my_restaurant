using System;

namespace MyRestaurant.Business.Dtos.V1
{
    public class EditGoodsReceivedNoteDto
    {
        public long PurchaseOrderId { get; set; }
        public string InvoiceNumber { get; set; }
        public int PaymentTypeId { get; set; }
        public decimal Nbt { get; set; }
        public decimal Vat { get; set; }
        public decimal Discount { get; set; }
        public DateTime ReceivedDate { get; set; }
    }
}
