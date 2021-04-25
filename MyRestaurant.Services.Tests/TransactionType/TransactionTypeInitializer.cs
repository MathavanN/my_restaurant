using MyRestaurant.Core;
using MyRestaurant.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyRestaurant.Services.Tests
{
    public class TransactionTypeInitializer
    {
        public static void Initialize(MyRestaurantContext context)
        {
            if (!context.TransactionTypes.Any())
            {
                var transactionTypes = new List<TransactionType>
                {
                    new TransactionType { Type = "Food" },
                    new TransactionType { Type = "Transportation" },
                    new TransactionType { Type = "Shopping" },
                    //new TransactionType { Type = "Mortgage/Rent" },
                    new TransactionType { Type = "Mortgage" },
                    new TransactionType { Type = "Clothing" },
                    new TransactionType { Type = "Housing" },
                    new TransactionType { Type = "Utilities" },
                    new TransactionType { Type = "Bills" },
                    new TransactionType { Type = "Personal Care" },
                    new TransactionType { Type = "Extra Income" },
                    new TransactionType { Type = "Miscellaneous" },
                    new TransactionType { Type = "Health Care" },
                    new TransactionType { Type = "Interests" },
                    new TransactionType { Type = "Insurance" },
                    new TransactionType { Type = "Business" },
                    new TransactionType { Type = "Tax" },
                    //new TransactionType { Type = "Salary" },
                    new TransactionType { Type = "Education" }
                };

                context.TransactionTypes.AddRange(transactionTypes);
                context.SaveChanges();
            }
        }
    }
}
