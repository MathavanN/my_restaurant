namespace MyRestaurant.Business.Dtos.V1
{
    public class GetStockItemDto
    {
        public long Id { get; set; }
        public int TypeId { get; set; }
        public string StockType { get; set; }
        public string Name { get; set; }
        public decimal ItemUnit { get; set; }
        public int UnitOfMeasureId { get; set; }
        public string UnitOfMeasureCode { get; set; }
        public string Description { get; set; }
    }
}
