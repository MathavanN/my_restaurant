using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Validators.V1;
using System;

namespace MyRestaurant.Business.Tests.Validators.V1.Fixtures
{
    public class CreatePaymentTypeDtoValidatorFixture : IDisposable
    {
        private bool _disposed;
        public CreatePaymentTypeDto Model { get; set; }
        public CreatePaymentTypeDtoValidator Validator { get; private set; }

        public CreatePaymentTypeDtoValidatorFixture()
        {
            Validator = new CreatePaymentTypeDtoValidator();

            Model = new CreatePaymentTypeDto
            {
                Name = "Credit",
                CreditPeriod = 30
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
