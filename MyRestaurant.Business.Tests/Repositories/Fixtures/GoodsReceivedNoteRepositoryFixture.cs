using Moq;
using MyRestaurant.Services;
using System;

namespace MyRestaurant.Business.Tests.Repositories.Fixtures
{
    public class GoodsReceivedNoteRepositoryFixture : IDisposable
    {
        public Mock<IGoodsReceivedNoteService> MockGoodsReceivedNoteService { get; private set; }

        public GoodsReceivedNoteRepositoryFixture()
        {
        }
        public void Dispose()
        {
            MockGoodsReceivedNoteService = null;
        }
    }
}
