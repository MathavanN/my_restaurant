using FluentValidation;
using MyRestaurant.Business.Dtos.V1;

namespace MyRestaurant.Api.Validators.V1
{
    public class CreateGoodsReceivedNoteItemDtoValidator : AbstractValidator<CreateGoodsReceivedNoteItemDto>
    {
        public CreateGoodsReceivedNoteItemDtoValidator()
        {
            RuleFor(x => x.GoodsReceivedNoteId).GreaterThan(0).WithMessage("GRN is required.");
            RuleFor(x => x.ItemId).GreaterThan(0).WithMessage("Item type is required.");
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Quantity must be grater than zero.");
            RuleFor(x => x.ItemUnitPrice).GreaterThan(0).WithMessage("Item unit price must be greater than zero.");
            RuleFor(x => x.Discount).GreaterThanOrEqualTo(0).WithMessage("Discount must be a positive value.");
            RuleFor(x => x.Nbt).GreaterThanOrEqualTo(0).WithMessage("NBT must be a positive value.");
            RuleFor(x => x.Vat).GreaterThanOrEqualTo(0).WithMessage("VAT must be a positive value.");
        }
    }
}
