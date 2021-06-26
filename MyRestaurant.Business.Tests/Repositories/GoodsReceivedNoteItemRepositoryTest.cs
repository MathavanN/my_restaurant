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
    public class GoodsReceivedNoteItemRepositoryTest : IClassFixture<GoodsReceivedNoteItemRepositoryFixture>
    {
        private readonly GoodsReceivedNoteItemRepositoryFixture _fixture;
        public GoodsReceivedNoteItemRepositoryTest(GoodsReceivedNoteItemRepositoryFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GetGoodsReceivedNoteItemsAsync_Returns_GetGoodsReceivedNoteItemDtos()
        {
            //Arrange
            _fixture.MockGoodsReceivedNoteItemService.Setup(x => x.GetGoodsReceivedNoteItemsAsync(It.IsAny<Expression<Func<GoodsReceivedNoteItem, bool>>>()))
                .Returns<Expression<Func<GoodsReceivedNoteItem, bool>>>(async (expression) =>
                {
                    var orders = _fixture.GoodsReceivedNoteItems.AsQueryable().Where(expression).ToList();
                    return await Task.FromResult(orders);
                });

            var repository = new GoodsReceivedNoteItemRepository(AutoMapperSingleton.Mapper, _fixture.MockGoodsReceivedNoteItemService.Object);

            //Act
            var result = await repository.GetGoodsReceivedNoteItemsAsync(101);

            //Assert
            var items = result.Should().BeAssignableTo<IEnumerable<GetGoodsReceivedNoteItemDto>>().Subject;
            items.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetGoodsReceivedNoteItemAsync_Returns_GetGoodsReceivedNoteItemDto()
        {
            //Arrange
            var id = 1;
            _fixture.MockGoodsReceivedNoteItemService.Setup(x => x.GetGoodsReceivedNoteItemAsync(It.IsAny<Expression<Func<GoodsReceivedNoteItem, bool>>>()))
                .Returns<Expression<Func<GoodsReceivedNoteItem, bool>>>(expression => Task.FromResult(_fixture.GoodsReceivedNoteItems.AsQueryable().FirstOrDefault(expression)));

            var repository = new GoodsReceivedNoteItemRepository(AutoMapperSingleton.Mapper, _fixture.MockGoodsReceivedNoteItemService.Object);

            //Act
            var result = await repository.GetGoodsReceivedNoteItemAsync(id);

            //Assert
            result.Should().BeOfType(typeof(GetGoodsReceivedNoteItemDto));
            result.Id.Should().Be(id);
            result.ItemTypeName.Should().Be("Grocery");
            result.ItemName.Should().Be("Rice");
        }

        [Fact]
        public async Task GetGoodsReceivedNoteItemAsync_Throws_NotFoundException()
        {
            //Arrange
            var id = 201;
            _fixture.MockGoodsReceivedNoteItemService.Setup(x => x.GetGoodsReceivedNoteItemAsync(It.IsAny<Expression<Func<GoodsReceivedNoteItem, bool>>>()))
                .Returns<Expression<Func<GoodsReceivedNoteItem, bool>>>(expression => Task.FromResult(_fixture.GoodsReceivedNoteItems.AsQueryable().FirstOrDefault(expression)));

            var repository = new GoodsReceivedNoteItemRepository(AutoMapperSingleton.Mapper, _fixture.MockGoodsReceivedNoteItemService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.GetGoodsReceivedNoteItemAsync(id));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound);
            exception.ErrorMessage.Should().Be("Goods received note item not found.");
            exception.ErrorType.Should().Be(HttpStatusCode.NotFound.ToString());
        }

        [Fact]
        public async Task CreateGoodsReceivedNoteItemAsync_Returns_New_GetGoodsReceivedNoteItemDto()
        {
            //Arrange
            _fixture.MockGoodsReceivedNoteItemService.Setup(x => x.AddGoodsReceivedNoteItemAsync(It.IsAny<GoodsReceivedNoteItem>()))
                .ReturnsAsync(_fixture.CreatedNewGoodsReceivedNoteItem);

            var repository = new GoodsReceivedNoteItemRepository(AutoMapperSingleton.Mapper, _fixture.MockGoodsReceivedNoteItemService.Object);

            //Act
            var result = await repository.CreateGoodsReceivedNoteItemAsync(_fixture.CreateGoodsReceivedNoteItemDto);

            //Assert
            result.Should().BeOfType(typeof(GetGoodsReceivedNoteItemDto));
            result.Id.Should().Be(5);
            result.ItemTypeName.Should().Be("Grocery");
            result.ItemName.Should().Be("Rice");
            result.ItemUnitPrice.Should().Be(350);
            result.Quantity.Should().Be(5);
        }

        [Fact]
        public async Task CreateGoodsReceivedNoteItemAsync_Throws_ConflictException()
        {
            //Arrange
            _fixture.MockGoodsReceivedNoteItemService.Setup(x => x.GetGoodsReceivedNoteItemAsync(It.IsAny<Expression<Func<GoodsReceivedNoteItem, bool>>>()))
                .Returns<Expression<Func<GoodsReceivedNoteItem, bool>>>(expression => Task.FromResult(_fixture.GoodsReceivedNoteItems.AsQueryable().FirstOrDefault(expression)));

            var repository = new GoodsReceivedNoteItemRepository(AutoMapperSingleton.Mapper, _fixture.MockGoodsReceivedNoteItemService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.CreateGoodsReceivedNoteItemAsync(new CreateGoodsReceivedNoteItemDto
            {
                GoodsReceivedNoteId = 202,
                ItemId = 20024,
                ItemUnitPrice = 350,
                Quantity = 5
            }));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.Conflict);
            exception.ErrorMessage.Should().Be("Item already available for this goods received note.");
            exception.ErrorType.Should().Be(HttpStatusCode.Conflict.ToString());
        }

        [Fact]
        public async Task UpdateGoodsReceivedNoteItemAsync_Returns_Updated_GetGoodsReceivedNoteItemDto()
        {
            //Arrange
            var id = 1;
            _fixture.MockGoodsReceivedNoteItemService.Setup(x => x.GetGoodsReceivedNoteItemAsync(It.IsAny<Expression<Func<GoodsReceivedNoteItem, bool>>>()))
                .Returns<Expression<Func<GoodsReceivedNoteItem, bool>>>(expression => Task.FromResult(_fixture.GoodsReceivedNoteItems.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockGoodsReceivedNoteItemService.Setup(x => x.UpdateGoodsReceivedNoteItemAsync(It.IsAny<GoodsReceivedNoteItem>()));

            var repository = new GoodsReceivedNoteItemRepository(AutoMapperSingleton.Mapper, _fixture.MockGoodsReceivedNoteItemService.Object);

            //Act
            var result = await repository.UpdateGoodsReceivedNoteItemAsync(id, _fixture.EditGoodsReceivedNoteItemDto);

            //Assert
            result.Should().BeOfType(typeof(GetGoodsReceivedNoteItemDto));
            result.Id.Should().Be(id);
            result.ItemTypeName.Should().Be("Grocery");
            result.ItemName.Should().Be("Rice");
            result.ItemUnitPrice.Should().Be(650);
            result.Quantity.Should().Be(7);
        }

        [Fact]
        public async Task UpdateGoodsReceivedNoteItemAsync_Throws_NotFoundException()
        {
            //Arrange
            var id = 201;
            _fixture.MockGoodsReceivedNoteItemService.Setup(x => x.GetGoodsReceivedNoteItemAsync(It.IsAny<Expression<Func<GoodsReceivedNoteItem, bool>>>()))
                .Returns<Expression<Func<GoodsReceivedNoteItem, bool>>>(expression => Task.FromResult(_fixture.GoodsReceivedNoteItems.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockGoodsReceivedNoteItemService.Setup(x => x.UpdateGoodsReceivedNoteItemAsync(It.IsAny<GoodsReceivedNoteItem>()));

            var repository = new GoodsReceivedNoteItemRepository(AutoMapperSingleton.Mapper, _fixture.MockGoodsReceivedNoteItemService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.UpdateGoodsReceivedNoteItemAsync(id, _fixture.EditGoodsReceivedNoteItemDto));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound);
            exception.ErrorMessage.Should().Be("Goods received note item not found.");
            exception.ErrorType.Should().Be(HttpStatusCode.NotFound.ToString());
        }

        [Fact]
        public async Task UpdateGoodsReceivedNoteItemAsync_Throws_ConflictException()
        {
            //Arrange
            var id = 2;
            _fixture.MockGoodsReceivedNoteItemService.Setup(x => x.GetGoodsReceivedNoteItemAsync(It.IsAny<Expression<Func<GoodsReceivedNoteItem, bool>>>()))
                .Returns<Expression<Func<GoodsReceivedNoteItem, bool>>>(expression => Task.FromResult(_fixture.GoodsReceivedNoteItems.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockGoodsReceivedNoteItemService.Setup(x => x.UpdateGoodsReceivedNoteItemAsync(It.IsAny<GoodsReceivedNoteItem>()));

            var repository = new GoodsReceivedNoteItemRepository(AutoMapperSingleton.Mapper, _fixture.MockGoodsReceivedNoteItemService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.UpdateGoodsReceivedNoteItemAsync(id, new EditGoodsReceivedNoteItemDto
            {
                GoodsReceivedNoteId = 202,
                ItemId = 20024,
                ItemUnitPrice = 260,
                Quantity = 6
            }));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.Conflict);
            exception.ErrorMessage.Should().Be("Item already available for this goods received note.");
            exception.ErrorType.Should().Be(HttpStatusCode.Conflict.ToString());
        }

        [Fact]
        public async Task DeleteGoodsReceivedNoteItemAsync_Returns_NoResult()
        {
            //Arrange
            var id = 2;
            _fixture.MockGoodsReceivedNoteItemService.Setup(x => x.GetGoodsReceivedNoteItemAsync(It.IsAny<Expression<Func<GoodsReceivedNoteItem, bool>>>()))
                .Returns<Expression<Func<GoodsReceivedNoteItem, bool>>>(expression => Task.FromResult(_fixture.GoodsReceivedNoteItems.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockGoodsReceivedNoteItemService.Setup(x => x.DeleteGoodsReceivedNoteItemAsync(It.IsAny<GoodsReceivedNoteItem>()));

            var repository = new GoodsReceivedNoteItemRepository(AutoMapperSingleton.Mapper, _fixture.MockGoodsReceivedNoteItemService.Object);

            //Act
            await repository.DeleteGoodsReceivedNoteItemAsync(id);

            // Assert
            _fixture.MockGoodsReceivedNoteItemService.Verify(x => x.DeleteGoodsReceivedNoteItemAsync(It.IsAny<GoodsReceivedNoteItem>()), Times.Once);
        }

        [Fact]
        public async Task DeleteGoodsReceivedNoteItemAsync_Throws_NotFoundException()
        {
            //Arrange
            var id = 201;
            _fixture.MockGoodsReceivedNoteItemService.Setup(x => x.GetGoodsReceivedNoteItemAsync(It.IsAny<Expression<Func<GoodsReceivedNoteItem, bool>>>()))
                .Returns<Expression<Func<GoodsReceivedNoteItem, bool>>>(expression => Task.FromResult(_fixture.GoodsReceivedNoteItems.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockGoodsReceivedNoteItemService.Setup(x => x.DeleteGoodsReceivedNoteItemAsync(It.IsAny<GoodsReceivedNoteItem>()));

            var repository = new GoodsReceivedNoteItemRepository(AutoMapperSingleton.Mapper, _fixture.MockGoodsReceivedNoteItemService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.DeleteGoodsReceivedNoteItemAsync(id));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound);
            exception.ErrorMessage.Should().Be("Goods received note item not found.");
            exception.ErrorType.Should().Be(HttpStatusCode.NotFound.ToString());
        }
    }
}
