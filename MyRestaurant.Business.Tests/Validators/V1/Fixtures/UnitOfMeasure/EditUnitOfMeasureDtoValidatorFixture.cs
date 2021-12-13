using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Validators.V1;
using System;

namespace MyRestaurant.Business.Tests.Validators.V1.Fixtures
{
    public class EditUnitOfMeasureDtoValidatorFixture : IDisposable
    {
        private bool _disposed;
        public EditUnitOfMeasureDto Model { get; set; }
        public EditUnitOfMeasureDtoValidator Validator { get; private set; }

        public EditUnitOfMeasureDtoValidatorFixture()
        {
            Validator = new EditUnitOfMeasureDtoValidator();
            Model = new EditUnitOfMeasureDto
            {
                Code = "g",
                Description = "Items in g unit"
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
