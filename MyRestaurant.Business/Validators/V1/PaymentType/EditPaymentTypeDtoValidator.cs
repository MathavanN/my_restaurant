using FluentValidation;
using MyRestaurant.Business.Dtos.V1;

namespace MyRestaurant.Business.Validators.V1
{
    public class EditPaymentTypeDtoValidator : AbstractValidator<EditPaymentTypeDto>
    {
        public EditPaymentTypeDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.")
                .MaximumLength(30).WithMessage("Name maximum length is 30.");
        }
    }
}
