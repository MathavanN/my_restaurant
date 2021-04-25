using FluentValidation;
using MyRestaurant.Business.Dtos.V1;

namespace MyRestaurant.Business.Validators.V1
{
    public class CreateTransactionTypeDtoValidator : AbstractValidator<CreateTransactionTypeDto>
    {
        public CreateTransactionTypeDtoValidator()
        {
            RuleFor(x => x.Type).NotEmpty().WithMessage("Type is required.")
                .MaximumLength(50).WithMessage("Type maximum length is 50.");

        }
    }
}
