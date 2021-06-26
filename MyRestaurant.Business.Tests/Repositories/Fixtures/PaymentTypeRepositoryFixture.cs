using Moq;
using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Models;
using MyRestaurant.Services;
using System;
using System.Collections.Generic;

namespace MyRestaurant.Business.Tests.Repositories.Fixtures
{
    public class PaymentTypeRepositoryFixture : IDisposable
    {
        private bool _disposed;
        public Mock<IPaymentTypeService> MockPaymentTypeService { get; private set; }
        public IEnumerable<PaymentType> PaymentTypes { get; private set; }
        public CreatePaymentTypeDto CreatePaymentTypeDto { get; private set; }
        public EditPaymentTypeDto EditPaymentTypeDto { get; private set; }
        public PaymentType CreatedNewPaymentType { get; private set; }

        public PaymentTypeRepositoryFixture()
        {
            MockPaymentTypeService = new Mock<IPaymentTypeService>();

            PaymentTypes = new List<PaymentType> {
                new PaymentType { Id = 1, Name = "Cash", CreditPeriod = 0 },
                new PaymentType { Id = 2, Name = "Credit", CreditPeriod = 15 }
            };

            CreatePaymentTypeDto = new CreatePaymentTypeDto { Name = "Credit1", CreditPeriod = 15 };

            CreatedNewPaymentType = new PaymentType { Id = 3, Name = CreatePaymentTypeDto.Name, CreditPeriod = CreatePaymentTypeDto.CreditPeriod };

            EditPaymentTypeDto = new EditPaymentTypeDto { Name = "Credit", CreditPeriod = 20 };
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
                    MockPaymentTypeService = null;
                }

                _disposed = true;
            }
        }
    }
}
