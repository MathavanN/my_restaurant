﻿namespace MyRestaurant.Business.Dtos.V1
{
    public class GetStockItemDto
    {
        public long Id { get; set; }
        public string StockType { get; set; }
        public string Name { get; set; }
        public string UnitOfMeasureName { get; set; }
        public string Description { get; set; }
    }
}