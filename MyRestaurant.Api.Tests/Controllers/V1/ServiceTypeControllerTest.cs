using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MyRestaurant.Api.Controllers.V1;
using MyRestaurant.Api.Tests.Controllers.V1.Fixtures;
using MyRestaurant.Business.Dtos.V1;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MyRestaurant.Api.Tests.Controllers.V1
{
    public class ServiceTypeControllerTest : IClassFixture<ServiceTypeControllerFixture>
    {
        private readonly ServiceTypeControllerFixture _fixture;
        public ServiceTypeControllerTest(ServiceTypeControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GetServiceTypes_Returns_OkObjectResult()
        {
            //Arrange
            _fixture.MockServiceTypeRepository.Setup(x => x.GetServiceTypesAsync())
                .ReturnsAsync(_fixture.ServiceTypes);

            var controller = new ServiceTypeController(_fixture.MockServiceTypeRepository.Object);

            //Act
            var result = await controller.GetServiceTypes();

            //Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var serviceTypes = okResult.Value.Should().BeAssignableTo<IEnumerable<GetServiceTypeDto>>().Subject;

            okResult.StatusCode.Should().Be(200);
            serviceTypes.Should().HaveCount(1);
        }

        [Fact]
        public async Task GetServiceType_Returns_OkObjectResult()
        {
            //Arrange
            var id = 1;
            _fixture.MockServiceTypeRepository.Setup(x => x.GetServiceTypeAsync(It.IsAny<int>()))
                .ReturnsAsync(_fixture.ServiceTypes.Single(d => d.Id == id));

            var controller = new ServiceTypeController(_fixture.MockServiceTypeRepository.Object);

            //Act
            var result = await controller.GetServiceType(id);

            //Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var serviceType = okResult.Value.Should().BeAssignableTo<GetServiceTypeDto>().Subject;

            okResult.StatusCode.Should().Be(200);
            serviceType.Id.Should().Be(id);
            serviceType.Type.Should().Be("Take Away");
        }

        [Fact]
        public async Task CreateServiceType_Returns_CreatedAtRouteResult()
        {
            //Arrange
            _fixture.MockServiceTypeRepository.Setup(x => x.CreateServiceTypeAsync(It.IsAny<CreateServiceTypeDto>()))
                .ReturnsAsync(_fixture.CreateServiceTypeDtoResult);

            var controller = new ServiceTypeController(_fixture.MockServiceTypeRepository.Object);

            //Act
            var result = await controller.CreateServiceType(_fixture.ValidCreateServiceTypeDto, _fixture.ApiVersion);

            //Assert
            var objectResult = result.Should().BeOfType<CreatedAtRouteResult>().Subject;
            objectResult.StatusCode.Should().Be(201);
            objectResult.RouteValues["id"].Should().Be(2);
            objectResult.RouteValues["version"].Should().Be($"{_fixture.ApiVersion}");

            var serviceType = objectResult.Value.Should().BeAssignableTo<GetServiceTypeDto>().Subject;
            serviceType.Id.Should().Be(2);
            serviceType.Type.Should().Be("Dine In");
        }

        [Fact]
        public async Task UpdateServiceType_Returns_OkObjectResult()
        {
            //Arrange
            var id = 1;
            _fixture.MockServiceTypeRepository.Setup(x => x.UpdateServiceTypeAsync(It.IsAny<int>(), It.IsAny<EditServiceTypeDto>()))
                .ReturnsAsync(_fixture.EditServiceTypeDtoResult);

            var controller = new ServiceTypeController(_fixture.MockServiceTypeRepository.Object);

            //Act
            var result = await controller.UpdateServiceType(id, _fixture.ValidUpdateServiceTypeDto);

            //Assert
            var objectResult = result.Should().BeOfType<OkObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);

            var serviceType = objectResult.Value.Should().BeAssignableTo<GetServiceTypeDto>().Subject;
            serviceType.Id.Should().Be(id);
            serviceType.Type.Should().Be("Take Out");
        }

        [Fact]
        public async Task DeleteServiceType_Returns_NoContentResult()
        {
            //Arrange
            var id = 2;
            _fixture.MockServiceTypeRepository.Setup(x => x.DeleteServiceTypeAsync(It.IsAny<int>()));

            var controller = new ServiceTypeController(_fixture.MockServiceTypeRepository.Object);

            //Act
            var result = await controller.DeleteServiceType(id);

            //Assert
            var objectResult = result.Should().BeOfType<NoContentResult>().Subject;
            objectResult.StatusCode.Should().Be(204);
        }
    }
}
