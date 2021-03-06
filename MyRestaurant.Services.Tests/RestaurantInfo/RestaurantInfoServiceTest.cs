using FluentAssertions;
using MyRestaurant.Models;
using System.Collections.Generic;
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
        public async void GetRestaurantInfosAsync_Returns_RestaurantInfos()
        {
            //Arrange
            var service = new RestaurantInfoService(_myRestaurantContext);

            //Act
            var result = await service.GetRestaurantInfosAsync();

            //Assert
            var restaurantInfos = result.Should().BeAssignableTo<IEnumerable<RestaurantInfo>>().Subject;
            restaurantInfos.Should().HaveCount(1);
        }

        [Fact]
        public async void GetRestaurantInfoAsync_Returns_RestaurantInfo()
        {
            //Arrange
            var id = 1;
            var service = new RestaurantInfoService(_myRestaurantContext);

            //Act
            var result = await service.GetRestaurantInfoAsync(d => d.Id == id);

            //Assert
            var restaurantInfo = result.Should().BeAssignableTo<RestaurantInfo>().Subject;
            restaurantInfo.Id.Should().Be(id);
            restaurantInfo.Name.Should().Be("Golden Dining");
        }

        [Fact]
        public async void GetRestaurantInfoAsync_Returns_Null()
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
        public async void AddRestaurantInfoAsync_Returns_New_RestaurantInfo()
        {
            //Arrange
            var service = new RestaurantInfoService(_myRestaurantContext);

            //Act
            var result = await service.AddRestaurantInfoAsync(new RestaurantInfo {
                Name = "Golden Dining",
                Address = "Kandy Road, Kaithady",
                City = "Jaffna",
                Country = "Sri Lanka",
                LandLine = "+9423454545",
                Mobile = "+94567876786",
                Email = "test@gmail.com"
            });

            //Assert
            var restaurantInfo = result.Should().BeAssignableTo<RestaurantInfo>().Subject;
            restaurantInfo.Name.Should().Be("Golden Dining");
            restaurantInfo.LandLine.Should().Be("+9423454545");
        }

        [Fact]
        public async void UpdateRestaurantInfoAsync_Successfully_Updated()
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
            var restaurantInfo = result.Should().BeAssignableTo<RestaurantInfo>().Subject;
            restaurantInfo.Id.Should().Be(id);
            restaurantInfo.LandLine.Should().Be("+9423656565");
            restaurantInfo.Mobile.Should().Be("+9423989898");
            restaurantInfo.Email.Should().Be("newtest@gmail.com");
        }

        [Fact]
        public async void DeleteRestaurantInfoAsync_Successfully_Deleted()
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
