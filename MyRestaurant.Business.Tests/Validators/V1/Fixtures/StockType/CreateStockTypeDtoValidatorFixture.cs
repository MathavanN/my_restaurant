using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Validators.V1;
using System;

namespace MyRestaurant.Business.Tests.Validators.V1.Fixtures
{
    public class CreateStockTypeDtoValidatorFixture : IDisposable
    {
        private bool _disposed;
        public CreateStockTypeDto Model { get; set; }
        public CreateStockTypeDtoValidator Validator { get; private set; }

        public CreateStockTypeDtoValidatorFixture()
        {
            Validator = new CreateStockTypeDtoValidator();
            Model = new CreateStockTypeDto
            {
                Type = "Grocery",
                Description = "Items for grocery type"
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
