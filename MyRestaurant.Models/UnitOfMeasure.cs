namespace MyRestaurant.Models
{
    public class UnitOfMeasure : MyRestaurantObject
    {
        public UnitOfMeasure()
        {
            StockItems = new HashSet<StockItem>();
            Code = default!;
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<StockItem> StockItems { get; set; }
    }
}
