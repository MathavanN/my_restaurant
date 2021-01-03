using FluentValidation;
using MyRestaurant.Business.Dtos.V1;

namespace MyRestaurant.Api.Validators.V1
{
    public class CreateSupplierDtoValidator : AbstractValidator<CreateSupplierDto>
    {
        public CreateSupplierDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.")
                .MaximumLength(256).WithMessage("Name maximum length is 256.");

            RuleFor(x => x.Address1).NotEmpty().WithMessage("Address is required")
                .MaximumLength(256).WithMessage("Address maximum length is 256.");

            RuleFor(x => x.City).NotEmpty().WithMessage("City is required")
                .MaximumLength(256).WithMessage("City maximum length is 100.");

            RuleFor(x => x.Country).NotEmpty().WithMessage("Country is required")
                .MaximumLength(256).WithMessage("Country maximum length is 100.");

            RuleFor(x => x.Email).EmailAddress().WithMessage("Email is not a valid email address.");
        }
    }
}
