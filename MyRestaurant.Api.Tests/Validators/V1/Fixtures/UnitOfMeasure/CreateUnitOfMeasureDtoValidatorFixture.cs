using MyRestaurant.Api.Validators.V1;
using MyRestaurant.Business.Dtos.V1;
using System;

namespace MyRestaurant.Api.Tests.Validators.V1.Fixtures
{
    public class CreateUnitOfMeasureDtoValidatorFixture : IDisposable
    {
        public CreateUnitOfMeasureDto Model { get; set; }
        public CreateUnitOfMeasureDtoValidator Validator { get; private set; }

        public CreateUnitOfMeasureDtoValidatorFixture()
        {
            Validator = new CreateUnitOfMeasureDtoValidator();
            Model = new CreateUnitOfMeasureDto
            {
                Code = "kg",
                Description = "Items in kg unit"
            };
        }

        public void Dispose()
        {
            Model = null;
            Validator = null;
        }
    }
}
