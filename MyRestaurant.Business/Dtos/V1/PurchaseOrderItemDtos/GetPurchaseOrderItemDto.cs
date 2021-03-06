﻿namespace MyRestaurant.Business.Dtos.V1
{
    public class GetPurchaseOrderItemDto
    {
        public long Id { get; set; }
        public long PurchaseOrderId { get; set; }
        public int ItemTypeId { get; set; }
        public string ItemTypeName { get; set; }
        public long ItemId { get; set; }
        public string ItemName { get; set; }
        public decimal ItemUnit { get; set; }
        public string UnitOfMeasureCode { get; set; }
        public decimal ItemUnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
