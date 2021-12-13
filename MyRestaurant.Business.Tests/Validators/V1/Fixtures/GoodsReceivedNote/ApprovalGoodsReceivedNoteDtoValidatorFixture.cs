using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Validators.V1;
using System;

namespace MyRestaurant.Business.Tests.Validators.V1.Fixtures
{
    public class ApprovalGoodsReceivedNoteDtoValidatorFixture : IDisposable
    {
        private bool _disposed;
        public ApprovalGoodsReceivedNoteDto Model { get; set; }
        public ApprovalGoodsReceivedNoteDtoValidator Validator { get; private set; }

        public ApprovalGoodsReceivedNoteDtoValidatorFixture()
        {
            Validator = new ApprovalGoodsReceivedNoteDtoValidator();

            Model = new ApprovalGoodsReceivedNoteDto
            {
                ApprovalStatus = "Pending",
                ApprovalReason = "GRN items are received"
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
