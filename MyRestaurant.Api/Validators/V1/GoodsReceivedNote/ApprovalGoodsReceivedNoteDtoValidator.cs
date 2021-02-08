using FluentValidation;
using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Models;

namespace MyRestaurant.Api.Validators.V1
{
    public class ApprovalGoodsReceivedNoteDtoValidator : AbstractValidator<ApprovalGoodsReceivedNoteDto>
    {
        public ApprovalGoodsReceivedNoteDtoValidator()
        {
            RuleFor(x => x.ApprovalStatus).NotEmpty().WithMessage("Approval status is required.")
                .IsEnumName(typeof(Status), caseSensitive: false);
            RuleFor(x => x.ApprovalReason).MaximumLength(500).WithMessage("Approval reason maximum length is 500.");
        }
    }
}