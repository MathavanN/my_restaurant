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
                    new StockType { Id = 1, Type = "Grocery", Description = "" },
                    new StockType { Id = 2, Type = "Beverage", Description = "" }
                };
                context.StockTypes.AddRange(stockTypes);
                context.SaveChanges();
            }
        }
    }
}
