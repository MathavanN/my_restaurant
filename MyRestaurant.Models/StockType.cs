namespace MyRestaurant.Models
{
    public class StockType : MyRestaurantObject
    {
        public StockType()
        {
            StockItems = new HashSet<StockItem>();
            Type = default!;
        }

        public int Id { get; set; }
        public string Type { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<StockItem> StockItems { get; set; }
    }
}
