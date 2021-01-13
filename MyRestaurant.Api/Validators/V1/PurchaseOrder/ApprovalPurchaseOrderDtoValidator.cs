using FluentValidation;
using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Models;

namespace MyRestaurant.Api.Validators.V1
{
    public class ApprovalPurchaseOrderDtoValidator : AbstractValidator<ApprovalPurchaseOrderDto>
    {
        public ApprovalPurchaseOrderDtoValidator()
        {
            RuleFor(x => x.ApprovalStatus).NotEmpty().WithMessage("Approval status is required.")
                .IsEnumName(typeof(PurchaseOrderStatus), caseSensitive: false);
            RuleFor(x => x.ApprovalReason).MaximumLength(500).WithMessage("Approval reason maximum length is 500.");
        }
    }
}
