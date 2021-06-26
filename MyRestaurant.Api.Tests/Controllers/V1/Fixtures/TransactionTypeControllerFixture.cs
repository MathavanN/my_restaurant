using Microsoft.AspNetCore.Mvc;
using Moq;
using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Repositories.Contracts;
using System;
using System.Collections.Generic;

namespace MyRestaurant.Api.Tests.Controllers.V1.Fixtures
{
    public class TransactionTypeControllerFixture : IDisposable
    {
        private bool _disposed;
        public ApiVersion ApiVersion { get; private set; }
        public Mock<ITransactionTypeRepository> MockTransactionTypeRepository { get; private set; }
        public IEnumerable<GetTransactionTypeDto> TransactionTypes { get; private set; }
        public CreateTransactionTypeDto ValidCreateTransactionTypeDto { get; private set; }
        public GetTransactionTypeDto CreateTransactionTypeDtoResult { get; private set; }
        public EditTransactionTypeDto ValidEditTransactionTypeDto { get; private set; }
        public GetTransactionTypeDto EditTransactionTypeDtoResult { get; private set; }

        public TransactionTypeControllerFixture()
        {
            ApiVersion = new ApiVersion(1, 0);

            MockTransactionTypeRepository = new Mock<ITransactionTypeRepository>();

            TransactionTypes = new List<GetTransactionTypeDto>
            {
                new GetTransactionTypeDto { Id = 1, Type = "Food" },
                new GetTransactionTypeDto { Id = 2, Type = "Extra Income" },
                new GetTransactionTypeDto { Id = 3, Type = "Mortgage" }
            };

            ValidCreateTransactionTypeDto = new CreateTransactionTypeDto
            {
                Type = "Salary"
            };

            CreateTransactionTypeDtoResult = new GetTransactionTypeDto
            {
                Id = 4,
                Type = "Salary"
            };

            ValidEditTransactionTypeDto = new EditTransactionTypeDto
            {
                Type = "Mortgage/Rent"
            };

            EditTransactionTypeDtoResult = new GetTransactionTypeDto
            {
                Id = 3,
                Type = "Mortgage/Rent"
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
                    MockTransactionTypeRepository = null;
                }

                _disposed = true;
            }
        }
    }
}
