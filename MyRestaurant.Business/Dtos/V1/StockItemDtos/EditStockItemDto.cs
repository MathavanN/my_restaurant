namespace MyRestaurant.Business.Dtos.V1
{
    public class EditStockItemDto
    {
        public int TypeId { get; set; }
        public string Name { get; set; } = default!;
        public int UnitOfMeasureId { get; set; }
        public decimal ItemUnit { get; set; }
        public string Description { get; set; } = default!;
    }
}
