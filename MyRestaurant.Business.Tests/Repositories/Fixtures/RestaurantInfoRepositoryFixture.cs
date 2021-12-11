using Moq;
using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Models;
using MyRestaurant.Services;
using System;
using System.Collections.Generic;

namespace MyRestaurant.Business.Tests.Repositories.Fixtures
{
    public class RestaurantInfoRepositoryFixture : IDisposable
    {
        private bool _disposed;
        public Mock<IRestaurantInfoService> MockRestaurantInfoService { get; private set; }
        public IEnumerable<RestaurantInfo> RestaurantInfos { get; private set; }
        public CreateRestaurantInfoDto CreateRestaurantInfoDto { get; private set; }
        //public RestaurantInfo RestaurantInfo { get; private set; }
        public RestaurantInfoRepositoryFixture()
        {
            MockRestaurantInfoService = new Mock<IRestaurantInfoService>();

            RestaurantInfos = new List<RestaurantInfo>
            {
                new RestaurantInfo
                {
                    Id = 1,
                    Name = "Golden Dining",
                    Address = "Kandy Road, Kaithady",
                    City = "Jaffna",
                    Country = "Sri Lanka",
                    LandLine = "+9423454544",
                    Mobile = "+94567876786",
                    Email = "test@gmail.com"
                }
            };

            CreateRestaurantInfoDto = new CreateRestaurantInfoDto
            {
                Name = "Golden Dining",
                Address = "Kandy Road, Kaithady",
                City = "Jaffna",
                Country = "Sri Lanka",
                LandLine = "+9423454544",
                Mobile = "+94567876786",
                Email = "test@gmail.com"
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
                    MockRestaurantInfoService = null;
                }

                _disposed = true;
            }
        }
    }
}
