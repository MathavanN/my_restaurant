using Moq;
using MyRestaurant.Services;
using System;

namespace MyRestaurant.Business.Tests.Repositories.Fixtures
{
    public class RestaurantInfoRepositoryFixture : IDisposable
    {
        public Mock<IRestaurantInfoService> MockRestaurantInfoService { get; private set; }

        public RestaurantInfoRepositoryFixture()
        {
        }
        public void Dispose()
        {
            MockRestaurantInfoService = null;
        }
    }
}
