using FluentAssertions;
using MyRestaurant.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MyRestaurant.Services.Tests
{
    public class RestaurantInfoServiceTest : MyRestaurantContextTestBase
    {
        public RestaurantInfoServiceTest()
        {
            RestaurantInfoInitializer.Initialize(_myRestaurantContext);
        }

        [Fact]
        public async Task GetRestaurantInfosAsync_Returns_RestaurantInfos()
        {
            //Arrange
            var service = new RestaurantInfoService(_myRestaurantContext);

            //Act
            var result = await service.GetRestaurantInfosAsync();

            //Assert
            result.Should().BeAssignableTo<IEnumerable<RestaurantInfo>>();
            result.Should().HaveCount(1);
        }

        [Fact]
        public async Task GetRestaurantInfoAsync_Returns_RestaurantInfo()
        {
            //Arrange
            var id = 1;
            var service = new RestaurantInfoService(_myRestaurantContext);

            //Act
            var result = await service.GetRestaurantInfoAsync(d => d.Id == id);

            //Assert
            result.Should().BeAssignableTo<RestaurantInfo>();
            result.Id.Should().Be(id);
            result.Name.Should().Be("Golden Dining");
        }

        [Fact]
        public async Task GetRestaurantInfoAsync_Returns_Null()
        {
            //Arrange
            var id = 10;
            var service = new RestaurantInfoService(_myRestaurantContext);

            //Act
            var result = await service.GetRestaurantInfoAsync(d => d.Id == id);

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task AddRestaurantInfoAsync_Returns_New_RestaurantInfo()
        {
            //Arrange
            var service = new RestaurantInfoService(_myRestaurantContext);

            //Act
            var result = await service.AddRestaurantInfoAsync(new RestaurantInfo
            {
                Name = "Golden Dining",
                Address = "Kandy Road, Kaithady",
                City = "Jaffna",
                Country = "Sri Lanka",
                LandLine = "+9423454545",
                Mobile = "+94567876786",
                Email = "test@gmail.com"
            });

            //Assert
            result.Should().BeAssignableTo<RestaurantInfo>();
            result.Name.Should().Be("Golden Dining");
            result.LandLine.Should().Be("+9423454545");
        }

        [Fact]
        public async Task UpdateRestaurantInfoAsync_Successfully_Updated()
        {
            //Arrange
            var id = 1;
            var service = new RestaurantInfoService(_myRestaurantContext);

            //Act
            var dbRestaurantInfo = await service.GetRestaurantInfoAsync(d => d.Id == id);
            dbRestaurantInfo.LandLine = "+9423656565";
            dbRestaurantInfo.Mobile = "+9423989898";
            dbRestaurantInfo.Email = "newtest@gmail.com";

            await service.UpdateRestaurantInfoAsync(dbRestaurantInfo);

            var result = await service.GetRestaurantInfoAsync(d => d.Id == id);

            //Assert
            result.Should().BeAssignableTo<RestaurantInfo>();
            result.Id.Should().Be(id);
            result.LandLine.Should().Be("+9423656565");
            result.Mobile.Should().Be("+9423989898");
            result.Email.Should().Be("newtest@gmail.com");
        }

        [Fact]
        public async Task DeleteRestaurantInfoAsync_Successfully_Deleted()
        {
            //Arrange
            var id = 1;
            var service = new RestaurantInfoService(_myRestaurantContext);

            //Act
            var dbRestaurantInfo = await service.GetRestaurantInfoAsync(d => d.Id == id);

            await service.DeleteRestaurantInfoAsync(dbRestaurantInfo);

            var result = await service.GetRestaurantInfoAsync(d => d.Id == id);

            //Assert
            result.Should().BeNull();
        }
    }
}
