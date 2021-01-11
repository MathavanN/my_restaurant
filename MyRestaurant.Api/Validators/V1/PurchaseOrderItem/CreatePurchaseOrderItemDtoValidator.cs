using FluentValidation;
using MyRestaurant.Business.Dtos.V1;

namespace MyRestaurant.Api.Validators.V1
{
    public class CreatePurchaseOrderItemDtoValidator : AbstractValidator<CreatePurchaseOrderItemDto>
    {
        public CreatePurchaseOrderItemDtoValidator()
        {
            RuleFor(x => x.PurchaseOrderId).GreaterThan(0).WithMessage("Purchase Order is required.");
            RuleFor(x => x.ItemId).GreaterThan(0).WithMessage("Item is required.");
            RuleFor(x => x.Discount).GreaterThanOrEqualTo(0).WithMessage("Discount must be a positive value.");
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Quantity must be grater than zero.");
            RuleFor(x => x.ItemUnitPrice).GreaterThan(0).WithMessage("Item unit price must be greater than zero.");
        }
    }
}
