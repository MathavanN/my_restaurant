using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Validators.V1;
using System;

namespace MyRestaurant.Business.Tests.Validators.V1.Fixtures
{
    public class CreateServiceTypeDtoValidatorFixture : IDisposable
    {
        private bool _disposed;
        public CreateServiceTypeDto Model { get; set; }
        public CreateServiceTypeDtoValidator Validator { get; private set; }

        public CreateServiceTypeDtoValidatorFixture()
        {
            Validator = new CreateServiceTypeDtoValidator();
            Model = new CreateServiceTypeDto
            {
                Type = "Take Away"
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
