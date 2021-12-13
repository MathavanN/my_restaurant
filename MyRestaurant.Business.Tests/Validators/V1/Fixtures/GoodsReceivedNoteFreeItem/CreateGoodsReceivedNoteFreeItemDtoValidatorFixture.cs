using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Validators.V1;
using System;

namespace MyRestaurant.Business.Tests.Validators.V1.Fixtures
{
    public class CreateGoodsReceivedNoteFreeItemDtoValidatorFixture : IDisposable
    {
        private bool _disposed;
        public CreateGoodsReceivedNoteFreeItemDto Model { get; set; }
        public CreateGoodsReceivedNoteFreeItemDtoValidator Validator { get; private set; }

        public CreateGoodsReceivedNoteFreeItemDtoValidatorFixture()
        {
            Validator = new CreateGoodsReceivedNoteFreeItemDtoValidator();

            Model = new CreateGoodsReceivedNoteFreeItemDto
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
