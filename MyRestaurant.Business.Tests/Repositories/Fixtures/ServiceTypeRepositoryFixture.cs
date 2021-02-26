using Moq;
using MyRestaurant.Services;
using System;

namespace MyRestaurant.Business.Tests.Repositories.Fixtures
{
    public class ServiceTypeRepositoryFixture : IDisposable
    {
        public Mock<IServiceTypeService> MockServiceTypeService { get; private set; }

        public ServiceTypeRepositoryFixture()
        {

        }
        public void Dispose()
        {
            MockServiceTypeService = null;
        }
    }
}
