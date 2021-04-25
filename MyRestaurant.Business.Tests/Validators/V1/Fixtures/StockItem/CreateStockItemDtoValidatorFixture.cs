using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Validators.V1;
using System;

namespace MyRestaurant.Business.Tests.Validators.V1.Fixtures
{
    public class CreateStockItemDtoValidatorFixture : IDisposable
    {
        public CreateStockItemDto Model { get; set; }
        public CreateStockItemDtoValidator Validator { get; private set; }

        public CreateStockItemDtoValidatorFixture()
        {
            Validator = new CreateStockItemDtoValidator();
            Model = new CreateStockItemDto
            {
                TypeId = 1,
                Name = "Rice",
                ItemUnit = 5,
                UnitOfMeasureId = 1,
                Description = "Rice 5kg bag"
            };
        }

        public void Dispose()
        {
            Model = null;
            Validator = null;
        }
    }
}
