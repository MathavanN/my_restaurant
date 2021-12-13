using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Validators.V1;
using System;

namespace MyRestaurant.Business.Tests.Validators.V1.Fixtures
{
    public class ApprovalPurchaseOrderDtoValidatorFixture : IDisposable
    {
        private bool _disposed;
        public ApprovalPurchaseOrderDto Model { get; set; }
        public ApprovalPurchaseOrderDtoValidator Validator { get; private set; }

        public ApprovalPurchaseOrderDtoValidatorFixture()
        {
            Validator = new ApprovalPurchaseOrderDtoValidator();

            Model = new ApprovalPurchaseOrderDto
            {
                ApprovalStatus = "Pending",
                ApprovalReason = "Purchase order items are required"
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
