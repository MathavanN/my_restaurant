using MyRestaurant.Api.Validators.V1;
using MyRestaurant.Business.Dtos.V1;
using System;

namespace MyRestaurant.Api.Tests.Validators.V1.Fixtures
{
    public class EditStockItemDtoValidatorFixture : IDisposable
    {
        public EditStockItemDto Model { get; set; }
        public EditStockItemDtoValidator Validator { get; private set; }

        public EditStockItemDtoValidatorFixture()
        {
            Validator = new EditStockItemDtoValidator();
            Model = new EditStockItemDto
            {
                TypeId = 1,
                Name = "Sugar",
                ItemUnit = 10,
                UnitOfMeasureId = 1,
                Description = "Sugar 10kg bag"
            };
        }

        public void Dispose()
        {
            Model = null;
            Validator = null;
        }
    }
}
