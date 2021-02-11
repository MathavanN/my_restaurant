using MyRestaurant.Api.Validators.V1;
using MyRestaurant.Business.Dtos.V1;
using System;

namespace MyRestaurant.Api.Tests.Validators.V1.Fixtures
{
    public class EditStockTypeDtoValidatorFixture : IDisposable
    {
        public EditStockTypeDto Model { get; set; }
        public EditStockTypeDtoValidator Validator { get; private set; }

        public EditStockTypeDtoValidatorFixture()
        {
            Validator = new EditStockTypeDtoValidator();
            Model = new EditStockTypeDto
            {
                Type = "Beverage",
                Description = "Items for beverage type"
            };
        }

        public void Dispose()
        {
            Model = null;
            Validator = null;
        }
    }
}
