using System;

namespace MyRestaurant.Business.Dtos.V1
{
    public class GetPurchaseOrderDto
    {
        public long Id { get; set; }
        public string OrderNumber { get; set; }
        public long SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string RequestedBy { get; set; }
        public DateTime RequestedDate { get; set; }
        public string ApprovalStatus { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime ApprovedDate { get; set; }
        public decimal Discount { get; set; }
        public string Description { get; set; }
    }
}
