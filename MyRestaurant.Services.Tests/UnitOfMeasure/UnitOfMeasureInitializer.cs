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
                    new UnitOfMeasure { Code = "kg", Description = "kg units" },
                    new UnitOfMeasure { Code = "g", Description = "g units" },
                    new UnitOfMeasure { Code = "ml", Description = "ml units" },
                    new UnitOfMeasure { Code = "l", Description = "l units" },
                    new UnitOfMeasure { Code = "none", Description = "" }
                };
                context.UnitOfMeasures.AddRange(unitOfMeasures);
                context.SaveChanges();
            }
        }
    }
}
