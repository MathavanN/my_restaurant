using Microsoft.AspNetCore.Mvc;
using Moq;
using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Repositories.Contracts;
using System;
using System.Collections.Generic;

namespace MyRestaurant.Api.Tests.Controllers.V1.Fixtures
{
    public class StockTypeControllerFixture : IDisposable
    {
        public ApiVersion ApiVersion { get; private set; }
        public Mock<IStockTypeRepository> MockStockTypeRepository { get; private set; }
        public IEnumerable<GetStockTypeDto> StockTypes { get; private set; }
        public CreateStockTypeDto ValidCreateStockTypeDto { get; private set; }
        public GetStockTypeDto CreateStockTypeDtoResult { get; private set; }
        public EditStockTypeDto ValidEditStockTypeDto { get; private set; }
        public GetStockTypeDto EditStockTypeDtoResult { get; private set; }

        public StockTypeControllerFixture()
        {
            ApiVersion = new ApiVersion(1, 0);

            MockStockTypeRepository = new Mock<IStockTypeRepository>();

            StockTypes = new List<GetStockTypeDto>
            {
                new GetStockTypeDto { Id = 1, Type = "Grocery", Description = "grocery items" },
                new GetStockTypeDto { Id = 2, Type = "Beverage", Description = "beverage items" }
            };

            ValidCreateStockTypeDto = new CreateStockTypeDto
            {
                Type = "Kitchen",
                Description = "kitchen items"
            };

            CreateStockTypeDtoResult = new GetStockTypeDto
            {
                Id = 3,
                Type = "Kitchen",
                Description = "kitchen items"
            };

            ValidEditStockTypeDto = new EditStockTypeDto
            {
                Type = "Beverage",
                Description = "Beverage items to add"
            };

            EditStockTypeDtoResult = new GetStockTypeDto
            {
                Id = 2,
                Type = "Beverage",
                Description = "Beverage items to add"
            };
        }

        public void Dispose()
        {
            MockStockTypeRepository = null;
        }
    }
}
