using MyRestaurant.Api.Validators.V1;
using MyRestaurant.Business.Dtos.V1;
using System;

namespace MyRestaurant.Api.Tests.Validators.V1.Fixtures
{
    public class EditPurchaseOrderItemDtoValidatorFixture : IDisposable
    {
        public EditPurchaseOrderItemDto Model { get; set; }
        public EditPurchaseOrderItemDtoValidator Validator { get; private set; }

        public EditPurchaseOrderItemDtoValidatorFixture()
        {
            Validator = new EditPurchaseOrderItemDtoValidator();

            Model = new EditPurchaseOrderItemDto
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
