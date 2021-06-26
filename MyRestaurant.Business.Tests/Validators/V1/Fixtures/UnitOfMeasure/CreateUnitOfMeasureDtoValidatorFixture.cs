using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Validators.V1;
using System;

namespace MyRestaurant.Business.Tests.Validators.V1.Fixtures
{
    public class CreateUnitOfMeasureDtoValidatorFixture : IDisposable
    {
        private bool _disposed;
        public CreateUnitOfMeasureDto Model { get; set; }
        public CreateUnitOfMeasureDtoValidator Validator { get; private set; }

        public CreateUnitOfMeasureDtoValidatorFixture()
        {
            Validator = new CreateUnitOfMeasureDtoValidator();
            Model = new CreateUnitOfMeasureDto
            {
                Code = "kg",
                Description = "Items in kg unit"
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
