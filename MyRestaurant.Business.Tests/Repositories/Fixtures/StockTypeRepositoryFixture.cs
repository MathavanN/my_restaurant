using Moq;
using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Models;
using MyRestaurant.Services;
using System;
using System.Collections.Generic;

namespace MyRestaurant.Business.Tests.Repositories.Fixtures
{
    public class StockTypeRepositoryFixture : IDisposable
    {
        private bool _disposed;
        public Mock<IStockTypeService> MockStockTypeService { get; private set; }
        public IEnumerable<StockType> StockTypes { get; private set; }
        public CreateStockTypeDto CreateStockTypeDto { get; private set; }
        public EditStockTypeDto EditStockTypeDto { get; private set; }
        public StockType CreatedNewStockType { get; private set; }

        public StockTypeRepositoryFixture()
        {
            MockStockTypeService = new Mock<IStockTypeService>();

            StockTypes = new List<StockType> {
                new StockType { Id = 1, Type = "Grocery", Description = "" },
                new StockType { Id = 2, Type = "Beverage", Description = "" }
            };

            CreateStockTypeDto = new CreateStockTypeDto { Type = "Office", Description = "" };

            CreatedNewStockType = new StockType { Id = 3, Type = CreateStockTypeDto.Type, Description = CreateStockTypeDto.Description };

            EditStockTypeDto = new EditStockTypeDto { Type = "Beverage", Description = "Drinks items" };
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
                    MockStockTypeService = null;
                }

                _disposed = true;
            }
        }
    }
}
