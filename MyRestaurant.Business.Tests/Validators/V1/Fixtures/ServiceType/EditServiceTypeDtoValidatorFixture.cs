using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Validators.V1;
using System;

namespace MyRestaurant.Business.Tests.Validators.V1.Fixtures
{
    public class EditServiceTypeDtoValidatorFixture : IDisposable
    {
        public EditServiceTypeDto Model { get; set; }
        public EditServiceTypeDtoValidator Validator { get; private set; }

        public EditServiceTypeDtoValidatorFixture()
        {
            Validator = new EditServiceTypeDtoValidator();
            Model = new EditServiceTypeDto
            {
                Type = "Take Away"
            };
        }

        public void Dispose()
        {
            Model = null;
            Validator = null;
        }
    }
}
