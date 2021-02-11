using MyRestaurant.Api.Validators.V1;
using MyRestaurant.Business.Dtos.V1;
using System;

namespace MyRestaurant.Api.Tests.Validators.V1.Fixtures
{
    public class CreateServiceTypeDtoValidatorFixture : IDisposable
    {
        public CreateServiceTypeDto Model { get; set; }
        public CreateServiceTypeDtoValidator Validator { get; private set; }

        public CreateServiceTypeDtoValidatorFixture()
        {
            Validator = new CreateServiceTypeDtoValidator();
            Model = new CreateServiceTypeDto
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
