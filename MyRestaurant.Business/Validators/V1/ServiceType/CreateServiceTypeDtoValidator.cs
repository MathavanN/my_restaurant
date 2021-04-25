using FluentValidation;
using MyRestaurant.Business.Dtos.V1;

namespace MyRestaurant.Business.Validators.V1
{
    public class CreateServiceTypeDtoValidator : AbstractValidator<CreateServiceTypeDto>
    {
        public CreateServiceTypeDtoValidator()
        {
            RuleFor(x => x.Type).NotEmpty().WithMessage("Service type is required.")
                .MaximumLength(20).WithMessage("Service type maximum length is 20.");
        }
    }
}
