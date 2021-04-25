using FluentValidation;
using MyRestaurant.Business.Dtos.V1;

namespace MyRestaurant.Business.Validators.V1
{
    public class CreateStockItemDtoValidator : AbstractValidator<CreateStockItemDto>
    {
        public CreateStockItemDtoValidator()
        {
            RuleFor(x => x.TypeId).GreaterThan(0).WithMessage("Item type is required.");
            RuleFor(x => x.UnitOfMeasureId).GreaterThan(0).WithMessage("Unit of measure is required.");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Item name is required.")
                .MaximumLength(250).WithMessage("Item name maximum length is 250.");
            RuleFor(x => x.ItemUnit).GreaterThan(0).WithMessage("Item unit should be greater than 0.");
            RuleFor(x => x.Description).MaximumLength(500).WithMessage("Description maximum length is 500.");
        }
    }
}
