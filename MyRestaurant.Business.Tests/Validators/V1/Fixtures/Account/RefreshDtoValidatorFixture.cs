using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Validators.V1;
using System;

namespace MyRestaurant.Business.Tests.Validators.V1.Fixtures
{
    public class RefreshDtoValidatorFixture : IDisposable
    {
        private bool _disposed;
        public RefreshDto Model { get; set; }
        public RefreshDtoValidator Validator { get; private set; }

        public RefreshDtoValidatorFixture()
        {
            Validator = new RefreshDtoValidator();

            Model = new RefreshDto
            {
                RefreshToken = "473ed8ba-2292-49e1-a930-5129a002e753"
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
