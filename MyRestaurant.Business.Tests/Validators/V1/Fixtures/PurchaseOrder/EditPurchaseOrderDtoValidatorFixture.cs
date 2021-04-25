using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Validators.V1;
using System;

namespace MyRestaurant.Business.Tests.Validators.V1.Fixtures
{
    public class EditPurchaseOrderDtoValidatorFixture : IDisposable
    {
        public EditPurchaseOrderDto Model { get; set; }
        public EditPurchaseOrderDtoValidator Validator { get; private set; }

        public EditPurchaseOrderDtoValidatorFixture()
        {
            Validator = new EditPurchaseOrderDtoValidator();

            Model = new EditPurchaseOrderDto
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
