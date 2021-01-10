namespace MyRestaurant.Business.Dtos.V1
{
    public class CreateStockItemDto
    {
        public int TypeId { get; set; }
        public string Name { get; set; }
        public int UnitOfMeasureId { get; set; }
        public decimal ItemUnit { get; set; }
        public string Description { get; set; }
    }
}
