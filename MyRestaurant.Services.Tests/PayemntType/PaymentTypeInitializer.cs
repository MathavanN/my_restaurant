using MyRestaurant.Core;
using MyRestaurant.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyRestaurant.Services.Tests
{
    public class PaymentTypeInitializer
    {
        public static void Initialize(MyRestaurantContext context)
        {
            if (!context.PaymentTypes.Any())
            {
                var paymentTypes = new List<PaymentType>
                {
                    new PaymentType { Name = "Cash", CreditPeriod = 0 },
                    new PaymentType { Name = "Credit", CreditPeriod = 30 },
                    new PaymentType { Name = "Credit100", CreditPeriod = 100 },
                };

                context.PaymentTypes.AddRange(paymentTypes);
                context.SaveChanges();
            }
        }
    }
}
