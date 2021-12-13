using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Validators.V1;
using System;

namespace MyRestaurant.Business.Tests.Validators.V1.Fixtures
{
    public class EditTransactionDtoValidatorFixture : IDisposable
    {
        private bool _disposed;
        public EditTransactionDto Model { get; set; }
        public EditTransactionDtoValidator Validator { get; private set; }

        public EditTransactionDtoValidatorFixture()
        {
            Validator = new EditTransactionDtoValidator();

            Model = new EditTransactionDto
            {
                TransactionTypeId = 2,
                PaymentTypeId = 1,
                Date = DateTime.Now.AddDays(-5),
                Description = "Income from sale",
                Amount = 10110.5m,
                Cashflow = "Income"
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
