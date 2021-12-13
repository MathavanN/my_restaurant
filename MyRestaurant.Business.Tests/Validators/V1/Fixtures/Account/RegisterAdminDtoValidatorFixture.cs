using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Validators.V1;
using System;

namespace MyRestaurant.Business.Tests.Validators.V1.Fixtures
{
    public class RegisterAdminDtoValidatorFixture : IDisposable
    {
        private bool _disposed;
        public RegisterAdminDto Model { get; set; }
        public RegisterAdminDtoValidator Validator { get; private set; }

        public RegisterAdminDtoValidatorFixture()
        {
            Validator = new RegisterAdminDtoValidator();

            Model = new RegisterAdminDto
            {
                Email = "test@gmail.com",
                Password = "test",
                ConfirmPassword = "test",
                FirstName = "James",
                LastName = "Vasanth"
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