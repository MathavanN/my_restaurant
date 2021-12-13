using Microsoft.AspNetCore.Mvc;
using Moq;
using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Repositories.Contracts;
using System;
using System.Collections.Generic;

namespace MyRestaurant.Api.Tests.Controllers.V1.Fixtures
{
    public class TransactionControllerFixture : IDisposable
    {
        private bool _disposed;
        public ApiVersion ApiVersion { get; private set; }
        public Mock<ITransactionRepository> MockTransactionRepository { get; private set; }
        public IEnumerable<GetTransactionDto> Transactions { get; private set; }
        public CreateTransactionDto ValidCreateTransactionDto { get; private set; }
        public GetTransactionDto CreateTransactionDtoResult { get; private set; }
        public EditTransactionDto ValidEditTransactionDto { get; private set; }
        public GetTransactionDto EditTransactionDtoResult { get; private set; }

        public TransactionControllerFixture()
        {
            ApiVersion = new ApiVersion(1, 0);

            MockTransactionRepository = new Mock<ITransactionRepository>();

            var crearedAt = DateTime.Now.AddDays(-1);

            Transactions = new List<GetTransactionDto>
            {
                new GetTransactionDto {
                    Id = 1,
                    TransactionTypeId = 1,
                    TransactionType = "Food",
                    PaymentTypeId = 2,
                    PaymentType = "Credit",
                    Date = DateTime.Now.AddDays(-10),
                    Description = "Peanuts in Coke",
                    Amount = 6.5m,
                    Cashflow = "Expense",
                    CreatedAt = crearedAt
                },
                new GetTransactionDto {
                    Id = 2,
                    TransactionTypeId = 2,
                    TransactionType = "Extra Income",
                    PaymentTypeId = 1,
                    PaymentType = "Cash",
                    Date = DateTime.Now.AddDays(-5),
                    Description = "Income from sale",
                    Amount = 110.5m,
                    Cashflow = "Income",
                    CreatedAt = crearedAt
                }
            };

            ValidCreateTransactionDto = new CreateTransactionDto
            {
                TransactionTypeId = 2,
                PaymentTypeId = 1,
                Date = DateTime.Now.AddDays(-2),
                Description = "Interest from Deposit",
                Amount = 456.5m,
                Cashflow = "Income"
            };

            CreateTransactionDtoResult = new GetTransactionDto
            {
                Id = 3,
                TransactionTypeId = 2,
                TransactionType = "Extra Income",
                PaymentTypeId = 1,
                PaymentType = "Cash",
                Date = DateTime.Now.AddDays(-2),
                Description = "Interest from Deposit",
                Amount = 456.5m,
                Cashflow = "Income",
                CreatedAt = DateTime.Now
            };

            ValidEditTransactionDto = new EditTransactionDto
            {
                TransactionTypeId = 2,
                PaymentTypeId = 1,
                Date = DateTime.Now.AddDays(-5),
                Description = "Income from sale",
                Amount = 10110.5m,
                Cashflow = "Income"
            };

            EditTransactionDtoResult = new GetTransactionDto
            {
                Id = 2,
                TransactionTypeId = 2,
                TransactionType = "Extra Income",
                PaymentTypeId = 1,
                PaymentType = "Cash",
                Date = DateTime.Now.AddDays(-5),
                Description = "Income from sale",
                Amount = 10110.5m,
                Cashflow = "Income",
                CreatedAt = crearedAt
            };
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
                    MockTransactionRepository = null;
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
                }

                _disposed = true;
            }
        }
    }
}
