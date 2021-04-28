using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Validators.V1;
using System;

namespace MyRestaurant.Business.Tests.Validators.V1.Fixtures
{
    public class CreatePurchaseOrderItemDtoValidatorFixture : IDisposable
    {
        public CreatePurchaseOrderItemDto Model { get; set; }
        public CreatePurchaseOrderItemDtoValidator Validator { get; private set; }

        public CreatePurchaseOrderItemDtoValidatorFixture()
        {
            Validator = new CreatePurchaseOrderItemDtoValidator();

            Model = new CreatePurchaseOrderItemDto
            {
                PurchaseOrderId = 1,
                ItemId = 1,
                ItemUnitPrice = 1250.50m,
                Quantity = 1
            };
        }
        public void Dispose()
        {
            Model = null;
            Validator = null;
        }
    }
}
