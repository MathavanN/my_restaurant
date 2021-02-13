using FluentValidation;
using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Api.Validators.Common;
using System;

namespace MyRestaurant.Api.Validators.V1
{
    public class EditGoodsReceivedNoteDtoValidator : AbstractValidator<EditGoodsReceivedNoteDto>
    {
        public EditGoodsReceivedNoteDtoValidator()
        {
            RuleFor(x => x.PurchaseOrderId).GreaterThan(0).WithMessage("Purchase order is required.");
            RuleFor(x => x.InvoiceNumber).NotEmpty().WithMessage("Invoice number is required.")
                .MaximumLength(30).WithMessage("Invoice number maximum length is 30.");
            RuleFor(x => x.PaymentTypeId).GreaterThan(0).WithMessage("Payment type is required.");
            RuleFor(x => x.Nbt).GreaterThanOrEqualTo(0).WithMessage("NBT must be a positive value.");
            RuleFor(x => x.Vat).GreaterThanOrEqualTo(0).WithMessage("VAT must be a positive value.");
            RuleFor(x => x.Discount).GreaterThanOrEqualTo(0).WithMessage("Discount must be a positive value.");
            RuleFor(x => x.ReceivedBy).NotEmpty().WithMessage("Received by is required.");
            RuleFor(x => x.ReceivedDate).Must(CommonValidators.BeAValidDate).WithMessage("Received date is required.")
                .LessThan(DateTime.Now).WithMessage("Received date cannot be a future date.");
        }

    }
}