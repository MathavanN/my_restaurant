using Microsoft.AspNetCore.Mvc;
using Moq;
using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Repositories.Contracts;
using System;
using System.Collections.Generic;

namespace MyRestaurant.Api.Tests.Controllers.V1.Fixtures
{
    public class RestaurantInfoControllerFixture : IDisposable
    {
        private bool _disposed;
        public ApiVersion ApiVersion { get; private set; }
        public Mock<IRestaurantInfoRepository> MockRestaurantInfoRepository { get; private set; }
        public IEnumerable<GetRestaurantInfoDto> RestaurantInfos { get; private set; }
        public CreateRestaurantInfoDto ValidCreateRestaurantInfoDto { get; private set; }
        public GetRestaurantInfoDto CreateRestaurantInfoDtoResult { get; private set; }

        public RestaurantInfoControllerFixture()
        {
            ApiVersion = new ApiVersion(1, 0);

            MockRestaurantInfoRepository = new Mock<IRestaurantInfoRepository>();

            RestaurantInfos = new List<GetRestaurantInfoDto>
            {
                new GetRestaurantInfoDto { Id = 1, Name = "Golden Dining",
                Address = "Kandy Road, Kaithady, Jaffna, Sri Lanka",
                LandLine = "+9423454544",
                Mobile = "+94567876786",
                Email = "test@gmail.com" },
            };

            ValidCreateRestaurantInfoDto = new CreateRestaurantInfoDto
            {
                Name = "Golden Dining",
                Address = "Kandy Road, Kaithady",
                City = "Jaffna",
                Country = "Sri Lanka",
                LandLine = "+9423454544",
                Mobile = "+94567876786",
                Email = "test@gmail.com"
            };

            CreateRestaurantInfoDtoResult = new GetRestaurantInfoDto
            {
                Id = 1,
                Name = "Golden Dining",
                Address = "Kandy Road, Kaithady, Jaffna, Sri Lanka",
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
                    MockRestaurantInfoRepository = null;
                }

                _disposed = true;
            }
        }
    }
}
