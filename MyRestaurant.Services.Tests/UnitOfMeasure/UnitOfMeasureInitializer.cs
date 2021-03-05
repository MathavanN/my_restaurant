using MyRestaurant.Core;
using MyRestaurant.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyRestaurant.Services.Tests
{
    public class UnitOfMeasureInitializer
    {
        public static void Initialize(MyRestaurantContext context)
        {
            if (!context.UnitOfMeasures.Any())
            {
                var unitOfMeasures = new List<UnitOfMeasure>
                {
                    new UnitOfMeasure { Id = 1, Code = "kg", Description = "kg units" },
                    new UnitOfMeasure { Id = 2, Code = "g", Description = "g units" },
                    new UnitOfMeasure { Id = 3, Code = "ml", Description = "ml units" }
                };
                context.UnitOfMeasures.AddRange(unitOfMeasures);
                context.SaveChanges();
            }
        }
    }
}
