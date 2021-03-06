using MyRestaurant.Core;
using MyRestaurant.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyRestaurant.Services.Tests
{
    public class GoodsReceivedNoteFreeItemInitializer
    {
        public static void Initialize(MyRestaurantContext context)
        {
            GoodsReceivedNoteInitializer.Initialize(context);

            if (!context.GoodsReceivedNoteFreeItems.Any())
            {
                var goodsReceivedNoteFreeItems = new List<GoodsReceivedNoteFreeItem>
                {
                    new GoodsReceivedNoteFreeItem {
                        Id = 1,
                        GoodsReceivedNoteId = 1,
                        ItemId = 8,
                        ItemUnitPrice = 250,
                        Quantity = 1,
                        Nbt = 0.1m,
                        Vat = 0.1m,
                        Discount = 0.1m
                    },
                    new GoodsReceivedNoteFreeItem
                    {
                        Id = 2,
                        GoodsReceivedNoteId = 2,
                        ItemId = 19,
                        ItemUnitPrice = 30,
                        Quantity = 5,
                        Nbt = 0.1m,
                        Vat = 0.1m,
                        Discount = 0.1m
                    }
                };

                context.GoodsReceivedNoteFreeItems.AddRange(goodsReceivedNoteFreeItems);
                context.SaveChanges();
            }
        }
    }
}
