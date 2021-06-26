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
    public class RestaurantInfoControllerTest : IClassFixture<RestaurantInfoControllerFixture>
    {
        private readonly RestaurantInfoControllerFixture _fixture;

        public RestaurantInfoControllerTest(RestaurantInfoControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GetRestaurantInfos_Returns_OkObjectResult()
        {
            //Arrange
            _fixture.MockRestaurantInfoRepository.Setup(x => x.GetRestaurantInfosAsync())
                .ReturnsAsync(_fixture.RestaurantInfos);

            var controller = new RestaurantInfoController(_fixture.MockRestaurantInfoRepository.Object);

            //Act
            var result = await controller.GetRestaurantInfos();

            //Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var info = okResult.Value.Should().BeAssignableTo<IEnumerable<GetRestaurantInfoDto>>().Subject;

            okResult.StatusCode.Should().Be(200);
            info.Should().HaveCount(1);
        }

        [Fact]
        public async Task GetRestaurantInfo_Returns_OkObjectResult()
        {
            //Arrange
            var id = 1;
            _fixture.MockRestaurantInfoRepository.Setup(x => x.GetRestaurantInfoAsync(It.IsAny<int>()))
                .ReturnsAsync(_fixture.RestaurantInfos.Single(d => d.Id == id));

            var controller = new RestaurantInfoController(_fixture.MockRestaurantInfoRepository.Object);

            //Act
            var result = await controller.GetRestaurantInfo(id);

            //Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var info = okResult.Value.Should().BeAssignableTo<GetRestaurantInfoDto>().Subject;

            okResult.StatusCode.Should().Be(200);
            info.Id.Should().Be(id);
            info.Name.Should().Be("Golden Dining");
            info.Email.Should().Be("test@gmail.com");
        }

        [Fact]
        public async Task CreateRestaurantInfo_Returns_CreatedAtRouteResult()
        {
            //Arrange
            _fixture.MockRestaurantInfoRepository.Setup(x => x.CreateRestaurantInfoAsync(It.IsAny<CreateRestaurantInfoDto>()))
                .ReturnsAsync(_fixture.CreateRestaurantInfoDtoResult);

            var controller = new RestaurantInfoController(_fixture.MockRestaurantInfoRepository.Object);

            //Act
            var result = await controller.CreateRestaurantInfo(_fixture.ValidCreateRestaurantInfoDto, _fixture.ApiVersion);

            //Assert
            var objectResult = result.Should().BeOfType<CreatedAtRouteResult>().Subject;
            objectResult.StatusCode.Should().Be(201);
            objectResult.RouteValues["id"].Should().Be(1);
            objectResult.RouteValues["version"].Should().Be($"{_fixture.ApiVersion}");

            var info = objectResult.Value.Should().BeAssignableTo<GetRestaurantInfoDto>().Subject;
            info.Id.Should().Be(1);
            info.Name.Should().Be("Golden Dining");
            info.Email.Should().Be("test@gmail.com");
        }
    }
}
