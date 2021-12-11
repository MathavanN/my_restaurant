using FluentAssertions;
using MyRestaurant.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MyRestaurant.Services.Tests
{
    public class ServiceTypeServiceTest : MyRestaurantContextTestBase
    {
        public ServiceTypeServiceTest()
        {
            ServiceTypeInitializer.Initialize(_myRestaurantContext);
        }

        [Fact]
        public async Task GetServiceTypesAsync_Returns_ServiceTypes()
        {
            //Arrange
            var service = new ServiceTypeService(_myRestaurantContext);

            //Act
            var result = await service.GetServiceTypesAsync();

            //Assert
            result.Should().BeAssignableTo<IEnumerable<ServiceType>>();
            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetServiceTypeAsync_Returns_ServiceType()
        {
            //Arrange
            var id = 1;
            var service = new ServiceTypeService(_myRestaurantContext);

            //Act
            var result = await service.GetServiceTypeAsync(d => d.Id == id);

            //Assert
            result.Should().BeAssignableTo<ServiceType>();
            result!.Id.Should().Be(id);
            result.Type.Should().Be("Take Away");
        }

        [Fact]
        public async Task GetServiceTypeAsync_Returns_Null()
        {
            //Arrange
            var id = 10;
            var service = new ServiceTypeService(_myRestaurantContext);

            //Act
            var result = await service.GetServiceTypeAsync(d => d.Id == id);

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task AddServiceTypeAsync_Returns_New_ServiceType()
        {
            //Arrange
            var service = new ServiceTypeService(_myRestaurantContext);

            //Act
            var result = await service.AddServiceTypeAsync(new ServiceType { Type = "Buffet" });

            //Assert
            result.Should().BeAssignableTo<ServiceType>();
            result.Type.Should().Be("Buffet");

            //Act
            var stockTypes = await service.GetServiceTypesAsync();

            //Assert
            stockTypes.Should().HaveCount(3);
        }

        [Fact]
        public async Task UpdateServiceTypeAsync_Successfully_Updated()
        {
            //Arrange
            var id = 1;
            var service = new ServiceTypeService(_myRestaurantContext);

            //Act
            var dbServiceType = await service.GetServiceTypeAsync(d => d.Id == id);
            dbServiceType!.Type = "Take Out";

            await service.UpdateServiceTypeAsync(dbServiceType);

            var result = await service.GetServiceTypeAsync(d => d.Id == id);

            //Assert
            result.Should().BeAssignableTo<ServiceType>();
            result!.Id.Should().Be(id);
            result.Type.Should().Be("Take Out");
        }

        [Fact]
        public async Task DeleteServiceTypeAsync_Successfully_Deleted()
        {
            //Arrange
            var id = 1;
            var service = new ServiceTypeService(_myRestaurantContext);

            //Act
            var dbServiceType = await service.GetServiceTypeAsync(d => d.Id == id);

            await service.DeleteServiceTypeAsync(dbServiceType!);

            var result = await service.GetServiceTypeAsync(d => d.Id == id);

            //Assert
            result.Should().BeNull();
        }
    }
}
