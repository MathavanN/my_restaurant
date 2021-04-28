using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Validators.V1;
using System;

namespace MyRestaurant.Business.Tests.Validators.V1.Fixtures
{
    public class ApprovalPurchaseOrderDtoValidatorFixture : IDisposable
    {
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
            Model = null;
            Validator = null;
        }
    }
}
