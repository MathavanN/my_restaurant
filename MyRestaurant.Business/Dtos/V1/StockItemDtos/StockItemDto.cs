namespace MyRestaurant.Business.Dtos.V1
{
    public class StockItemDto
    {
        public int TypeId { get; set; }
        public string Name { get; set; } = default!;
        public decimal ItemUnit { get; set; }
        public int UnitOfMeasureId { get; set; }
        public string? Description { get; set; }
    }
}
