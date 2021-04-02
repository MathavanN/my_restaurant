using System.Collections.Generic;

namespace MyRestaurant.Models
{
    public class StockType : MyRestaurantObject
    {
        public StockType()
        {
            StockItems = new HashSet<StockItem>();
        }

        public int Id { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }

        public virtual ICollection<StockItem> StockItems { get; set; }
    }
}
