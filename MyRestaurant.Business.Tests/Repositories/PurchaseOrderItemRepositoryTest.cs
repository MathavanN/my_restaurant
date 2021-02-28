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
    public class PurchaseOrderItemRepositoryTest : IClassFixture<PurchaseOrderItemRepositoryFixture>
    {
        private readonly PurchaseOrderItemRepositoryFixture _fixture;
        public PurchaseOrderItemRepositoryTest(PurchaseOrderItemRepositoryFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async void GetPurchaseOrderItemsAsync_Returns_GetPurchaseOrderItemDtos()
        {
            //Arrange
            _fixture.MockPurchaseOrderItemService.Setup(x => x.GetPurchaseOrderItemsAsync(It.IsAny<Expression<Func<PurchaseOrderItem, bool>>>()))
                .Returns<Expression<Func<PurchaseOrderItem, bool>>>(async (expression) =>
                {
                    var orders = _fixture.PurchaseOrderItems.AsQueryable().Where(expression).ToList();
                    return await Task.FromResult(orders);
                });

            var repository = new PurchaseOrderItemRepository(AutoMapperSingleton.Mapper, _fixture.MockPurchaseOrderItemService.Object);

            //Act
            var result = await repository.GetPurchaseOrderItemsAsync(101);

            //Assert
            var orders = result.Should().BeAssignableTo<IEnumerable<GetPurchaseOrderItemDto>>().Subject;
            orders.Should().HaveCount(2);
        }

        [Fact]
        public async void GetPurchaseOrderItemAsync_Returns_GetPurchaseOrderItemDto()
        {
            //Arrange
            var id = 1;
            _fixture.MockPurchaseOrderItemService.Setup(x => x.GetPurchaseOrderItemAsync(It.IsAny<Expression<Func<PurchaseOrderItem, bool>>>()))
                .Returns<Expression<Func<PurchaseOrderItem, bool>>>(expression => Task.FromResult(_fixture.PurchaseOrderItems.AsQueryable().FirstOrDefault(expression)));

            var repository = new PurchaseOrderItemRepository(AutoMapperSingleton.Mapper, _fixture.MockPurchaseOrderItemService.Object);

            //Act
            var result = await repository.GetPurchaseOrderItemAsync(id);

            //Assert
            result.Should().BeOfType(typeof(GetPurchaseOrderItemDto));
            result.Id.Should().Be(id);
            result.ItemTypeName.Should().Be("Grocery");
            result.ItemName.Should().Be("Rice");
        }

        [Fact]
        public async void GetPurchaseOrderItemAsync_Throws_NotFoundException()
        {
            //Arrange
            var id = 201;
            _fixture.MockPurchaseOrderItemService.Setup(x => x.GetPurchaseOrderItemAsync(It.IsAny<Expression<Func<PurchaseOrderItem, bool>>>()))
                .Returns<Expression<Func<PurchaseOrderItem, bool>>>(expression => Task.FromResult(_fixture.PurchaseOrderItems.AsQueryable().FirstOrDefault(expression)));

            var repository = new PurchaseOrderItemRepository(AutoMapperSingleton.Mapper, _fixture.MockPurchaseOrderItemService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.GetPurchaseOrderItemAsync(id));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound);
            exception.ErrorMessage.Should().Be("Purchase order item not found.");
            exception.ErrorType.Should().Be(HttpStatusCode.NotFound.ToString());
        }

        [Fact]
        public async void CreatePurchaseOrderItemAsync_Returns_New_GetPurchaseOrderItemDto()
        {
            //Arrange
            _fixture.MockPurchaseOrderItemService.Setup(x => x.AddPurchaseOrderItemAsync(It.IsAny<PurchaseOrderItem>()))
                .ReturnsAsync(_fixture.CreatedNewPurchaseOrderItem);

            var repository = new PurchaseOrderItemRepository(AutoMapperSingleton.Mapper, _fixture.MockPurchaseOrderItemService.Object);

            //Act
            var result = await repository.CreatePurchaseOrderItemAsync(_fixture.CreatePurchaseOrderItemDto);

            //Assert
            result.Should().BeOfType(typeof(GetPurchaseOrderItemDto));
            result.Id.Should().Be(5);
            result.ItemTypeName.Should().Be("Grocery");
            result.ItemName.Should().Be("Rice");
            result.ItemUnitPrice.Should().Be(350);
            result.Quantity.Should().Be(5);
        }

        [Fact]
        public async void CreatePurchaseOrderItemAsync_Throws_ConflictException()
        {
            //Arrange
            _fixture.MockPurchaseOrderItemService.Setup(x => x.GetPurchaseOrderItemAsync(It.IsAny<Expression<Func<PurchaseOrderItem, bool>>>()))
                .Returns<Expression<Func<PurchaseOrderItem, bool>>>(expression => Task.FromResult(_fixture.PurchaseOrderItems.AsQueryable().FirstOrDefault(expression)));

            var repository = new PurchaseOrderItemRepository(AutoMapperSingleton.Mapper, _fixture.MockPurchaseOrderItemService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.CreatePurchaseOrderItemAsync(new CreatePurchaseOrderItemDto {
                PurchaseOrderId = 202,
                ItemId = 20024,
                ItemUnitPrice = 350,
                Quantity = 5
            }));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.Conflict);
            exception.ErrorMessage.Should().Be("Item already available for this purchase request.");
            exception.ErrorType.Should().Be(HttpStatusCode.Conflict.ToString());
        }

        [Fact]
        public async void UpdatePurchaseOrderItemAsync_Returns_Updated_GetPurchaseOrderItemDto()
        {
            //Arrange
            var id = 1;
            _fixture.MockPurchaseOrderItemService.Setup(x => x.GetPurchaseOrderItemAsync(It.IsAny<Expression<Func<PurchaseOrderItem, bool>>>()))
                .Returns<Expression<Func<PurchaseOrderItem, bool>>>(expression => Task.FromResult(_fixture.PurchaseOrderItems.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockPurchaseOrderItemService.Setup(x => x.UpdatePurchaseOrderItemAsync(It.IsAny<PurchaseOrderItem>()));

            var repository = new PurchaseOrderItemRepository(AutoMapperSingleton.Mapper, _fixture.MockPurchaseOrderItemService.Object);

            //Act
            var result = await repository.UpdatePurchaseOrderItemAsync(id, _fixture.EditPurchaseOrderItemDto);

            //Assert
            result.Should().BeOfType(typeof(GetPurchaseOrderItemDto));
            result.Id.Should().Be(id);
            result.ItemTypeName.Should().Be("Grocery");
            result.ItemName.Should().Be("Rice");
            result.ItemUnitPrice.Should().Be(650);
            result.Quantity.Should().Be(7);
        }

        [Fact]
        public async void UpdatePurchaseOrderItemAsync_Throws_NotFoundException()
        {
            //Arrange
            var id = 201;
            _fixture.MockPurchaseOrderItemService.Setup(x => x.GetPurchaseOrderItemAsync(It.IsAny<Expression<Func<PurchaseOrderItem, bool>>>()))
                .Returns<Expression<Func<PurchaseOrderItem, bool>>>(expression => Task.FromResult(_fixture.PurchaseOrderItems.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockPurchaseOrderItemService.Setup(x => x.UpdatePurchaseOrderItemAsync(It.IsAny<PurchaseOrderItem>()));

            var repository = new PurchaseOrderItemRepository(AutoMapperSingleton.Mapper, _fixture.MockPurchaseOrderItemService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.UpdatePurchaseOrderItemAsync(id, _fixture.EditPurchaseOrderItemDto));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound);
            exception.ErrorMessage.Should().Be("Purchase order item not found.");
            exception.ErrorType.Should().Be(HttpStatusCode.NotFound.ToString());
        }

        [Fact]
        public async void UpdatePurchaseOrderItemAsync_Throws_ConflictException()
        {
            //Arrange
            var id = 2;
            _fixture.MockPurchaseOrderItemService.Setup(x => x.GetPurchaseOrderItemAsync(It.IsAny<Expression<Func<PurchaseOrderItem, bool>>>()))
                .Returns<Expression<Func<PurchaseOrderItem, bool>>>(expression => Task.FromResult(_fixture.PurchaseOrderItems.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockPurchaseOrderItemService.Setup(x => x.UpdatePurchaseOrderItemAsync(It.IsAny<PurchaseOrderItem>()));

            var repository = new PurchaseOrderItemRepository(AutoMapperSingleton.Mapper, _fixture.MockPurchaseOrderItemService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.UpdatePurchaseOrderItemAsync(id, new EditPurchaseOrderItemDto {
                PurchaseOrderId = 202,
                ItemId = 20024,
                ItemUnitPrice = 260,
                Quantity = 6
            }));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.Conflict);
            exception.ErrorMessage.Should().Be("Item already available for this purchase request.");
            exception.ErrorType.Should().Be(HttpStatusCode.Conflict.ToString());
        }

        [Fact]
        public async void DeletePurchaseOrderItemAsync_Returns_NoResult()
        {
            //Arrange
            var id = 2;
            _fixture.MockPurchaseOrderItemService.Setup(x => x.GetPurchaseOrderItemAsync(It.IsAny<Expression<Func<PurchaseOrderItem, bool>>>()))
                .Returns<Expression<Func<PurchaseOrderItem, bool>>>(expression => Task.FromResult(_fixture.PurchaseOrderItems.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockPurchaseOrderItemService.Setup(x => x.DeletePurchaseOrderItemAsync(It.IsAny<PurchaseOrderItem>()));

            var repository = new PurchaseOrderItemRepository(AutoMapperSingleton.Mapper, _fixture.MockPurchaseOrderItemService.Object);

            //Act
            await repository.DeletePurchaseOrderItemAsync(id);

            // Assert
            _fixture.MockPurchaseOrderItemService.Verify(x => x.DeletePurchaseOrderItemAsync(It.IsAny<PurchaseOrderItem>()), Times.Once);
        }

        [Fact]
        public async void DeletePurchaseOrderItemAsync_Throws_NotFoundException()
        {
            //Arrange
            var id = 201;
            _fixture.MockPurchaseOrderItemService.Setup(x => x.GetPurchaseOrderItemAsync(It.IsAny<Expression<Func<PurchaseOrderItem, bool>>>()))
                .Returns<Expression<Func<PurchaseOrderItem, bool>>>(expression => Task.FromResult(_fixture.PurchaseOrderItems.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockPurchaseOrderItemService.Setup(x => x.DeletePurchaseOrderItemAsync(It.IsAny<PurchaseOrderItem>()));

            var repository = new PurchaseOrderItemRepository(AutoMapperSingleton.Mapper, _fixture.MockPurchaseOrderItemService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.DeletePurchaseOrderItemAsync(id));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound);
            exception.ErrorMessage.Should().Be("Purchase order item not found.");
            exception.ErrorType.Should().Be(HttpStatusCode.NotFound.ToString());
        }
    }
}
