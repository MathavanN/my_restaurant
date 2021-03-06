using MyRestaurant.Core;
using MyRestaurant.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyRestaurant.Services.Tests
{
    public class ServiceTypeInitializer
    {
        public static void Initialize(MyRestaurantContext context)
        {
            if (!context.ServiceTypes.Any())
            {
                var serviceTypes = new List<ServiceType>
                {
                    new ServiceType { Type = "Take Away" },
                    new ServiceType { Type = "Dine In" }
                };

                context.ServiceTypes.AddRange(serviceTypes);
                context.SaveChanges();
            }
        }
    }
}
