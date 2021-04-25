using MyRestaurant.Core;
using MyRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyRestaurant.Services.Tests
{
    public class TransactionInitializer
    {
        public static void Initialize(MyRestaurantContext context)
        {
            TransactionTypeInitializer.Initialize(context);

            PaymentTypeInitializer.Initialize(context);

            if (!context.Transactions.Any())
            {
                var transactions = new List<Transaction>
                {
                    new Transaction {
                        TransactionTypeId = 1,
                        PaymentTypeId = 2,
                        Date = DateTime.Now.AddDays(-10),
                        Description = "Peanuts in Coke",
                        Amount = 6.5m,
                        Cashflow = Cashflow.Expense,
                        CreatedAt = DateTime.Now
                    },
                    new Transaction {
                        TransactionTypeId = 10,
                        PaymentTypeId = 1,
                        Date = DateTime.Now.AddDays(-5),
                        Description = "Income from sale",
                        Amount = 110.5m,
                        Cashflow = Cashflow.Income,
                        CreatedAt = DateTime.Now
                    }
                };

                context.Transactions.AddRange(transactions);
                context.SaveChanges();
            }
        }
    }
}
