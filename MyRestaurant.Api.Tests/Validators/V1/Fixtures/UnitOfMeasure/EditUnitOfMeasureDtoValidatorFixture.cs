using MyRestaurant.Api.Validators.V1;
using MyRestaurant.Business.Dtos.V1;
using System;

namespace MyRestaurant.Api.Tests.Validators.V1.Fixtures
{
    public class EditUnitOfMeasureDtoValidatorFixture : IDisposable
    {
        public EditUnitOfMeasureDto Model { get; set; }
        public EditUnitOfMeasureDtoValidator Validator { get; private set; }

        public EditUnitOfMeasureDtoValidatorFixture()
        {
            Validator = new EditUnitOfMeasureDtoValidator();
            Model = new EditUnitOfMeasureDto
            {
                Code = "g",
                Description = "Items in g unit"
            };
        }

        public void Dispose()
        {
            Model = null;
            Validator = null;
        }
    }
}
