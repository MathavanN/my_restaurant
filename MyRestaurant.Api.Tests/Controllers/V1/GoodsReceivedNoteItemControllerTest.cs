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
    public class GoodsReceivedNoteItemControllerTest : IClassFixture<GoodsReceivedNoteItemControllerFixure>
    {
        private readonly GoodsReceivedNoteItemControllerFixure _fixture;

        public GoodsReceivedNoteItemControllerTest(GoodsReceivedNoteItemControllerFixure fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async void GetGoodsReceivedNoteItems_Returns_OkObjectResult()
        {
            //Arrange
            _fixture.MockGoodsReceivedNoteItemRepository.Setup(x => x.GetGoodsReceivedNoteItemsAsync(101))
                .ReturnsAsync(_fixture.GoodsReceivedNoteItems.Where(d => d.GoodsReceivedNoteId == 101));

            var controller = new GoodsReceivedNoteItemController(_fixture.MockGoodsReceivedNoteItemRepository.Object);

            //Act
            var result = await controller.GetGoodsReceivedNoteItems(101);

            //Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var items = okResult.Value.Should().BeAssignableTo<IEnumerable<GetGoodsReceivedNoteItemDto>>().Subject;

            okResult.StatusCode.Should().Be(200);
            items.Should().HaveCount(2);
        }

        [Fact]
        public async void GetGoodsReceivedNoteItem_Returns_OkObjectResult()
        {
            //Arrange
            var id = 2;
            _fixture.MockGoodsReceivedNoteItemRepository.Setup(x => x.GetGoodsReceivedNoteItemAsync(id))
                .ReturnsAsync(_fixture.GoodsReceivedNoteItems.Single(d => d.Id == id));

            var controller = new GoodsReceivedNoteItemController(_fixture.MockGoodsReceivedNoteItemRepository.Object);

            //Act
            var result = await controller.GetGoodsReceivedNoteItem(id);

            //Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var item = okResult.Value.Should().BeAssignableTo<GetGoodsReceivedNoteItemDto>().Subject;

            okResult.StatusCode.Should().Be(200);
            item.Id.Should().Be(id);
            item.ItemTypeName.Should().Be("Grocery");
            item.ItemName.Should().Be("Chilli Powder");
            item.ItemUnit.Should().Be(250);
        }

        [Fact]
        public async void CreateGoodsReceivedNoteItem_Returns_CreatedAtRouteResult()
        {
            //Arrange
            _fixture.MockGoodsReceivedNoteItemRepository.Setup(x => x.CreateGoodsReceivedNoteItemAsync(_fixture.ValidCreateGoodsReceivedNoteItemDto))
                .ReturnsAsync(_fixture.CreateGoodsReceivedNoteItemDtoResult);

            var controller = new GoodsReceivedNoteItemController(_fixture.MockGoodsReceivedNoteItemRepository.Object);

            //Act
            var result = await controller.CreateGoodsReceivedNoteItem(_fixture.ValidCreateGoodsReceivedNoteItemDto, _fixture.ApiVersion);

            //Assert
            var objectResult = result.Should().BeOfType<CreatedAtRouteResult>().Subject;
            objectResult.StatusCode.Should().Be(201);
            objectResult.RouteValues["id"].Should().Be(5);
            objectResult.RouteValues["version"].Should().Be($"{_fixture.ApiVersion}");

            var item = objectResult.Value.Should().BeAssignableTo<GetGoodsReceivedNoteItemDto>().Subject;
            item.Id.Should().Be(5);
            item.ItemTypeName.Should().Be("Grocery");
            item.ItemName.Should().Be("Rice");
            item.ItemUnit.Should().Be(10);
        }

        [Fact]
        public async void UpdateGoodsReceivedNoteItem_Returns_OkObjectResult()
        {
            //Arrange
            var id = 2;
            _fixture.MockGoodsReceivedNoteItemRepository.Setup(x => x.UpdateGoodsReceivedNoteItemAsync(id, _fixture.ValidEditGoodsReceivedNoteItemDto))
                .ReturnsAsync(_fixture.EditGoodsReceivedNoteItemDtoResult);

            var controller = new GoodsReceivedNoteItemController(_fixture.MockGoodsReceivedNoteItemRepository.Object);

            //Act
            var result = await controller.UpdateGoodsReceivedNoteItem(id, _fixture.ValidEditGoodsReceivedNoteItemDto);

            //Assert
            var objectResult = result.Should().BeOfType<OkObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);

            var item = objectResult.Value.Should().BeAssignableTo<GetGoodsReceivedNoteItemDto>().Subject;
            item.Id.Should().Be(2);
            item.ItemTypeName.Should().Be("Grocery");
            item.ItemName.Should().Be("Chilli Powder");
            item.Quantity.Should().Be(7);
        }

        [Fact]
        public async void DeleteGoodsReceivedNoteItem_Returns_NoContentResult()
        {
            //Arrange
            var id = 2;
            _fixture.MockGoodsReceivedNoteItemRepository.Setup(x => x.DeleteGoodsReceivedNoteItemAsync(id));

            var controller = new GoodsReceivedNoteItemController(_fixture.MockGoodsReceivedNoteItemRepository.Object);

            //Act
            var result = await controller.DeleteGoodsReceivedNoteItem(id);

            //Assert
            var objectResult = result.Should().BeOfType<NoContentResult>().Subject;
            objectResult.StatusCode.Should().Be(204);
        }
    }
}
