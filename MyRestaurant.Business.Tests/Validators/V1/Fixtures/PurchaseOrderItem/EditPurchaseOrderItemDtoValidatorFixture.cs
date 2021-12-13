using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Validators.V1;
using System;

namespace MyRestaurant.Business.Tests.Validators.V1.Fixtures
{
    public class EditPurchaseOrderItemDtoValidatorFixture : IDisposable
    {
        private bool _disposed;
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
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
                    Model = null;
                    Validator = null;
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
                }

                _disposed = true;
            }
        }
    }
}
