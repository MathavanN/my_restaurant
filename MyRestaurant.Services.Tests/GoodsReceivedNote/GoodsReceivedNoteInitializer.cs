using MyRestaurant.Core;
using MyRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyRestaurant.Services.Tests
{
    public class GoodsReceivedNoteInitializer
    {
        public static void Initialize(MyRestaurantContext context)
        {
            PaymentTypeInitializer.Initialize(context);
            UnitOfMeasureInitializer.Initialize(context);
            StockTypeInitializer.Initialize(context);
            StockItemInitializer.Initialize(context);
            SupplierInitializer.Initialize(context);
            UserInitializer.Initialize(context);
            PurchaseOrderInitializer.Initialize(context);
            PurchaseOrderItemInitializer.Initialize(context);

            if (!context.GoodsReceivedNotes.Any())
            {
                var goodsReceivedNotes = new List<GoodsReceivedNote>
                {
                    new GoodsReceivedNote {
                        PurchaseOrderId = 1,
                        InvoiceNumber = "INV_20210132_01",
                        PaymentTypeId = 1,
                        Nbt = 0.5m,
                        Vat = 0.5m,
                        Discount = 0.5m,
                        ReceivedBy = context.Users.First(d => d.FirstName == "Admin").Id,
                        ReceivedDate = DateTime.Now.AddDays(-5),
                        ApprovedBy = Guid.Empty,
                        ApprovalStatus = Status.Pending,
                        ApprovedDate = default,
                        ApprovalReason = "",
                        CreatedBy = context.Users.First(d => d.FirstName == "Admin").Id,
                        CreatedDate = DateTime.Now
                    },
                    new GoodsReceivedNote {
                        PurchaseOrderId = 4,
                        InvoiceNumber = "INV_20210206_02",
                        PaymentTypeId = 1,
                        Nbt = 0,
                        Vat = 0,
                        Discount = 0,
                        ReceivedBy = context.Users.First(d => d.FirstName == "Normal").Id,
                        ReceivedDate = DateTime.Now.AddDays(-10),
                        ApprovalStatus = Status.Approved,
                        ApprovedBy = context.Users.First(d => d.FirstName == "Admin").Id,
                        ApprovedDate = DateTime.Now.AddDays(-8),
                        CreatedBy = context.Users.First(d => d.FirstName == "Admin").Id,
                        CreatedDate = DateTime.Now.AddDays(-9)
                    },
                    new GoodsReceivedNote {
                        PurchaseOrderId = 6,
                        InvoiceNumber = "INV_20210224_01",
                        PaymentTypeId = 1,
                        Vat = 0.7m,
                        Discount = 1.4m,
                        Nbt = 0.7m,
                        ReceivedBy = context.Users.First(d => d.FirstName == "Report").Id,
                        ReceivedDate = DateTime.Now.AddMinutes(-60),
                        ApprovalStatus = Status.Approved,
                        ApprovedBy = context.Users.First(d => d.FirstName == "Admin").Id,
                        ApprovedDate = DateTime.Now.AddMinutes(-30),
                        ApprovalReason = "Item are received",
                        CreatedBy = context.Users.First(d => d.FirstName == "Admin").Id,
                        CreatedDate = DateTime.Now.AddMinutes(-60)
                    }
                };

                context.GoodsReceivedNotes.AddRange(goodsReceivedNotes);
                context.SaveChanges();
            }
        }
    }
}
