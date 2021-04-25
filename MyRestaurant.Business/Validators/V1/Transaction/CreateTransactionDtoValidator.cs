﻿using FluentValidation;
using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Validators.Common;
using MyRestaurant.Models;
using System;

namespace MyRestaurant.Business.Validators.V1
{
    public class CreateTransactionDtoValidator : AbstractValidator<CreateTransactionDto>
    {
        public CreateTransactionDtoValidator()
        {
            RuleFor(x => x.TransactionTypeId).GreaterThan(0).WithMessage("Transaction type is required.");
            RuleFor(x => x.PaymentTypeId).GreaterThan(0).WithMessage("Payment type is required.");
            RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Amount is required.");
            RuleFor(x => x.Date).Must(CommonValidators.BeAValidDate).WithMessage("Transaction date is required.")
                .LessThan(DateTime.Now).WithMessage("Transaction date cannot be a future date.");
            RuleFor(x => x.Cashflow).NotEmpty().WithMessage("Cash flow is required.")
                .IsEnumName(typeof(Cashflow), caseSensitive: false);
            RuleFor(x => x.Description).NotEmpty().MaximumLength(500).WithMessage("Description maximum length is 500.");
        }
    }
}
