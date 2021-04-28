using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MyRestaurant.Api.Controllers.V1;
using MyRestaurant.Api.Tests.Controllers.V1.Fixtures;
using MyRestaurant.Business.Dtos.V1;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MyRestaurant.Api.Tests.Controllers.V1
{
    public class StockTypeControllerTest : IClassFixture<StockTypeControllerFixture>
    {
        private readonly StockTypeControllerFixture _fixture;

        public StockTypeControllerTest(StockTypeControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async void GetStockTypes_Returns_OkObjectResult()
        {
            //Arrange
            _fixture.MockStockTypeRepository.Setup(x => x.GetStockTypesAsync())
                .ReturnsAsync(_fixture.StockTypes);

            var controller = new StockTypeController(_fixture.MockStockTypeRepository.Object);

            //Act
            var result = await controller.GetStockTypes();

            //Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var stockTypes = okResult.Value.Should().BeAssignableTo<IEnumerable<GetStockTypeDto>>().Subject;

            okResult.StatusCode.Should().Be(200);
            stockTypes.Should().HaveCount(2);
        }

        [Fact]
        public async void GetStockType_Returns_OkObjectResult()
        {
            //Arrange
            var id = 2;
            _fixture.MockStockTypeRepository.Setup(x => x.GetStockTypeAsync(id))
                .ReturnsAsync(_fixture.StockTypes.Single(d => d.Id == id));

            var controller = new StockTypeController(_fixture.MockStockTypeRepository.Object);

            //Act
            var result = await controller.GetStockType(id);

            //Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var stockType = okResult.Value.Should().BeAssignableTo<GetStockTypeDto>().Subject;

            okResult.StatusCode.Should().Be(200);
            stockType.Id.Should().Be(id);
            stockType.Type.Should().Be("Beverage");
            stockType.Description.Should().Be("beverage items");
        }

        [Fact]
        public async void CreateStockType_Returns_CreatedAtRouteResult()
        {
            //Arrange
            _fixture.MockStockTypeRepository.Setup(x => x.CreateStockTypeAsync(_fixture.ValidCreateStockTypeDto))
                .ReturnsAsync(_fixture.CreateStockTypeDtoResult);

            var controller = new StockTypeController(_fixture.MockStockTypeRepository.Object);

            //Act
            var result = await controller.CreateStockType(_fixture.ValidCreateStockTypeDto, _fixture.ApiVersion);

            //Assert
            var objectResult = result.Should().BeOfType<CreatedAtRouteResult>().Subject;
            objectResult.StatusCode.Should().Be(201);
            objectResult.RouteValues["id"].Should().Be(3);
            objectResult.RouteValues["version"].Should().Be($"{_fixture.ApiVersion}");

            var stockType = objectResult.Value.Should().BeAssignableTo<GetStockTypeDto>().Subject;
            stockType.Id.Should().Be(3);
            stockType.Type.Should().Be("Kitchen");
            stockType.Description.Should().Be("kitchen items");
        }

        [Fact]
        public async void UpdateStockType_Returns_OkObjectResult()
        {
            //Arrange
            var id = 2;
            _fixture.MockStockTypeRepository.Setup(x => x.UpdateStockTypeAsync(id, _fixture.ValidEditStockTypeDto))
                .ReturnsAsync(_fixture.EditStockTypeDtoResult);

            var controller = new StockTypeController(_fixture.MockStockTypeRepository.Object);

            //Act
            var result = await controller.UpdateStockType(id, _fixture.ValidEditStockTypeDto);

            //Assert
            var objectResult = result.Should().BeOfType<OkObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);

            var stockType = objectResult.Value.Should().BeAssignableTo<GetStockTypeDto>().Subject;
            stockType.Id.Should().Be(2);
            stockType.Type.Should().Be("Beverage");
            stockType.Description.Should().Be("Beverage items to add");
        }

        [Fact]
        public async void DeleteStockType_Returns_NoContentResult()
        {
            //Arrange
            var id = 2;
            _fixture.MockStockTypeRepository.Setup(x => x.DeleteStockTypeAsync(id));

            var controller = new StockTypeController(_fixture.MockStockTypeRepository.Object);

            //Act
            var result = await controller.DeleteStockType(id);

            //Assert
            var objectResult = result.Should().BeOfType<NoContentResult>().Subject;
            objectResult.StatusCode.Should().Be(204);
        }
    }
}