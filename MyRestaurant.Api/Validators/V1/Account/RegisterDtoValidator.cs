using FluentValidation;
using MyRestaurant.Business.Dtos.V1;

namespace MyRestaurant.Api.Validators.V1
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("FirstName is required.");

            RuleFor(x => x.LastName).NotEmpty().WithMessage("LastName is required.");

            RuleFor(x => x.Email).EmailAddress().WithMessage("Email is not a valid email address.");

            RuleFor(x => x.Password).NotEmpty().Equal(x => x.ConfirmPassword).WithMessage("Password must be same.");
        }
    }
}
