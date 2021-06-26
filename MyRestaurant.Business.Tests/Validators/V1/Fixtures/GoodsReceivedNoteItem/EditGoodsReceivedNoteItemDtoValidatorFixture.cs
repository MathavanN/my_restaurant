using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Validators.V1;
using System;

namespace MyRestaurant.Business.Tests.Validators.V1.Fixtures
{
    public class EditGoodsReceivedNoteItemDtoValidatorFixture : IDisposable
    {
        private bool _disposed;
        public EditGoodsReceivedNoteItemDto Model { get; set; }
        public EditGoodsReceivedNoteItemDtoValidator Validator { get; private set; }

        public EditGoodsReceivedNoteItemDtoValidatorFixture()
        {
            Validator = new EditGoodsReceivedNoteItemDtoValidator();

            Model = new EditGoodsReceivedNoteItemDto
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
                    Model = null;
                    Validator = null;
                }

                _disposed = true;
            }
        }
    }
}
