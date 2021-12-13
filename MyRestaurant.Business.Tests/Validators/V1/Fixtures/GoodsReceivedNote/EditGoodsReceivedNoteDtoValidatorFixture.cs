using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Validators.V1;
using System;

namespace MyRestaurant.Business.Tests.Validators.V1.Fixtures
{
    public class EditGoodsReceivedNoteDtoValidatorFixture : IDisposable
    {
        private bool _disposed;
        public EditGoodsReceivedNoteDto Model { get; set; }
        public EditGoodsReceivedNoteDtoValidator Validator { get; private set; }

        public EditGoodsReceivedNoteDtoValidatorFixture()
        {
            Validator = new EditGoodsReceivedNoteDtoValidator();

            Model = new EditGoodsReceivedNoteDto
            {
                PurchaseOrderId = 1,
                InvoiceNumber = "INV_2021_02_13",
                PaymentTypeId = 1,
                Nbt = 0.3m,
                Vat = 0.5m,
                Discount = 1.2m,
                ReceivedBy = Guid.NewGuid(),
                ReceivedDate = DateTime.Now
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
