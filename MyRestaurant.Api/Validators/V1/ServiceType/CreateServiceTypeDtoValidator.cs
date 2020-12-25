using FluentValidation;
using MyRestaurant.Business.Dtos.V1.ServiceTypeDtos;

namespace MyRestaurant.Api.Validators.V1.ServiceType
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
