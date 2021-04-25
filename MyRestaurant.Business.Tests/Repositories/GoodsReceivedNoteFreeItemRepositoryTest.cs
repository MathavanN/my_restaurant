using FluentAssertions;
using Moq;
using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Errors;
using MyRestaurant.Business.Repositories;
using MyRestaurant.Business.Tests.Repositories.Fixtures;
using MyRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace MyRestaurant.Business.Tests.Repositories
{
    public class GoodsReceivedNoteFreeItemRepositoryTest : IClassFixture<GoodsReceivedNoteFreeItemRepositoryFixture>
    {
        private readonly GoodsReceivedNoteFreeItemRepositoryFixture _fixture;
        public GoodsReceivedNoteFreeItemRepositoryTest(GoodsReceivedNoteFreeItemRepositoryFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async void GetGoodsReceivedNoteFreeItemsAsync_Returns_GetGoodsReceivedNoteFreeItemDtos()
        {
            //Arrange
            _fixture.MockGoodsReceivedNoteFreeItemService.Setup(x => x.GetGoodsReceivedNoteFreeItemsAsync(It.IsAny<Expression<Func<GoodsReceivedNoteFreeItem, bool>>>()))
                .Returns<Expression<Func<GoodsReceivedNoteFreeItem, bool>>>(async (expression) =>
                {
                    var orders = _fixture.GoodsReceivedNoteFreeItems.AsQueryable().Where(expression).ToList();
                    return await Task.FromResult(orders);
                });

            var repository = new GoodsReceivedNoteFreeItemRepository(AutoMapperSingleton.Mapper, _fixture.MockGoodsReceivedNoteFreeItemService.Object);

            //Act
            var result = await repository.GetGoodsReceivedNoteFreeItemsAsync(101);

            //Assert
            var items = result.Should().BeAssignableTo<IEnumerable<GetGoodsReceivedNoteFreeItemDto>>().Subject;
            items.Should().HaveCount(1);
        }

        [Fact]
        public async void GetGoodsReceivedNoteFreeItemAsync_Returns_GetGoodsReceivedNoteFreeItemDto()
        {
            //Arrange
            var id = 1;
            _fixture.MockGoodsReceivedNoteFreeItemService.Setup(x => x.GetGoodsReceivedNoteFreeItemAsync(It.IsAny<Expression<Func<GoodsReceivedNoteFreeItem, bool>>>()))
                .Returns<Expression<Func<GoodsReceivedNoteFreeItem, bool>>>(expression => Task.FromResult(_fixture.GoodsReceivedNoteFreeItems.AsQueryable().FirstOrDefault(expression)));

            var repository = new GoodsReceivedNoteFreeItemRepository(AutoMapperSingleton.Mapper, _fixture.MockGoodsReceivedNoteFreeItemService.Object);

            //Act
            var result = await repository.GetGoodsReceivedNoteFreeItemAsync(id);

            //Assert
            result.Should().BeOfType(typeof(GetGoodsReceivedNoteFreeItemDto));
            result.Id.Should().Be(id);
            result.ItemTypeName.Should().Be("Grocery");
            result.ItemName.Should().Be("Chilli Powder");
        }

        [Fact]
        public async void GetGoodsReceivedNoteFreeItemAsync_Throws_NotFoundException()
        {
            //Arrange
            var id = 201;
            _fixture.MockGoodsReceivedNoteFreeItemService.Setup(x => x.GetGoodsReceivedNoteFreeItemAsync(It.IsAny<Expression<Func<GoodsReceivedNoteFreeItem, bool>>>()))
                .Returns<Expression<Func<GoodsReceivedNoteFreeItem, bool>>>(expression => Task.FromResult(_fixture.GoodsReceivedNoteFreeItems.AsQueryable().FirstOrDefault(expression)));

            var repository = new GoodsReceivedNoteFreeItemRepository(AutoMapperSingleton.Mapper, _fixture.MockGoodsReceivedNoteFreeItemService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.GetGoodsReceivedNoteFreeItemAsync(id));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound);
            exception.ErrorMessage.Should().Be("Goods received note free item not found.");
            exception.ErrorType.Should().Be(HttpStatusCode.NotFound.ToString());
        }

        [Fact]
        public async void CreateGoodsReceivedNoteFreeItemAsync_Returns_New_GetGoodsReceivedNoteFreeItemDto()
        {
            //Arrange
            _fixture.MockGoodsReceivedNoteFreeItemService.Setup(x => x.AddGoodsReceivedNoteFreeItemAsync(It.IsAny<GoodsReceivedNoteFreeItem>()))
                .ReturnsAsync(_fixture.CreatedNewGoodsReceivedNoteFreeItem);

            var repository = new GoodsReceivedNoteFreeItemRepository(AutoMapperSingleton.Mapper, _fixture.MockGoodsReceivedNoteFreeItemService.Object);

            //Act
            var result = await repository.CreateGoodsReceivedNoteFreeItemAsync(_fixture.CreateGoodsReceivedNoteFreeItemDto);

            //Assert
            result.Should().BeOfType(typeof(GetGoodsReceivedNoteFreeItemDto));
            result.Id.Should().Be(3);
            result.ItemTypeName.Should().Be("Grocery");
            result.ItemName.Should().Be("Rice");
            result.ItemUnitPrice.Should().Be(350);
            result.Quantity.Should().Be(5);
        }

        [Fact]
        public async void CreateGoodsReceivedNoteFreeItemAsync_Throws_ConflictException()
        {
            //Arrange
            _fixture.MockGoodsReceivedNoteFreeItemService.Setup(x => x.GetGoodsReceivedNoteFreeItemAsync(It.IsAny<Expression<Func<GoodsReceivedNoteFreeItem, bool>>>()))
                .Returns<Expression<Func<GoodsReceivedNoteFreeItem, bool>>>(expression => Task.FromResult(_fixture.GoodsReceivedNoteFreeItems.AsQueryable().FirstOrDefault(expression)));

            var repository = new GoodsReceivedNoteFreeItemRepository(AutoMapperSingleton.Mapper, _fixture.MockGoodsReceivedNoteFreeItemService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.CreateGoodsReceivedNoteFreeItemAsync(new CreateGoodsReceivedNoteFreeItemDto
            {
                GoodsReceivedNoteId = 202,
                ItemId = 20050,
                ItemUnitPrice = 350,
                Quantity = 5
            }));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.Conflict);
            exception.ErrorMessage.Should().Be("Item already available for this goods received note.");
            exception.ErrorType.Should().Be(HttpStatusCode.Conflict.ToString());
        }

        [Fact]
        public async void UpdateGoodsReceivedNoteFreeItemAsync_Returns_Updated_GetGoodsReceivedNoteFreeItemDto()
        {
            //Arrange
            var id = 1;
            _fixture.MockGoodsReceivedNoteFreeItemService.Setup(x => x.GetGoodsReceivedNoteFreeItemAsync(It.IsAny<Expression<Func<GoodsReceivedNoteFreeItem, bool>>>()))
                .Returns<Expression<Func<GoodsReceivedNoteFreeItem, bool>>>(expression => Task.FromResult(_fixture.GoodsReceivedNoteFreeItems.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockGoodsReceivedNoteFreeItemService.Setup(x => x.UpdateGoodsReceivedNoteFreeItemAsync(It.IsAny<GoodsReceivedNoteFreeItem>()));

            var repository = new GoodsReceivedNoteFreeItemRepository(AutoMapperSingleton.Mapper, _fixture.MockGoodsReceivedNoteFreeItemService.Object);

            //Act
            var result = await repository.UpdateGoodsReceivedNoteFreeItemAsync(id, _fixture.EditGoodsReceivedNoteFreeItemDto);

            //Assert
            result.Should().BeOfType(typeof(GetGoodsReceivedNoteFreeItemDto));
            result.Id.Should().Be(id);
            result.ItemTypeName.Should().Be("Grocery");
            result.ItemName.Should().Be("Chilli Powder");
            result.ItemUnitPrice.Should().Be(350);
            result.Quantity.Should().Be(2);
        }

        [Fact]
        public async void UpdateGoodsReceivedNoteFreeItemAsync_Throws_NotFoundException()
        {
            //Arrange
            var id = 201;
            _fixture.MockGoodsReceivedNoteFreeItemService.Setup(x => x.GetGoodsReceivedNoteFreeItemAsync(It.IsAny<Expression<Func<GoodsReceivedNoteFreeItem, bool>>>()))
                .Returns<Expression<Func<GoodsReceivedNoteFreeItem, bool>>>(expression => Task.FromResult(_fixture.GoodsReceivedNoteFreeItems.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockGoodsReceivedNoteFreeItemService.Setup(x => x.UpdateGoodsReceivedNoteFreeItemAsync(It.IsAny<GoodsReceivedNoteFreeItem>()));

            var repository = new GoodsReceivedNoteFreeItemRepository(AutoMapperSingleton.Mapper, _fixture.MockGoodsReceivedNoteFreeItemService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.UpdateGoodsReceivedNoteFreeItemAsync(id, _fixture.EditGoodsReceivedNoteFreeItemDto));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound);
            exception.ErrorMessage.Should().Be("Goods received note free item not found.");
            exception.ErrorType.Should().Be(HttpStatusCode.NotFound.ToString());
        }

        [Fact]
        public async void DeleteGoodsReceivedNoteFreeItemAsync_Returns_NoResult()
        {
            //Arrange
            var id = 2;
            _fixture.MockGoodsReceivedNoteFreeItemService.Setup(x => x.GetGoodsReceivedNoteFreeItemAsync(It.IsAny<Expression<Func<GoodsReceivedNoteFreeItem, bool>>>()))
                .Returns<Expression<Func<GoodsReceivedNoteFreeItem, bool>>>(expression => Task.FromResult(_fixture.GoodsReceivedNoteFreeItems.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockGoodsReceivedNoteFreeItemService.Setup(x => x.DeleteGoodsReceivedNoteFreeItemAsync(It.IsAny<GoodsReceivedNoteFreeItem>()));

            var repository = new GoodsReceivedNoteFreeItemRepository(AutoMapperSingleton.Mapper, _fixture.MockGoodsReceivedNoteFreeItemService.Object);

            //Act
            await repository.DeleteGoodsReceivedNoteFreeItemAsync(id);

            // Assert
            _fixture.MockGoodsReceivedNoteFreeItemService.Verify(x => x.DeleteGoodsReceivedNoteFreeItemAsync(It.IsAny<GoodsReceivedNoteFreeItem>()), Times.Once);
        }

        [Fact]
        public async void DeleteGoodsReceivedNoteFreeItemAsync_Throws_NotFoundException()
        {
            //Arrange
            var id = 201;
            _fixture.MockGoodsReceivedNoteFreeItemService.Setup(x => x.GetGoodsReceivedNoteFreeItemAsync(It.IsAny<Expression<Func<GoodsReceivedNoteFreeItem, bool>>>()))
                .Returns<Expression<Func<GoodsReceivedNoteFreeItem, bool>>>(expression => Task.FromResult(_fixture.GoodsReceivedNoteFreeItems.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockGoodsReceivedNoteFreeItemService.Setup(x => x.DeleteGoodsReceivedNoteFreeItemAsync(It.IsAny<GoodsReceivedNoteFreeItem>()));

            var repository = new GoodsReceivedNoteFreeItemRepository(AutoMapperSingleton.Mapper, _fixture.MockGoodsReceivedNoteFreeItemService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.DeleteGoodsReceivedNoteFreeItemAsync(id));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound);
            exception.ErrorMessage.Should().Be("Goods received note free item not found.");
            exception.ErrorType.Should().Be(HttpStatusCode.NotFound.ToString());
        }
    }
}
