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
            if (!context.UnitOfMeasures.Any())
            {
                var unitOfMeasures = new List<UnitOfMeasure>
                {
                    new UnitOfMeasure { Id = 1, Code = "kg", Description = "" },
                    new UnitOfMeasure { Id = 2, Code = "g", Description = "" },
                    new UnitOfMeasure { Id = 3, Code = "l", Description = "" },
                    new UnitOfMeasure { Id = 4, Code = "ml", Description = "" },
                    new UnitOfMeasure { Id = 5, Code = "none", Description = "" }
                };
                context.UnitOfMeasures.AddRange(unitOfMeasures);
                context.SaveChanges();
            }

            if (!context.StockTypes.Any())
            {
                var stockTypes = new List<StockType>
                {
                    new StockType { Id = 1, Type = "Grocery" },
                    new StockType { Id = 2, Type = "Beverage" },
                    new StockType { Id = 3, Type = "Stationery" }
                };
                context.StockTypes.AddRange(stockTypes);
                context.SaveChanges();
            }

            if (!context.StockItems.Any())
            {
                var stockItems = new List<StockItem>
                {
                    new StockItem { Id = 1, TypeId = 1, Name = "Rice", ItemUnit = 10, UnitOfMeasureId = 1 },
                    new StockItem { Id = 2, TypeId = 1, Name = "Chilli Powder", ItemUnit = 250, UnitOfMeasureId = 2 },
                    new StockItem { Id = 3, TypeId = 2, Name = "Water", ItemUnit = 1, UnitOfMeasureId = 3 },
                    new StockItem { Id = 4, TypeId = 3, Name = "Blue Pen", ItemUnit = 1, UnitOfMeasureId = 5 },
                };
                context.StockItems.AddRange(stockItems);
                context.SaveChanges();
            }
        }
    }
}
