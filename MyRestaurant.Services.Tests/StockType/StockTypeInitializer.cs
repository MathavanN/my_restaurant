using MyRestaurant.Core;
using MyRestaurant.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyRestaurant.Services.Tests
{
    public class StockTypeInitializer
    {
        public static void Initialize(MyRestaurantContext context)
        {
            if (!context.StockTypes.Any())
            {
                var stockTypes = new List<StockType>
                {
                    new StockType { Type = "Grocery", Description = "" },
                    new StockType { Type = "Beverage", Description = "" },
                    new StockType { Type = "Stationery" }
                };
                context.StockTypes.AddRange(stockTypes);
                context.SaveChanges();
            }
        }
    }
}
