using Moq;
using MyRestaurant.Models;
using MyRestaurant.Services;
using System.Collections.Generic;

namespace MyRestaurant.Business.Tests.Repositories.Fixtures
{
    public class PaymentTypeRepositoryFixture
    {
        public Mock<IPaymentTypeService> MockPaymentTypeService { get; private set; }

        public IEnumerable<PaymentType> PaymentTypes { get; private set; }

        public PaymentTypeRepositoryFixture()
        {
            MockPaymentTypeService = new Mock<IPaymentTypeService>();

            PaymentTypes = new List<PaymentType> {
                new PaymentType { Id = 1, Name = "Cash", CreditPeriod = 0 },
                new PaymentType { Id = 2, Name = "Credit", CreditPeriod = 15 }
            };
        }
    }
}
