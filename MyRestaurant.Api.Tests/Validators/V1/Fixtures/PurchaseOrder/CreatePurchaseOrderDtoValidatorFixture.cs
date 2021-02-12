using MyRestaurant.Api.Validators.V1;
using MyRestaurant.Business.Dtos.V1;
using System;

namespace MyRestaurant.Api.Tests.Validators.V1.Fixtures
{
    public class CreatePurchaseOrderDtoValidatorFixture : IDisposable
    {
        public CreatePurchaseOrderDto Model { get; set; }
        public CreatePurchaseOrderDtoValidator Validator { get; private set; }

        public CreatePurchaseOrderDtoValidatorFixture()
        {
            Validator = new CreatePurchaseOrderDtoValidator();

            Model = new CreatePurchaseOrderDto
            {
                SupplierId = 1,
                Description = "Purchase order description"
            };
        }
        public void Dispose()
        {
            Model = null;
            Validator = null;
        }
    }
}
