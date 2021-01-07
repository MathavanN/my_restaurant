namespace MyRestaurant.Business.Dtos.V1
{
    public class EditStockItemDto
    {
        public int TypeId { get; set; }
        public string Name { get; set; }
        public int UnitOfMeasureId { get; set; }
        public string Description { get; set; }
    }
}
