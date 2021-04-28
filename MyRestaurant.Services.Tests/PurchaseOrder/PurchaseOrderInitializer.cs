using MyRestaurant.Core;
using MyRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyRestaurant.Services.Tests
{
    public class PurchaseOrderInitializer
    {
        public static void Initialize(MyRestaurantContext context)
        {
            UserInitializer.Initialize(context);
            SupplierInitializer.Initialize(context);

            if (!context.PurchaseOrders.Any())
            {
                var orders = new List<PurchaseOrder>
                {
                    new PurchaseOrder
                    {
                        OrderNumber = "PO_20210130_8d8c510caee6a4b",
                        SupplierId = 1,
                        RequestedBy = context.Users.First(d => d.FirstName == "Normal").Id,
                        RequestedDate = DateTime.Now.AddDays(-10),
                        ApprovalStatus = Status.Approved,
                        ApprovedBy = context.Users.First(d => d.FirstName == "Admin").Id,
                        ApprovedDate = DateTime.Now,
                        Description = "Test",
                        ApprovalReason = "Items are required"
                    },
                    new PurchaseOrder
                    {
                        OrderNumber = "PO_20210130_8d8c512f7cd7920",
                        SupplierId = 2,
                        RequestedBy = context.Users.First(d => d.FirstName == "Admin").Id,
                        RequestedDate = DateTime.Now.AddDays(-5),
                        ApprovalStatus = Status.Pending,
                        ApprovedBy = Guid.Empty,
                        ApprovedDate = default,
                        Description = "",
                        ApprovalReason = ""
                    },
                    new PurchaseOrder
                    {
                        OrderNumber = "PO_20210206_8d8caa8b86ce209",
                        SupplierId = 1,
                        RequestedBy = context.Users.First(d => d.FirstName == "Normal").Id,
                        RequestedDate = DateTime.Now.AddDays(-2),
                        ApprovalStatus = Status.Rejected,
                        ApprovedBy = context.Users.First(d => d.FirstName == "Golden").Id,
                        ApprovedDate = DateTime.Now.AddDays(-1),
                        ApprovalReason = "Test for reject",
                        Description = "Test Description"
                    },
                    new PurchaseOrder
                    {
                        OrderNumber = "PO_20210227_8d8caa8b86ce209",
                        SupplierId = 2,
                        RequestedBy = context.Users.First(d => d.FirstName == "Report").Id,
                        RequestedDate = DateTime.Now.AddDays(-2),
                        ApprovalStatus = Status.Approved,
                        ApprovedBy = context.Users.First(d => d.FirstName == "Golden").Id,
                        ApprovedDate = DateTime.Now.AddDays(-1),
                        ApprovalReason = "Items required",
                        Description = "Test Description",
                    },
                    new PurchaseOrder
                    {
                        OrderNumber = "PO_20210224_8d8c510caee6a4b",
                        SupplierId = 1,
                        RequestedBy = context.Users.First(d => d.FirstName == "Normal").Id,
                        RequestedDate = DateTime.Now.AddDays(-1),
                        ApprovalStatus = Status.Approved,
                        ApprovedBy = context.Users.First(d => d.FirstName == "Golden").Id,
                        ApprovedDate = DateTime.Now.AddDays(-1),
                        ApprovalReason = "Items required",
                        Description = "Test Description",
                    },
                    new PurchaseOrder
                    {
                        OrderNumber = "PO_20210223_8d8c510caee6a4b",
                        SupplierId = 1,
                        RequestedBy = context.Users.First(d => d.FirstName == "Report").Id,
                        RequestedDate = DateTime.Now.AddDays(-3),
                        ApprovalStatus = Status.Approved,
                        ApprovedBy = context.Users.First(d => d.FirstName == "Admin").Id,
                        ApprovedDate = DateTime.Now.AddDays(-2),
                        ApprovalReason = "Items required",
                        Description = "Test Description"
                    }
                };

                context.PurchaseOrders.AddRange(orders);
                context.SaveChanges();
            }
        }
    }
}
