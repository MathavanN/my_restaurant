using FluentValidation;
using MyRestaurant.Business.Dtos.V1;

namespace MyRestaurant.Business.Validators.V1
{
    public class EditSupplierDtoValidator : AbstractValidator<EditSupplierDto>
    {
        public EditSupplierDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.")
                .MaximumLength(256).WithMessage("Name maximum length is 256.");

            RuleFor(x => x.Address1).NotEmpty().WithMessage("Address is required.")
                .MaximumLength(256).WithMessage("Address maximum length is 256.");

            RuleFor(x => x.City).NotEmpty().WithMessage("City is required.")
                .MaximumLength(100).WithMessage("City maximum length is 100.");

            RuleFor(x => x.Country).NotEmpty().WithMessage("Country is required.")
                .MaximumLength(100).WithMessage("Country maximum length is 100.");

            RuleFor(x => x.Email).EmailAddress()
                .When(x => !string.IsNullOrEmpty(x.Email))
                .WithMessage("Email is not a valid email address.");
        }
    }
}
