using FluentValidation;
using MyRestaurant.Business.Dtos.V1;

namespace MyRestaurant.Business.Validators.V1
{
    public class EditUnitOfMeasureDtoValidator : AbstractValidator<EditUnitOfMeasureDto>
    {
        public EditUnitOfMeasureDtoValidator()
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage("Code is required.")
                .MaximumLength(20).WithMessage("Code maximum length is 20.");

            RuleFor(x => x.Description).MaximumLength(50).WithMessage("Description maximum length is 50.");
        }
    }
}
