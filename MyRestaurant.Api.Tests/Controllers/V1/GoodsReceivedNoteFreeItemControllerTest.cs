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
    public class GoodsReceivedNoteFreeItemControllerTest : IClassFixture<GoodsReceivedNoteFreeItemControllerFixture>
    {
        private readonly GoodsReceivedNoteFreeItemControllerFixture _fixture;

        public GoodsReceivedNoteFreeItemControllerTest(GoodsReceivedNoteFreeItemControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GetGoodsReceivedNoteFreeItems_Returns_OkObjectResult()
        {
            //Arrange
            _fixture.MockGoodsReceivedNoteFreeItemRepository.Setup(x => x.GetGoodsReceivedNoteFreeItemsAsync(It.IsAny<long>()))
                .ReturnsAsync(_fixture.GoodsReceivedNoteFreeItems.Where(d => d.GoodsReceivedNoteId == 202));

            var controller = new GoodsReceivedNoteFreeItemController(_fixture.MockGoodsReceivedNoteFreeItemRepository.Object);

            //Act
            var result = await controller.GetGoodsReceivedNoteFreeItems(202);

            //Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var items = okResult.Value.Should().BeAssignableTo<IEnumerable<GetGoodsReceivedNoteFreeItemDto>>().Subject;

            okResult.StatusCode.Should().Be(200);
            items.Should().HaveCount(1);
        }

        [Fact]
        public async Task GetGoodsReceivedNoteFreeItem_Returns_OkObjectResult()
        {
            //Arrange
            var id = 2;
            _fixture.MockGoodsReceivedNoteFreeItemRepository.Setup(x => x.GetGoodsReceivedNoteFreeItemAsync(It.IsAny<long>()))
                .ReturnsAsync(_fixture.GoodsReceivedNoteFreeItems.Single(d => d.Id == id));

            var controller = new GoodsReceivedNoteFreeItemController(_fixture.MockGoodsReceivedNoteFreeItemRepository.Object);

            //Act
            var result = await controller.GetGoodsReceivedNoteFreeItem(id);

            //Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var item = okResult.Value.Should().BeAssignableTo<GetGoodsReceivedNoteFreeItemDto>().Subject;

            okResult.StatusCode.Should().Be(200);
            item.Id.Should().Be(id);
            item.ItemTypeName.Should().Be("Grocery");
            item.ItemName.Should().Be("Salt");
            item.ItemUnit.Should().Be(5);
        }

        [Fact]
        public async Task CreateGoodsReceivedNoteFreeItem_Returns_CreatedAtRouteResult()
        {
            //Arrange
            _fixture.MockGoodsReceivedNoteFreeItemRepository.Setup(x => x.CreateGoodsReceivedNoteFreeItemAsync(It.IsAny<CreateGoodsReceivedNoteFreeItemDto>()))
                .ReturnsAsync(_fixture.CreateGoodsReceivedNoteFreeItemDtoResult);

            var controller = new GoodsReceivedNoteFreeItemController(_fixture.MockGoodsReceivedNoteFreeItemRepository.Object);

            //Act
            var result = await controller.CreateGoodsReceivedNoteFreeItem(_fixture.ValidCreateGoodsReceivedNoteFreeItemDto, _fixture.ApiVersion);

            //Assert
            var objectResult = result.Should().BeOfType<CreatedAtRouteResult>().Subject;
            objectResult.StatusCode.Should().Be(201);
            objectResult.RouteValues["id"].Should().Be(3);
            objectResult.RouteValues["version"].Should().Be($"{_fixture.ApiVersion}");

            var item = objectResult.Value.Should().BeAssignableTo<GetGoodsReceivedNoteFreeItemDto>().Subject;
            item.Id.Should().Be(3);
            item.ItemTypeName.Should().Be("Grocery");
            item.ItemName.Should().Be("Rice");
            item.ItemUnit.Should().Be(10);
        }

        [Fact]
        public async Task UpdateGoodsReceivedNoteFreeItem_Returns_OkObjectResult()
        {
            //Arrange
            var id = 2;
            _fixture.MockGoodsReceivedNoteFreeItemRepository.Setup(x => x.UpdateGoodsReceivedNoteFreeItemAsync(It.IsAny<long>(), It.IsAny<EditGoodsReceivedNoteFreeItemDto>()))
                .ReturnsAsync(_fixture.EditGoodsReceivedNoteFreeItemDtoResult);

            var controller = new GoodsReceivedNoteFreeItemController(_fixture.MockGoodsReceivedNoteFreeItemRepository.Object);

            //Act
            var result = await controller.UpdateGoodsReceivedNoteFreeItem(id, _fixture.ValidEditGoodsReceivedNoteFreeItemDto);

            //Assert
            var objectResult = result.Should().BeOfType<OkObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);

            var item = objectResult.Value.Should().BeAssignableTo<GetGoodsReceivedNoteFreeItemDto>().Subject;
            item.Id.Should().Be(2);
            item.ItemTypeName.Should().Be("Bevarage");
            item.ItemName.Should().Be("Coca cola");
            item.ItemUnitPrice.Should().Be(150);
        }

        [Fact]
        public async Task DeleteGoodsReceivedNoteFreeItem_Returns_NoContentResult()
        {
            //Arrange
            var id = 2;
            _fixture.MockGoodsReceivedNoteFreeItemRepository.Setup(x => x.DeleteGoodsReceivedNoteFreeItemAsync(It.IsAny<long>()));

            var controller = new GoodsReceivedNoteFreeItemController(_fixture.MockGoodsReceivedNoteFreeItemRepository.Object);

            //Act
            var result = await controller.DeleteGoodsReceivedNoteFreeItem(id);

            //Assert
            var objectResult = result.Should().BeOfType<NoContentResult>().Subject;
            objectResult.StatusCode.Should().Be(204);
        }
    }
}
