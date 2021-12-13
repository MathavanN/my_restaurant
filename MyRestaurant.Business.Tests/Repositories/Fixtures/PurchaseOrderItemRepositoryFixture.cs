using Moq;
using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Models;
using MyRestaurant.Services;
using System;
using System.Collections.Generic;
using System.Linq;
namespace MyRestaurant.Business.Tests.Repositories.Fixtures
{
    public class PurchaseOrderItemRepositoryFixture : IDisposable
    {
        private bool _disposed;
        public Mock<IPurchaseOrderItemService> MockPurchaseOrderItemService { get; private set; }
        public IEnumerable<PurchaseOrderItem> PurchaseOrderItems { get; private set; }
        public CreatePurchaseOrderItemDto CreatePurchaseOrderItemDto { get; private set; }
        public PurchaseOrderItem CreatedNewPurchaseOrderItem { get; private set; }
        public EditPurchaseOrderItemDto EditPurchaseOrderItemDto { get; private set; }
        public PurchaseOrderItemRepositoryFixture()
        {
            MockPurchaseOrderItemService = new Mock<IPurchaseOrderItemService>();

            var unitOfMeasure1 = new UnitOfMeasure { Id = 1, Code = "kg", Description = "" };
            var unitOfMeasure2 = new UnitOfMeasure { Id = 2, Code = "g", Description = "" };
            var unitOfMeasure3 = new UnitOfMeasure { Id = 3, Code = "l", Description = "" };
            var unitOfMeasure4 = new UnitOfMeasure { Id = 4, Code = "ml", Description = "" };
            var unitOfMeasure5 = new UnitOfMeasure { Id = 5, Code = "none", Description = "" };

            var stockType1 = new StockType { Id = 1, Type = "Grocery" };
            var stockType2 = new StockType { Id = 2, Type = "Beverage" };
            var stockType3 = new StockType { Id = 3, Type = "Stationery" };

            var stockItems = new List<StockItem>
            {
                new StockItem { Id = 20025, TypeId = 1, Type = stockType1, Name = "Rice", ItemUnit = 10, UnitOfMeasureId = 1, UnitOfMeasure = unitOfMeasure1 },
                new StockItem { Id = 20026, TypeId = 1, Type = stockType1, Name = "Chilli Powder", ItemUnit = 250, UnitOfMeasureId = 2, UnitOfMeasure = unitOfMeasure2 },
                new StockItem { Id = 20050, TypeId = 2, Type = stockType2, Name = "Water", ItemUnit = 1, UnitOfMeasureId = 3, UnitOfMeasure = unitOfMeasure3 },
                new StockItem { Id = 20024, TypeId = 3, Type = stockType3, Name = "Blue Pen", ItemUnit = 1, UnitOfMeasureId = 5, UnitOfMeasure = unitOfMeasure5 },
                new StockItem { Id = 20023, TypeId = 5, Type = stockType1, Name = "Rice", ItemUnit = 10, UnitOfMeasureId = 1, UnitOfMeasure = unitOfMeasure1 },
            };

            PurchaseOrderItems = new List<PurchaseOrderItem>
            {
                new PurchaseOrderItem {
                    Id = 1,
                    PurchaseOrderId = 101,
                    Item = stockItems.First(d => d.Id == 20025),
                    ItemId = 20025,
                    ItemUnitPrice = 540,
                    Quantity = 5
                },
                new PurchaseOrderItem {
                    Id = 2,
                    PurchaseOrderId = 202,
                    Item = stockItems.First(d => d.Id == 20026),
                    ItemId = 20026,
                    ItemUnitPrice = 30,
                    Quantity = 10
                },
                new PurchaseOrderItem {
                    Id = 3,
                    PurchaseOrderId = 101,
                    Item = stockItems.First(d => d.Id == 20050),
                    ItemId = 20050,
                    ItemUnitPrice = 50,
                    Quantity = 5
                },
                new PurchaseOrderItem {
                    Id = 4,
                    PurchaseOrderId = 202,
                    Item = stockItems.First(d => d.Id == 20024),
                    ItemId = 20024,
                    ItemUnitPrice = 260,
                    Quantity = 6
                }
            };

            CreatePurchaseOrderItemDto = new CreatePurchaseOrderItemDto
            {
                PurchaseOrderId = 202,
                ItemId = 20023,
                ItemUnitPrice = 350,
                Quantity = 5
            };

            CreatedNewPurchaseOrderItem = new PurchaseOrderItem
            {
                Id = 5,
                PurchaseOrderId = 202,
                Item = stockItems.First(d => d.Id == 20023),
                ItemId = 20023,
                ItemUnitPrice = 350,
                Quantity = 5
            };

            EditPurchaseOrderItemDto = new EditPurchaseOrderItemDto
            {
                PurchaseOrderId = 101,
                ItemId = 20025,
                ItemUnitPrice = 650,
                Quantity = 7
            };
        }
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
                    MockPurchaseOrderItemService = null;
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
                }

                _disposed = true;
            }
        }
    }
}
