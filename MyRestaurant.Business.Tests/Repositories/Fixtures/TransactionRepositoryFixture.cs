using Moq;
using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Models;
using MyRestaurant.Services;
using System;
using System.Collections.Generic;

namespace MyRestaurant.Business.Tests.Repositories.Fixtures
{
    public class TransactionRepositoryFixture : IDisposable
    {
        private bool _disposed;
        public Mock<ITransactionService> MockTransactionService { get; private set; }
        public IEnumerable<Transaction> Transactions { get; private set; }
        public CreateTransactionDto CreateTransactionDto { get; private set; }
        public EditTransactionDto EditTransactionDto { get; private set; }
        public Transaction CreatedNewTransaction { get; private set; }

        public TransactionRepositoryFixture()
        {
            MockTransactionService = new Mock<ITransactionService>();
            var paymentTypeCash = new PaymentType { Id = 1, Name = "Cash", CreditPeriod = 0 };
            var paymentTypeCredit = new PaymentType { Id = 2, Name = "Credit", CreditPeriod = 15 };

            var transactionType1 = new TransactionType { Id = 1, Type = "Food" };
            var transactionType2 = new TransactionType { Id = 2, Type = "Extra Income" };

            Transactions = new List<Transaction> {
                new Transaction {
                    Id = 1,
                    TransactionTypeId = 1,
                    TransactionType = transactionType1,
                    PaymentTypeId = 2,
                    PaymentType = paymentTypeCredit,
                    Date = DateTime.Now.AddDays(-10),
                    Description = "Peanuts in Coke",
                    Amount = 6.5m,
                    Cashflow = Cashflow.Expense,
                    CreatedAt = DateTime.Now
                },
                new Transaction {
                    Id = 2,
                    TransactionTypeId = 2,
                    TransactionType = transactionType2,
                    PaymentTypeId = 1,
                    PaymentType = paymentTypeCash,
                    Date = DateTime.Now.AddDays(-5),
                    Description = "Income from sale",
                    Amount = 110.5m,
                    Cashflow = Cashflow.Income,
                    CreatedAt = DateTime.Now
                }
            };

            CreateTransactionDto = new CreateTransactionDto
            {
                TransactionTypeId = 2,
                PaymentTypeId = 1,
                Date = DateTime.Now.AddDays(-2),
                Description = "Interest from Deposit",
                Amount = 456.5m,
                Cashflow = "Income"
            };

            CreatedNewTransaction = new Transaction
            {
                Id = 3,
                TransactionTypeId = 2,
                TransactionType = transactionType2,
                PaymentTypeId = 1,
                PaymentType = paymentTypeCash,
                Date = DateTime.Now.AddDays(-2),
                Description = "Interest from Deposit",
                Amount = 456.5m,
                Cashflow = Cashflow.Income,
                CreatedAt = DateTime.Now
            };

            EditTransactionDto = new EditTransactionDto
            {
                TransactionTypeId = 2,
                PaymentTypeId = 1,
                Date = DateTime.Now.AddDays(-5),
                Description = "Income from sale",
                Amount = 10110.5m,
                Cashflow = "Income"
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
                    MockTransactionService = null;
                }

                _disposed = true;
            }
        }
    }
}
