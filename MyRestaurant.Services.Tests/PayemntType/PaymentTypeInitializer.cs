using MyRestaurant.Core;
using MyRestaurant.Models;
using System.Linq;

namespace MyRestaurant.Services.Tests
{
    public class PaymentTypeInitializer
    {
        public static void Initialize(MyRestaurantContext context)
        {
            if (context.PaymentTypes.Any())
            {
                return;
            }

            Seed(context);
        }

        private static void Seed(MyRestaurantContext context)
        {
            var paymentTypes = new[]
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
