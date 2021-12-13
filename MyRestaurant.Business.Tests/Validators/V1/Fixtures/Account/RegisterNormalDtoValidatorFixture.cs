using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Validators.V1;
using System;
using System.Collections.Generic;

namespace MyRestaurant.Business.Tests.Validators.V1.Fixtures
{
    public class RegisterNormalDtoValidatorFixture : IDisposable
    {
        private bool _disposed;
        public RegisterNormalDto Model { get; set; }
        public RegisterNormalDtoValidator Validator { get; private set; }

        public RegisterNormalDtoValidatorFixture()
        {
            Validator = new RegisterNormalDtoValidator();

            Model = new RegisterNormalDto
            {
                Email = "test@gmail.com",
                Password = "test",
                ConfirmPassword = "test",
                FirstName = "James",
                LastName = "Vasanth",
                Roles = new List<string> { "Report", "Normal" }
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
