using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Validators.V1;
using System;

namespace MyRestaurant.Business.Tests.Validators.V1.Fixtures
{
    public class CreateTransactionTypeDtoValidatorFixture : IDisposable
    {
        private bool _disposed;
        public CreateTransactionTypeDto Model { get; set; }
        public CreateTransactionTypeDtoValidator Validator { get; private set; }
        public CreateTransactionTypeDtoValidatorFixture()
        {
            Validator = new CreateTransactionTypeDtoValidator();

            Model = new CreateTransactionTypeDto
            {
                Type = "Food"
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
