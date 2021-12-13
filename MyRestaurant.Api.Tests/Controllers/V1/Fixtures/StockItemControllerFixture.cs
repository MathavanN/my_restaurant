using Microsoft.AspNetCore.Mvc;
using Moq;
using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyRestaurant.Api.Tests.Controllers.V1.Fixtures
{
    public class StockItemControllerFixture : IDisposable
    {
        private bool _disposed;
        public ApiVersion ApiVersion { get; private set; }
        public Mock<IStockItemRepository> MockStockItemRepository { get; private set; }
        public IEnumerable<GetStockItemDto> StockItems { get; private set; }
        public StockItemEnvelop StockItemEnvelop { get; private set; }
        public CreateStockItemDto ValidCreateStockItemDto { get; private set; }
        public GetStockItemDto CreateStockItemDtoResult { get; private set; }
        public EditStockItemDto ValidEditStockItemDto { get; private set; }
        public GetStockItemDto EditStockItemDtoResult { get; private set; }

        public StockItemControllerFixture()
        {
            ApiVersion = new ApiVersion(1, 0);

            MockStockItemRepository = new Mock<IStockItemRepository>();

            StockItems = new List<GetStockItemDto>
            {
                new GetStockItemDto { Id = 1, TypeId = 1, StockType = "Beverage", ItemUnit = 1.5m, UnitOfMeasureId = 1, UnitOfMeasureCode = "l", Name = "Coca-Cola", Description="" },
                new GetStockItemDto { Id = 2, TypeId = 1, StockType = "Beverage", ItemUnit = 300.0m, UnitOfMeasureId = 2, UnitOfMeasureCode = "ml", Name = "Coca-Cola", Description="" },
                new GetStockItemDto { Id = 3, TypeId = 2, StockType = "Grocery", ItemUnit = 10, UnitOfMeasureId = 3, UnitOfMeasureCode = "kg", Name = "Rice", Description="" },
            };

            StockItemEnvelop = new StockItemEnvelop
            {
                StockItemCount = 2,
                StockItems = StockItems.Where(d => d.TypeId == 1),
                ItemsPerPage = 10,
                TotalPages = 1
            };

            ValidCreateStockItemDto = new CreateStockItemDto
            {
                TypeId = 2,
                Name = "Sugar",
                ItemUnit = 20,
                UnitOfMeasureId = 3,
                Description = "test"
            };

            CreateStockItemDtoResult = new GetStockItemDto
            {
                Id = 4,
                TypeId = 2,
                StockType = "Grocery",
                ItemUnit = 20,
                UnitOfMeasureId = 3,
                UnitOfMeasureCode = "kg",
                Name = "Sugar",
                Description = "test"
            };

            ValidEditStockItemDto = new EditStockItemDto
            {
                TypeId = 1,
                ItemUnit = 400.0m,
                UnitOfMeasureId = 2,
                Name = "Coca-Cola",
                Description = ""
            };

            EditStockItemDtoResult = new GetStockItemDto
            {
                Id = 2,
                TypeId = 1,
                StockType = "Beverage",
                ItemUnit = 400.0m,
                UnitOfMeasureId = 2,
                UnitOfMeasureCode = "ml",
                Name = "Coca-Cola",
                Description = ""
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
                    MockStockItemRepository = null;
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
                }

                _disposed = true;
            }
        }
    }
}
