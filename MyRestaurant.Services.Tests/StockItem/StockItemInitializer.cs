using MyRestaurant.Core;
using MyRestaurant.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyRestaurant.Services.Tests
{
    public class StockItemInitializer
    {
        public static void Initialize(MyRestaurantContext context)
        {
            UnitOfMeasureInitializer.Initialize(context);

            StockTypeInitializer.Initialize(context);

            if (!context.StockItems.Any())
            {
                var stockItems = new List<StockItem>
                {
                    new StockItem { TypeId = 1, Name = "Rice", ItemUnit = 10, UnitOfMeasureId = 1 },
                    new StockItem { TypeId = 1, Name = "Chilli Powder", ItemUnit = 250, UnitOfMeasureId = 2 },
                    new StockItem { TypeId = 2, Name = "Water", ItemUnit = 1, UnitOfMeasureId = 4 },
                    new StockItem { TypeId = 3, Name = "Blue Pen", ItemUnit = 10, UnitOfMeasureId = 5 },
                    new StockItem { TypeId = 3, Name = "Oats", ItemUnit = 186, UnitOfMeasureId = 1 },
                    new StockItem { TypeId = 1, Name = "Cheese", ItemUnit = 88, UnitOfMeasureId = 2 },
                    new StockItem { TypeId = 1, Name = "Pasta", ItemUnit = 52, UnitOfMeasureId = 1 },
                    new StockItem { TypeId = 1, Name = "Chips", ItemUnit = 21, UnitOfMeasureId = 1 },
                    new StockItem { TypeId = 1, Name = "Nut butter", ItemUnit = 167, UnitOfMeasureId = 1 },
                    new StockItem { TypeId = 1, Name = "Popcorn", ItemUnit = 101, UnitOfMeasureId = 1 },
                    new StockItem { TypeId = 3, Name = "Mouse", ItemUnit = 5, UnitOfMeasureId = 5 },
                    new StockItem { TypeId = 1, Name = "Sausages", ItemUnit = 72, UnitOfMeasureId = 1 },
                    new StockItem { TypeId = 1, Name = "Flour ", ItemUnit = 99, UnitOfMeasureId = 1 },
                    new StockItem { TypeId = 1, Name = "Chips Large", ItemUnit = 116, UnitOfMeasureId = 1 },
                    new StockItem { TypeId = 3, Name = "Table Large", ItemUnit = 1, UnitOfMeasureId = 5 },
                    new StockItem { TypeId = 2, Name = "Vinegar", ItemUnit = 125, UnitOfMeasureId = 4 },
                    new StockItem { TypeId = 2, Name = "Ketchup", ItemUnit = 199, UnitOfMeasureId = 3 },
                    new StockItem { TypeId = 1, Name = "Mustard", ItemUnit = 73, UnitOfMeasureId = 2 },
                    new StockItem { TypeId = 1, Name = "Butter", ItemUnit = 60, UnitOfMeasureId = 2 },
                    new StockItem { TypeId = 1, Name = "Dried fruit", ItemUnit = 120, UnitOfMeasureId = 2 },
                    new StockItem { TypeId = 3, Name = "Table", ItemUnit = 2, UnitOfMeasureId = 5 },
                    new StockItem { TypeId = 2, Name = "Cooking Oil", ItemUnit = 148, UnitOfMeasureId = 4 },
                    new StockItem { TypeId = 1, Name = "Garlic", ItemUnit = 143, UnitOfMeasureId = 2 },
                    new StockItem { TypeId = 3, Name = "Towels", ItemUnit = 20, UnitOfMeasureId = 5 },
                    new StockItem { TypeId = 3, Name = "Gloves", ItemUnit = 30, UnitOfMeasureId = 5 },
                    new StockItem { TypeId = 3, Name = "Keyboard", ItemUnit = 5, UnitOfMeasureId = 5 },
                    new StockItem { TypeId = 2, Name = "Honey", ItemUnit = 87, UnitOfMeasureId = 3 },
                    new StockItem { TypeId = 1, Name = "Sugar", ItemUnit = 142, UnitOfMeasureId = 1 },
                    new StockItem { TypeId = 1, Name = "Tuna", ItemUnit = 177, UnitOfMeasureId = 2 }
                };
                context.StockItems.AddRange(stockItems);
                context.SaveChanges();
            }
        }
    }
}
