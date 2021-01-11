using FluentValidation;
using MyRestaurant.Business.Dtos.V1;

namespace MyRestaurant.Api.Validators.V1
{
    public class CreateStockTypeDtoValidator : AbstractValidator<CreateStockTypeDto>
    {
        public CreateStockTypeDtoValidator()
        {
            RuleFor(x => x.Type).NotEmpty().WithMessage("Type is required.")
                .MaximumLength(50).WithMessage("Type maximum length is 50.");

            RuleFor(x => x.Description).MaximumLength(100).WithMessage("Description maximum length is 100.");
        }
    }
}
