using MyRestaurant.Api.Validators.V1;
using MyRestaurant.Business.Dtos.V1;
using System;

namespace MyRestaurant.Api.Tests.Validators.V1.Fixtures
{
    public class EditPaymentTypeDtoValidatorFixture : IDisposable
    {
        public EditPaymentTypeDto Model { get; set; }
        public EditPaymentTypeDtoValidator Validator { get; private set; }

        public EditPaymentTypeDtoValidatorFixture()
        {
            Validator = new EditPaymentTypeDtoValidator();
            Model = new EditPaymentTypeDto
            {
                Name = "Credit",
                CreditPeriod = 15
            };
        }

        public void Dispose()
        {
            Model = null;
            Validator = null;
        }
    }
}
