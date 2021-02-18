using Microsoft.AspNetCore.Mvc;
using Moq;
using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Repositories.Contracts;
using System;
using System.Collections.Generic;

namespace MyRestaurant.Api.Tests.Controllers.V1.Fixtures
{
    public class PaymentTypeControllerFixture : IDisposable
    {
        public ApiVersion ApiVersion { get; private set; }
        public Mock<IPaymentTypeRepository> MockPaymentTypeRepository { get; private set; }
        public IEnumerable<GetPaymentTypeDto> PaymentTypes { get; private set; }
        public CreatePaymentTypeDto ValidCreatePaymentTypeDto { get; private set; }
        public GetPaymentTypeDto CreatePaymentTypeDtoResult { get; private set; }
        public EditPaymentTypeDto ValidUpdatePaymentTypeDto { get; private set; }
        public GetPaymentTypeDto EditPaymentTypeDtoResult { get; private set; }

        public PaymentTypeControllerFixture()
        {
            ApiVersion = new ApiVersion(1, 0);
            
            MockPaymentTypeRepository = new Mock<IPaymentTypeRepository>();
            
            PaymentTypes = new List<GetPaymentTypeDto> {
                new GetPaymentTypeDto { Id = 1, Name = "Cash", CreditPeriod = 0 },
                new GetPaymentTypeDto { Id = 2, Name = "Credit", CreditPeriod = 30 }
            };

            ValidCreatePaymentTypeDto = new CreatePaymentTypeDto { Name = "Credit1", CreditPeriod = 15 };

            CreatePaymentTypeDtoResult = new GetPaymentTypeDto { Id = 3, Name = "Credit1", CreditPeriod = 15 };

            ValidUpdatePaymentTypeDto = new EditPaymentTypeDto { Name = "Credit", CreditPeriod = 20 };

            EditPaymentTypeDtoResult = new GetPaymentTypeDto { Id = 2, Name = "Credit", CreditPeriod = 20 };
        }

        public void Dispose()
        {
            MockPaymentTypeRepository = null;
        }
    }
}
