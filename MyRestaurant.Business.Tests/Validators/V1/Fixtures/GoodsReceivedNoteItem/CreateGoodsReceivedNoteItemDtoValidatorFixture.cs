using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Validators.V1;
using System;

namespace MyRestaurant.Business.Tests.Validators.V1.Fixtures
{
    public class CreateGoodsReceivedNoteItemDtoValidatorFixture : IDisposable
    {
        private bool _disposed;
        public CreateGoodsReceivedNoteItemDto Model { get; set; }
        public CreateGoodsReceivedNoteItemDtoValidator Validator { get; private set; }

        public CreateGoodsReceivedNoteItemDtoValidatorFixture()
        {
            Validator = new CreateGoodsReceivedNoteItemDtoValidator();

            Model = new CreateGoodsReceivedNoteItemDto
            {
                GoodsReceivedNoteId = 1,
                ItemId = 1,
                ItemUnitPrice = 1450.50m,
                Quantity = 2,
                Nbt = 0.3m,
                Vat = 0.5m,
                Discount = 1.2m,
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
