using Moq;
using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Models;
using MyRestaurant.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyRestaurant.Business.Tests.Repositories.Fixtures
{
    public class GoodsReceivedNoteItemRepositoryFixture : IDisposable
    {
        private bool _disposed;
        public Mock<IGoodsReceivedNoteItemService> MockGoodsReceivedNoteItemService { get; private set; }
        public IEnumerable<GoodsReceivedNoteItem> GoodsReceivedNoteItems { get; private set; }
        public CreateGoodsReceivedNoteItemDto CreateGoodsReceivedNoteItemDto { get; private set; }
        public GoodsReceivedNoteItem CreatedNewGoodsReceivedNoteItem { get; private set; }
        public EditGoodsReceivedNoteItemDto EditGoodsReceivedNoteItemDto { get; private set; }
        public GoodsReceivedNoteItemRepositoryFixture()
        {
            MockGoodsReceivedNoteItemService = new Mock<IGoodsReceivedNoteItemService>();

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

            GoodsReceivedNoteItems = new List<GoodsReceivedNoteItem>
            {
                new GoodsReceivedNoteItem {
                    Id = 1,
                    GoodsReceivedNoteId = 101,
                    Item = stockItems.FirstOrDefault(d => d.Id == 20025),
                    ItemId = 20025,
                    ItemUnitPrice = 540,
                    Quantity = 5,
                    Nbt = 0.1m,
                    Vat = 0.1m,
                    Discount = 0.1m
                },
                new GoodsReceivedNoteItem {
                    Id = 2,
                    GoodsReceivedNoteId = 202,
                    Item = stockItems.FirstOrDefault(d => d.Id == 20026),
                    ItemId = 20026,
                    ItemUnitPrice = 30,
                    Quantity = 10,
                    Nbt = 0.1m,
                    Vat = 0.1m,
                    Discount = 0.1m
                },
                new GoodsReceivedNoteItem {
                    Id = 3,
                    GoodsReceivedNoteId = 101,
                    Item = stockItems.FirstOrDefault(d => d.Id == 20050),
                    ItemId = 20050,
                    ItemUnitPrice = 50,
                    Quantity = 5,
                    Nbt = 0.1m,
                    Vat = 0.1m,
                    Discount = 0.1m
                },
                new GoodsReceivedNoteItem {
                    Id = 4,
                    GoodsReceivedNoteId = 202,
                    Item = stockItems.FirstOrDefault(d => d.Id == 20024),
                    ItemId = 20024,
                    ItemUnitPrice = 260,
                    Quantity = 6,
                    Nbt = 0.1m,
                    Vat = 0.1m,
                    Discount = 0.1m
                }
            };

            CreateGoodsReceivedNoteItemDto = new CreateGoodsReceivedNoteItemDto
            {
                GoodsReceivedNoteId = 202,
                ItemId = 20023,
                ItemUnitPrice = 350,
                Quantity = 5
            };

            CreatedNewGoodsReceivedNoteItem = new GoodsReceivedNoteItem
            {
                Id = 5,
                GoodsReceivedNoteId = 202,
                Item = stockItems.FirstOrDefault(d => d.Id == 20023),
                ItemId = 20023,
                ItemUnitPrice = 350,
                Quantity = 5
            };

            EditGoodsReceivedNoteItemDto = new EditGoodsReceivedNoteItemDto
            {
                GoodsReceivedNoteId = 101,
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
                    MockGoodsReceivedNoteItemService = null;
                }

                _disposed = true;
            }
        }
    }
}
