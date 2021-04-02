using System;

namespace MyRestaurant.Business.Dtos.V1
{
    public class GetPurchaseOrderDto
    {
        public long Id { get; set; }
        public string OrderNumber { get; set; }
        public long SupplierId { get; set; }
        public string SupplierName { get; set; }
        public Guid RequestedUserId { get; set; }
        public string RequestedUserName { get; set; }
        public DateTime RequestedDate { get; set; }
        public string ApprovalStatus { get; set; }
        public Guid ApprovedUserId { get; set; }
        public string ApprovedUserName { get; set; }
        public DateTime ApprovedDate { get; set; }
        public string Description { get; set; }
    }
}
