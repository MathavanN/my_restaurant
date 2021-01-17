using FluentValidation;
using MyRestaurant.Business.Dtos.V1;

namespace MyRestaurant.Api.Validators.V1
{
    public class EditPurchaseOrderDtoValidator : AbstractValidator<EditPurchaseOrderDto>
    {
        public EditPurchaseOrderDtoValidator()
        {
            RuleFor(x => x.SupplierId).GreaterThan(0).WithMessage("Supplier is required.");
            RuleFor(x => x.Discount).GreaterThanOrEqualTo(0).WithMessage("Discount must be a positive value.");
            RuleFor(x => x.Description).MaximumLength(500).WithMessage("Service type maximum length is 500.");
        }
    }
}
