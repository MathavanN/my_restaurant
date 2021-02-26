using Moq;
using MyRestaurant.Services;
using System;

namespace MyRestaurant.Business.Tests.Repositories.Fixtures
{
    public class GoodsReceivedNoteItemRepositoryFixture : IDisposable
    {
        public Mock<IGoodsReceivedNoteItemService> MockGoodsReceivedNoteItemService { get; private set; }

        public GoodsReceivedNoteItemRepositoryFixture()
        {

        }
        public void Dispose()
        {
            MockGoodsReceivedNoteItemService = null;
        }
    }
}
