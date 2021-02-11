using MyRestaurant.Api.Validators.V1;
using MyRestaurant.Business.Dtos.V1;
using System;

namespace MyRestaurant.Api.Tests.Validators.V1.Fixtures
{
    public class CreatePaymentTypeDtoValidatorFixture : IDisposable
    {
        public CreatePaymentTypeDto Model { get; set; }
        public CreatePaymentTypeDtoValidator Validator { get; private set; }

        public CreatePaymentTypeDtoValidatorFixture()
        {
            Validator = new CreatePaymentTypeDtoValidator();

            Model = new CreatePaymentTypeDto
            {
                Name = "Credit",
                CreditPeriod = 30
            };
        }
        public void Dispose()
        {
            Model = null;
            Validator = null;
        }
    }
}
