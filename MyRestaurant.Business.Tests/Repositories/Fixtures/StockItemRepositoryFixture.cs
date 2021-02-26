using Moq;
using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Models;
using MyRestaurant.Services;
using MyRestaurant.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyRestaurant.Business.Tests.Repositories.Fixtures
{
    public class StockItemRepositoryFixture : IDisposable
    {
        public Mock<IStockItemService> MockStockItemService { get; private set; }
        public IEnumerable<StockItem> StockItems { get; private set; }
        public CollectionEnvelop<StockItem> CollectionEnvelop { get; private set; }
        public CreateStockItemDto CreateStockItemDto { get; private set; }
        public StockItem CreatedNewStockItem { get; set; }
        public EditStockItemDto EditStockItemDto { get; private set; }

        public StockItemRepositoryFixture()
        {
            MockStockItemService = new Mock<IStockItemService>();
            var unitOfMeasure1 = new UnitOfMeasure { Id = 1, Code = "kg", Description = "" };
            var unitOfMeasure2 = new UnitOfMeasure { Id = 2, Code = "g", Description = "" };
            var unitOfMeasure3 = new UnitOfMeasure { Id = 3, Code = "l", Description = "" };
            var unitOfMeasure4 = new UnitOfMeasure { Id = 4, Code = "ml", Description = "" };
            var unitOfMeasure5 = new UnitOfMeasure { Id = 5, Code = "none", Description = "" };

            var stockType1 = new StockType { Id = 1, Type = "Grocery" };
            var stockType2 = new StockType { Id = 2, Type = "Beverage" };
            var stockType3 = new StockType { Id = 3, Type = "Stationery" };

            StockItems = new List<StockItem>
            {
                new StockItem { Id = 1, TypeId = 1, Type = stockType1, Name = "Rice", ItemUnit = 10, UnitOfMeasureId = 1, UnitOfMeasure = unitOfMeasure1 },
                new StockItem { Id = 2, TypeId = 1, Type = stockType1, Name = "Chilli Powder", ItemUnit = 250, UnitOfMeasureId = 2, UnitOfMeasure = unitOfMeasure2 },
                new StockItem { Id = 3, TypeId = 2, Type = stockType2, Name = "Water", ItemUnit = 1, UnitOfMeasureId = 3, UnitOfMeasure = unitOfMeasure3 },
                new StockItem { Id = 4, TypeId = 3, Type = stockType3, Name = "Blue Pen", ItemUnit = 1, UnitOfMeasureId = 5, UnitOfMeasure = unitOfMeasure5 },
            };

            CollectionEnvelop = new CollectionEnvelop<StockItem>
            {
                Items = StockItems.Where(d => d.TypeId == 1),
                ItemsPerPage = 10,
                TotalItems = 2
            };

            CreateStockItemDto = new CreateStockItemDto
            {
                Name = "Cream Soda",
                TypeId = 2,
                UnitOfMeasureId = 4,
                ItemUnit = 300
            };

            CreatedNewStockItem = new StockItem
            {
                Id = 5,
                TypeId = 2,
                Type = stockType2,
                Name = "Cream Soda",
                ItemUnit = 300,
                UnitOfMeasureId = 4,
                UnitOfMeasure = unitOfMeasure4
            };

            EditStockItemDto = new EditStockItemDto
            {
                TypeId = 1,
                Name = "Rice",
                ItemUnit = 20,
                UnitOfMeasureId = 1,
                Description = "20kg bag"
            };
        }
        public void Dispose()
        {
            MockStockItemService = null;
        }
    }
}
