using FluentValidation;
using MyRestaurant.Business.Dtos.V1;

namespace MyRestaurant.Business.Validators.V1
{
    public class RefreshDtoValidator : AbstractValidator<RefreshDto>
    {
        public RefreshDtoValidator()
        {
            RuleFor(x => x.RefreshToken).NotEmpty().WithMessage("RefreshToken is required.");
        }
    }
}
