using Moq;
using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Models;
using MyRestaurant.Services;
using System;
using System.Collections.Generic;

namespace MyRestaurant.Business.Tests.Repositories.Fixtures
{
    public class TransactionTypeRepositoryFixture : IDisposable
    {
        private bool _disposed;
        public Mock<ITransactionTypeService> MockTransactionTypeService { get; private set; }
        public IEnumerable<TransactionType> TransactionTypes { get; private set; }
        public CreateTransactionTypeDto CreateTransactionTypeDto { get; private set; }
        public EditTransactionTypeDto EditTransactionTypeDto { get; private set; }
        public TransactionType CreatedNewTransactionType { get; private set; }

        public TransactionTypeRepositoryFixture()
        {
            MockTransactionTypeService = new Mock<ITransactionTypeService>();

            TransactionTypes = new List<TransactionType>
            {
                new TransactionType { Id = 1, Type = "Food" },
                new TransactionType { Id = 2, Type = "Transportation" },
                new TransactionType { Id = 3, Type = "Shopping" },
                new TransactionType { Id = 4, Type = "Mortgage" },
                new TransactionType { Id = 5, Type = "Extra Income" }
            };

            CreateTransactionTypeDto = new CreateTransactionTypeDto { Type = "Utilities" };
            CreatedNewTransactionType = new TransactionType { Id = 6, Type = CreateTransactionTypeDto.Type };
            EditTransactionTypeDto = new EditTransactionTypeDto { Type = "Mortgage/Rent" };
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
                    MockTransactionTypeService = null;
                }

                _disposed = true;
            }
        }
    }
}
