using Moq;
using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Models;
using MyRestaurant.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyRestaurant.Business.Tests.Repositories.Fixtures
{
    public class GoodsReceivedNoteFreeItemRepositoryFixture : IDisposable
    {
        private bool _disposed;
        public Mock<IGoodsReceivedNoteFreeItemService> MockGoodsReceivedNoteFreeItemService { get; private set; }
        public IEnumerable<GoodsReceivedNoteFreeItem> GoodsReceivedNoteFreeItems { get; private set; }
        public CreateGoodsReceivedNoteFreeItemDto CreateGoodsReceivedNoteFreeItemDto { get; private set; }
        public GoodsReceivedNoteFreeItem CreatedNewGoodsReceivedNoteFreeItem { get; private set; }
        public EditGoodsReceivedNoteFreeItemDto EditGoodsReceivedNoteFreeItemDto { get; private set; }
        public GoodsReceivedNoteFreeItemRepositoryFixture()
        {
            MockGoodsReceivedNoteFreeItemService = new Mock<IGoodsReceivedNoteFreeItemService>();

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

            GoodsReceivedNoteFreeItems = new List<GoodsReceivedNoteFreeItem>
            {
                new GoodsReceivedNoteFreeItem {
                    Id = 1,
                    GoodsReceivedNoteId = 101,
                    ItemId = 20026,
                    Item = stockItems.First(d => d.Id == 20026),
                    ItemUnitPrice = 250,
                    Quantity = 1,
                    Nbt = 0.1m,
                    Vat = 0.1m,
                    Discount = 0.1m
                },
                new GoodsReceivedNoteFreeItem
                {
                    Id = 2,
                    GoodsReceivedNoteId = 202,
                    ItemId = 20050,
                    Item = stockItems.First(d => d.Id == 20050),
                    ItemUnitPrice = 30,
                    Quantity = 5,
                    Nbt = 0.1m,
                    Vat = 0.1m,
                    Discount = 0.1m
                }
            };

            CreateGoodsReceivedNoteFreeItemDto = new CreateGoodsReceivedNoteFreeItemDto
            {
                GoodsReceivedNoteId = 202,
                ItemId = 20023,
                ItemUnitPrice = 350,
                Quantity = 5,
                Nbt = 0.1m,
                Vat = 0.1m,
                Discount = 0.1m
            };

            CreatedNewGoodsReceivedNoteFreeItem = new GoodsReceivedNoteFreeItem
            {
                Id = 3,
                GoodsReceivedNoteId = 202,
                ItemId = 20023,
                Item = stockItems.First(d => d.Id == 20023),
                ItemUnitPrice = 350,
                Quantity = 5,
                Nbt = 0.1m,
                Vat = 0.1m,
                Discount = 0.1m
            };

            EditGoodsReceivedNoteFreeItemDto = new EditGoodsReceivedNoteFreeItemDto
            {
                GoodsReceivedNoteId = 101,
                ItemId = 20026,
                ItemUnitPrice = 350,
                Quantity = 2
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
                    MockGoodsReceivedNoteFreeItemService = null;
                }

                _disposed = true;
            }
        }
    }
}
