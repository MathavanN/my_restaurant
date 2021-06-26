using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Validators.V1;
using System;

namespace MyRestaurant.Business.Tests.Validators.V1.Fixtures
{
    public class EditPaymentTypeDtoValidatorFixture : IDisposable
    {
        private bool _disposed;
        public EditPaymentTypeDto Model { get; set; }
        public EditPaymentTypeDtoValidator Validator { get; private set; }

        public EditPaymentTypeDtoValidatorFixture()
        {
            Validator = new EditPaymentTypeDtoValidator();
            Model = new EditPaymentTypeDto
            {
                Name = "Credit",
                CreditPeriod = 15
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
