using MyRestaurant.Business.Tests.Repositories.Fixtures;
using Xunit;

namespace MyRestaurant.Business.Tests.Repositories
{
    public class ServiceTypeRepositoryTest : IClassFixture<ServiceTypeRepositoryFixture>
    {
        private readonly ServiceTypeRepositoryFixture _fixture;
        public ServiceTypeRepositoryTest(ServiceTypeRepositoryFixture fixture)
        {
            _fixture = fixture;
        }
    }
}
