using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Validators.V1;
using System;

namespace MyRestaurant.Business.Tests.Validators.V1.Fixtures
{
    public class CreateStockItemDtoValidatorFixture : IDisposable
    {
        private bool _disposed;
        public CreateStockItemDto Model { get; set; }
        public CreateStockItemDtoValidator Validator { get; private set; }

        public CreateStockItemDtoValidatorFixture()
        {
            Validator = new CreateStockItemDtoValidator();
            Model = new CreateStockItemDto
            {
                TypeId = 1,
                Name = "Rice",
                ItemUnit = 5,
                UnitOfMeasureId = 1,
                Description = "Rice 5kg bag"
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
