namespace MyRestaurant.Models
{
    public class StockItem : MyRestaurantObject
    {
        public long Id { get; set; }
        public int TypeId { get; set; }
        public string Name { get; set; }
        public int UnitOfMeasureId { get; set; }
        public string Description { get; set; }

        public virtual StockType Type { get; set; }
        public virtual UnitOfMeasure UnitOfMeasure { get; set; }
    }
}
