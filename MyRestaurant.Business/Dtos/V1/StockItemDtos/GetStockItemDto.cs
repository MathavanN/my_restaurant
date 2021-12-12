namespace MyRestaurant.Business.Dtos.V1
{
    public class GetStockItemDto : StockItemDto
    {
        public long Id { get; set; }
        public string StockType { get; set; } = default!;
        public string UnitOfMeasureCode { get; set; } = default!;
    }
}
