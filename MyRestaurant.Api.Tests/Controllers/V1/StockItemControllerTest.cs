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
    public class StockItemControllerTest : IClassFixture<StockItemControllerFixture>
    {
        private readonly StockItemControllerFixture _fixture;

        public StockItemControllerTest(StockItemControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GetStockItems_Returns_OkObjectResult()
        {
            //Arrange
            _fixture.MockStockItemRepository.Setup(x => x.GetStockItemsAsync())
                .ReturnsAsync(_fixture.StockItems);

            var controller = new StockItemController(_fixture.MockStockItemRepository.Object);

            //Act
            var result = await controller.GetStockItems();

            //Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var stockItems = okResult.Value.Should().BeAssignableTo<IEnumerable<GetStockItemDto>>().Subject;

            okResult.StatusCode.Should().Be(200);
            stockItems.Should().HaveCount(3);
        }

        [Fact]
        public async Task GetStockItemsByType_Returns_OkObjectResult()
        {
            //Arrange
            _fixture.MockStockItemRepository.Setup(x => x.GetStockItemsByTypeAsync(It.IsAny<int>(), 10, 0))
                .ReturnsAsync(_fixture.StockItemEnvelop);

            var controller = new StockItemController(_fixture.MockStockItemRepository.Object);

            //Act
            var result = await controller.GetStockItemsByType(1, 10, 0);

            //Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var stockItemEnvelop = okResult.Value.Should().BeAssignableTo<StockItemEnvelop>().Subject;

            okResult.StatusCode.Should().Be(200);
            stockItemEnvelop.StockItemCount.Should().Be(2);
            stockItemEnvelop.StockItems.Should().HaveCount(2);
            stockItemEnvelop.ItemsPerPage.Should().Be(10);
            stockItemEnvelop.TotalPages.Should().Be(1);
        }

        [Fact]
        public async Task GetStockItem_Returns_OkObjectResult()
        {
            //Arrange
            var id = 2;
            _fixture.MockStockItemRepository.Setup(x => x.GetStockItemAsync(It.IsAny<long>()))
                .ReturnsAsync(_fixture.StockItems.Single(d => d.Id == id));

            var controller = new StockItemController(_fixture.MockStockItemRepository.Object);

            //Act
            var result = await controller.GetStockItem(id);

            //Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var stockItem = okResult.Value.Should().BeAssignableTo<GetStockItemDto>().Subject;

            okResult.StatusCode.Should().Be(200);
            stockItem.Id.Should().Be(id);
            stockItem.StockType.Should().Be("Beverage");
            stockItem.UnitOfMeasureCode.Should().Be("ml");
            stockItem.Name.Should().Be("Coca-Cola");
        }

        [Fact]
        public async Task CreateStockItem_Returns_CreatedAtRouteResult()
        {
            //Arrange
            _fixture.MockStockItemRepository.Setup(x => x.CreateStockItemAsync(It.IsAny<CreateStockItemDto>()))
                .ReturnsAsync(_fixture.CreateStockItemDtoResult);

            var controller = new StockItemController(_fixture.MockStockItemRepository.Object);

            //Act
            var result = await controller.CreateStockItem(_fixture.ValidCreateStockItemDto, _fixture.ApiVersion);

            //Assert
            var objectResult = result.Should().BeOfType<CreatedAtRouteResult>().Subject;
            objectResult.StatusCode.Should().Be(201);
            objectResult.RouteValues!["id"].Should().Be(4);
            objectResult.RouteValues["version"].Should().Be($"{_fixture.ApiVersion}");

            var stockItem = objectResult.Value.Should().BeAssignableTo<GetStockItemDto>().Subject;
            stockItem.Id.Should().Be(4);
            stockItem.StockType.Should().Be("Grocery");
            stockItem.UnitOfMeasureCode.Should().Be("kg");
            stockItem.Name.Should().Be("Sugar");
        }

        [Fact]
        public async Task UpdateStockItem_Returns_OkObjectResult()
        {
            //Arrange
            var id = 2;
            _fixture.MockStockItemRepository.Setup(x => x.UpdateStockItemAsync(It.IsAny<long>(), It.IsAny<EditStockItemDto>()))
                .ReturnsAsync(_fixture.EditStockItemDtoResult);

            var controller = new StockItemController(_fixture.MockStockItemRepository.Object);

            //Act
            var result = await controller.UpdateStockItem(id, _fixture.ValidEditStockItemDto);

            //Assert
            var objectResult = result.Should().BeOfType<OkObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);

            var stockItem = objectResult.Value.Should().BeAssignableTo<GetStockItemDto>().Subject;
            stockItem.Id.Should().Be(2);
            stockItem.StockType.Should().Be("Beverage");
            stockItem.UnitOfMeasureCode.Should().Be("ml");
            stockItem.Name.Should().Be("Coca-Cola");
            stockItem.ItemUnit.Should().Be(400);
        }

        [Fact]
        public async Task DeleteStockItem_Returns_NoContentResult()
        {
            //Arrange
            var id = 2;
            _fixture.MockStockItemRepository.Setup(x => x.DeleteStockItemAsync(It.IsAny<long>()));

            var controller = new StockItemController(_fixture.MockStockItemRepository.Object);

            //Act
            var result = await controller.DeleteStockItem(id);

            //Assert
            var objectResult = result.Should().BeOfType<NoContentResult>().Subject;
            objectResult.StatusCode.Should().Be(204);
        }
    }
}
