using MyRestaurant.Core;
using MyRestaurant.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyRestaurant.Services.Tests
{
    public class RestaurantInfoInitializer
    {
        public static void Initialize(MyRestaurantContext context)
        {
            if (!context.PaymentTypes.Any())
            {
                var restaurantInfo = new List<RestaurantInfo>
                {
                    new RestaurantInfo
                    {
                        Id = 1,
                        Name = "Golden Dining",
                        Address = "Kandy Road, Kaithady",
                        City = "Jaffna",
                        Country = "Sri Lanka",
                        LandLine = "+9423454544",
                        Mobile = "+94567876786",
                        Email = "test@gmail.com"
                    }
                };

                context.RestaurantInfos.AddRange(restaurantInfo);
                context.SaveChanges();
            }
        }
    }
}
