using FluentValidation;
using MyRestaurant.Business.Dtos.V1;

namespace MyRestaurant.Api.Validators.V1
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.Email).EmailAddress().WithMessage("Email is not a valid email address.");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.");
        }
    }
}
