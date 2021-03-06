using MyRestaurant.Core;
using MyRestaurant.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyRestaurant.Services.Tests
{
    public class GoodsReceivedNoteItemInitializer
    {
        public static void Initialize(MyRestaurantContext context)
        {
            GoodsReceivedNoteInitializer.Initialize(context);

            if (!context.GoodsReceivedNoteItems.Any())
            {
                var goodsReceivedNoteItems = new List<GoodsReceivedNoteItem>
                {
                    new GoodsReceivedNoteItem {
                        GoodsReceivedNoteId = 1,
                        ItemId = 1,
                        ItemUnitPrice = 540,
                        Quantity = 5,
                        Nbt = 0.1m,
                        Vat = 0.1m,
                        Discount = 0.1m
                    },
                    new GoodsReceivedNoteItem {
                        GoodsReceivedNoteId = 2,
                        ItemId = 2,
                        ItemUnitPrice = 30,
                        Quantity = 10,
                        Nbt = 0.1m,
                        Vat = 0.1m,
                        Discount = 0.1m
                    },
                    new GoodsReceivedNoteItem {
                        GoodsReceivedNoteId = 1,
                        ItemId = 3,
                        ItemUnitPrice = 50,
                        Quantity = 5,
                        Nbt = 0.1m,
                        Vat = 0.1m,
                        Discount = 0.1m
                    },
                    new GoodsReceivedNoteItem {
                        GoodsReceivedNoteId = 2,
                        ItemId = 4,
                        ItemUnitPrice = 260,
                        Quantity = 6,
                        Nbt = 0.1m,
                        Vat = 0.1m,
                        Discount = 0.1m
                    }
                };

                context.GoodsReceivedNoteItems.AddRange(goodsReceivedNoteItems);
                context.SaveChanges();
            }
        }
    }
}
