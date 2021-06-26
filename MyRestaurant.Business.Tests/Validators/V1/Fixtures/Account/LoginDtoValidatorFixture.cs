using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Validators.V1;
using System;

namespace MyRestaurant.Business.Tests.Validators.V1.Fixtures
{
    public class LoginDtoValidatorFixture : IDisposable
    {
        private bool _disposed;
        public LoginDto Model { get; set; }
        public LoginDtoValidator Validator { get; private set; }

        public LoginDtoValidatorFixture()
        {
            Validator = new LoginDtoValidator();

            Model = new LoginDto
            {
                Email = "test@gmail.com",
                Password = "test"
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
