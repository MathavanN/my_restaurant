using MyRestaurant.Core;
using MyRestaurant.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyRestaurant.Services.Tests
{
    public class PurchaseOrderItemInitializer
    {
        public static void Initialize(MyRestaurantContext context)
        {
            StockItemInitializer.Initialize(context);
            PurchaseOrderInitializer.Initialize(context);

            if (!context.PurchaseOrderItems.Any())
            {
                var items = new List<PurchaseOrderItem>
                {
                    new PurchaseOrderItem { PurchaseOrderId = 1, ItemId = 1, ItemUnitPrice = 540, Quantity = 5 },
                    new PurchaseOrderItem { PurchaseOrderId = 2, ItemId = 2, ItemUnitPrice = 30, Quantity = 10 },
                    new PurchaseOrderItem { PurchaseOrderId = 1, ItemId = 3, ItemUnitPrice = 50, Quantity = 5 },
                    new PurchaseOrderItem { PurchaseOrderId = 2, ItemId = 4, ItemUnitPrice = 260, Quantity = 6 },
                    new PurchaseOrderItem { PurchaseOrderId = 3, ItemId = 15, ItemUnitPrice = 8500, Quantity = 1 },
                    new PurchaseOrderItem { PurchaseOrderId = 4, ItemId = 21, ItemUnitPrice = 3240, Quantity = 3 },
                    new PurchaseOrderItem { PurchaseOrderId = 5, ItemId = 29, ItemUnitPrice = 450, Quantity = 40 },
                    new PurchaseOrderItem { PurchaseOrderId = 6, ItemId = 28, ItemUnitPrice = 320, Quantity = 20 }
                };
                context.PurchaseOrderItems.AddRange(items);
                context.SaveChanges();
            }
        }
    }
}
