using Moq;
using MyRestaurant.Services;
using System;

namespace MyRestaurant.Business.Tests.Repositories.Fixtures
{
    public class PurchaseOrderItemRepositoryFixture : IDisposable
    {
        public Mock<IPurchaseOrderItemService> MockPurchaseOrderItemService { get; private set; }

        public PurchaseOrderItemRepositoryFixture()
        {
        }
        public void Dispose()
        {
            MockPurchaseOrderItemService = null;
        }
    }
}
