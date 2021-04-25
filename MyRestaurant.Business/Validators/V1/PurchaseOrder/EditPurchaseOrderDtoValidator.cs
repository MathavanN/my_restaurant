using FluentValidation;
using MyRestaurant.Business.Dtos.V1;

namespace MyRestaurant.Business.Validators.V1
{
    public class EditPurchaseOrderDtoValidator : AbstractValidator<EditPurchaseOrderDto>
    {
        public EditPurchaseOrderDtoValidator()
        {
            RuleFor(x => x.SupplierId).GreaterThan(0).WithMessage("Supplier is required.");
            RuleFor(x => x.Description).MaximumLength(500).WithMessage("Description maximum length is 500.");
        }
    }
}
