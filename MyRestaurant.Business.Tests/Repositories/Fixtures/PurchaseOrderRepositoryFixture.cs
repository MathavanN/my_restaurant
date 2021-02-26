using Moq;
using MyRestaurant.Services;
using System;

namespace MyRestaurant.Business.Tests.Repositories.Fixtures
{
    public class PurchaseOrderRepositoryFixture : IDisposable
    {
        public Mock<IPurchaseOrderService> MockPurchaseOrderService { get; private set; }

        public PurchaseOrderRepositoryFixture()
        {

        }
        public void Dispose()
        {
            MockPurchaseOrderService = null;
        }
    }
}
