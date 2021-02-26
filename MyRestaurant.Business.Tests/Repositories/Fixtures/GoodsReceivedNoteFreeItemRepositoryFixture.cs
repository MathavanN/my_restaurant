using Moq;
using MyRestaurant.Services;
using System;

namespace MyRestaurant.Business.Tests.Repositories.Fixtures
{
    public class GoodsReceivedNoteFreeItemRepositoryFixture : IDisposable
    {
        public Mock<IGoodsReceivedNoteFreeItemService> MockGoodsReceivedNoteFreeItemService { get; private set; }

        public GoodsReceivedNoteFreeItemRepositoryFixture()
        {

        }
        public void Dispose()
        {
            MockGoodsReceivedNoteFreeItemService = null;
        }
    }
}
