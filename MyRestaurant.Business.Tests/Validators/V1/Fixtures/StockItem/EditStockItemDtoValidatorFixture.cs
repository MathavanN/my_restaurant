using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Validators.V1;
using System;

namespace MyRestaurant.Business.Tests.Validators.V1.Fixtures
{
    public class EditStockItemDtoValidatorFixture : IDisposable
    {
        private bool _disposed;
        public EditStockItemDto Model { get; set; }
        public EditStockItemDtoValidator Validator { get; private set; }

        public EditStockItemDtoValidatorFixture()
        {
            Validator = new EditStockItemDtoValidator();
            Model = new EditStockItemDto
            {
                TypeId = 1,
                Name = "Sugar",
                ItemUnit = 10,
                UnitOfMeasureId = 1,
                Description = "Sugar 10kg bag"
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
