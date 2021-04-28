using FluentValidation;
using MyRestaurant.Business.Dtos.V1;

namespace MyRestaurant.Business.Validators.V1
{
    public class EditPurchaseOrderItemDtoValidator : AbstractValidator<EditPurchaseOrderItemDto>
    {
        public EditPurchaseOrderItemDtoValidator()
        {
            RuleFor(x => x.PurchaseOrderId).GreaterThan(0).WithMessage("Purchase order id is required.");
            RuleFor(x => x.ItemId).GreaterThan(0).WithMessage("Item type is required.");
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Quantity must be grater than zero.");
            RuleFor(x => x.ItemUnitPrice).GreaterThan(0).WithMessage("Item unit price must be greater than zero.");
        }
    }
}
