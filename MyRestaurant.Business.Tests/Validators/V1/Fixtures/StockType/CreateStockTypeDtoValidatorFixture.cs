using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Validators.V1;
using System;

namespace MyRestaurant.Business.Tests.Validators.V1.Fixtures
{
    public class CreateStockTypeDtoValidatorFixture : IDisposable
    {
        public CreateStockTypeDto Model { get; set; }
        public CreateStockTypeDtoValidator Validator { get; private set; }

        public CreateStockTypeDtoValidatorFixture()
        {
            Validator = new CreateStockTypeDtoValidator();
            Model = new CreateStockTypeDto
            {
                Type = "Grocery",
                Description = "Items for grocery type"
            };
        }

        public void Dispose()
        {
            Model = null;
            Validator = null;
        }
    }
}
