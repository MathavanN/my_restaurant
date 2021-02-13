using FluentValidation;
using MyRestaurant.Business.Dtos.V1;

namespace MyRestaurant.Api.Validators.V1
{

    public class RegisterNormalDtoValidator : AbstractValidator<RegisterNormalDto>
    {
        public RegisterNormalDtoValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("FirstName is required.");

            RuleFor(x => x.LastName).NotEmpty().WithMessage("LastName is required.");

            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.")
                                 .NotNull().WithMessage("Email must not be empty.")
                                 .EmailAddress().WithMessage("Email is not a valid email address.");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.")
                                    .Equal(x => x.ConfirmPassword).WithMessage("Password must be same.");

            RuleFor(x => x.Roles).NotEmpty().WithMessage("Role is required.");
            RuleForEach(x => x.Roles).IsEnumName(typeof(NormalUserRoles), caseSensitive: false);
        }
    }
}
