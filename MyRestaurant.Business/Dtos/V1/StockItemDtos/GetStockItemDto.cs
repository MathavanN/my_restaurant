namespace MyRestaurant.Business.Dtos.V1
{
    public class GetStockItemDto
    {
        public long Id { get; set; }
        public int TypeId { get; set; }
        public string StockType { get; set; } = default!;
        public string Name { get; set; } = default!;
        public decimal ItemUnit { get; set; }
        public int UnitOfMeasureId { get; set; }
        public string UnitOfMeasureCode { get; set; } = default!;
        public string Description { get; set; } = default!;
    }
}
