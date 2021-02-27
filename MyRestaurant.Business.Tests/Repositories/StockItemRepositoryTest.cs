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
    public class StockItemRepositoryTest : IClassFixture<StockItemRepositoryFixture>
    {
        private readonly StockItemRepositoryFixture _fixture;
        public StockItemRepositoryTest(StockItemRepositoryFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async void GetStockItemsAsync_Returns_GetStockItemDtos()
        {
            //Arrange
            _fixture.MockStockItemService.Setup(x => x.GetStockItemsAsync())
                .ReturnsAsync(_fixture.StockItems);

            var repository = new StockItemRepository(AutoMapperSingleton.Mapper, _fixture.MockStockItemService.Object);

            //Act
            var result = await repository.GetStockItemsAsync();

            //Assert
            var items = result.Should().BeAssignableTo<IEnumerable<GetStockItemDto>>().Subject;
            items.Should().HaveCount(4);
        }

        [Fact]
        public async void GetStockItemsByTypeAsync_Returns_StockItemEnvelop()
        {
            //Arrange
            _fixture.MockStockItemService.Setup(x => x.GetStockItemsAsync(d => d.TypeId == 1, 0, 10))
                .ReturnsAsync(_fixture.CollectionEnvelop);

            var repository = new StockItemRepository(AutoMapperSingleton.Mapper, _fixture.MockStockItemService.Object);

            //Act
            var result = await repository.GetStockItemsByTypeAsync(1, 10, 0);

            //Assert
            var stockItemEnvelop = result.Should().BeAssignableTo<StockItemEnvelop>().Subject;
            stockItemEnvelop.StockItemCount.Should().Be(2);
            stockItemEnvelop.StockItems.Should().HaveCount(2);
            stockItemEnvelop.ItemsPerPage.Should().Be(10);
            stockItemEnvelop.TotalPages.Should().Be(1);
        }

        [Fact]
        public async void GetStockItemsByTypeAsync_With_Empty_Paged_Params_Returns_StockItemEnvelop()
        {
            //Arrange
            _fixture.MockStockItemService.Setup(x => x.GetStockItemsAsync(d => d.TypeId == 1, 0, 10))
                .ReturnsAsync(_fixture.CollectionEnvelop);

            var repository = new StockItemRepository(AutoMapperSingleton.Mapper, _fixture.MockStockItemService.Object);

            //Act
            var result = await repository.GetStockItemsByTypeAsync(1, null, null);

            //Assert
            var stockItemEnvelop = result.Should().BeAssignableTo<StockItemEnvelop>().Subject;
            stockItemEnvelop.StockItemCount.Should().Be(2);
            stockItemEnvelop.StockItems.Should().HaveCount(2);
            stockItemEnvelop.ItemsPerPage.Should().Be(10);
            stockItemEnvelop.TotalPages.Should().Be(1);
        }

        [Fact]
        public async void GetStockItemAsync_Returns_GetStockItemDto()
        {
            //Arrange
            var id = 1;
            _fixture.MockStockItemService.Setup(x => x.GetStockItemAsync(It.IsAny<Expression<Func<StockItem, bool>>>()))
                .Returns<Expression<Func<StockItem, bool>>>(expression => Task.FromResult(_fixture.StockItems.AsQueryable().FirstOrDefault(expression)));

            var repository = new StockItemRepository(AutoMapperSingleton.Mapper, _fixture.MockStockItemService.Object);

            //Act
            var result = await repository.GetStockItemAsync(id);

            //Assert
            result.Should().BeOfType(typeof(GetStockItemDto));
            result.Id.Should().Be(id);
            result.Name.Should().Be("Rice");
            result.StockType.Should().Be("Grocery");
            result.UnitOfMeasureCode.Should().Be("kg");
        }

        [Fact]
        public async void GetStockItemAsync_Returns_NotFoundException()
        {
            //Arrange
            var id = 201;
            _fixture.MockStockItemService.Setup(x => x.GetStockItemAsync(It.IsAny<Expression<Func<StockItem, bool>>>()))
                .Returns<Expression<Func<StockItem, bool>>>(expression => Task.FromResult(_fixture.StockItems.AsQueryable().FirstOrDefault(expression)));

            var repository = new StockItemRepository(AutoMapperSingleton.Mapper, _fixture.MockStockItemService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.GetStockItemAsync(id));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound);
            exception.ErrorMessage.Should().Be("Stock item not found.");
            exception.ErrorType.Should().Be(HttpStatusCode.NotFound.ToString());
        }

        [Fact]
        public async void CreateStockItemAsync_Returns_New_GetStockItemDto()
        {
            //Arrange
            _fixture.MockStockItemService.Setup(x => x.AddStockItemAsync(It.IsAny<StockItem>()))
                .ReturnsAsync(_fixture.CreatedNewStockItem);

            var repository = new StockItemRepository(AutoMapperSingleton.Mapper, _fixture.MockStockItemService.Object);

            //Act
            var result = await repository.CreateStockItemAsync(_fixture.CreateStockItemDto);

            //Assert
            result.Should().BeOfType(typeof(GetStockItemDto));
            result.Id.Should().Be(5);
            result.Name.Should().Be("Cream Soda");
            result.StockType.Should().Be("Beverage");
            result.UnitOfMeasureCode.Should().Be("ml");
        }

        [Fact]
        public async void CreateStockItemAsync_Returns_ConflictException()
        {
            //Arrange
            _fixture.MockStockItemService.Setup(x => x.GetStockItemAsync(It.IsAny<Expression<Func<StockItem, bool>>>()))
                .Returns<Expression<Func<StockItem, bool>>>(expression => Task.FromResult(_fixture.StockItems.AsQueryable().FirstOrDefault(expression)));

            var repository = new StockItemRepository(AutoMapperSingleton.Mapper, _fixture.MockStockItemService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.CreateStockItemAsync(new CreateStockItemDto { Name = "Rice", TypeId = 1, UnitOfMeasureId = 1, ItemUnit = 10 }));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.Conflict);
            exception.ErrorMessage.Should().Be("Stock Item is already available.");
            exception.ErrorType.Should().Be(HttpStatusCode.Conflict.ToString());
        }

        [Fact]
        public async void UpdateStockItemAsync_Returns_Updated_GetStockItemDto()
        {
            //Arrange
            long id = 1;
            _fixture.MockStockItemService.Setup(x => x.GetStockItemAsync(It.IsAny<Expression<Func<StockItem, bool>>>()))
                .Returns<Expression<Func<StockItem, bool>>>(expression => Task.FromResult(_fixture.StockItems.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockStockItemService.Setup(x => x.UpdateStockItemAsync(It.IsAny<StockItem>()));

            var repository = new StockItemRepository(AutoMapperSingleton.Mapper, _fixture.MockStockItemService.Object);

            //Act
            var result = await repository.UpdateStockItemAsync(id, _fixture.EditStockItemDto);

            //Assert
            result.Should().BeOfType(typeof(GetStockItemDto));
            result.Id.Should().Be(id);
            result.Name.Should().Be("Rice");
            result.StockType.Should().Be("Grocery");
            result.UnitOfMeasureCode.Should().Be("kg");
            result.ItemUnit.Should().Be(10);
            result.Description.Should().Be("10kg bag");
        }

        [Fact]
        public async void UpdateStockItemAsync_Returns_NotFoundException()
        {
            //Arrange
            var id = 201;
            _fixture.MockStockItemService.Setup(x => x.GetStockItemAsync(It.IsAny<Expression<Func<StockItem, bool>>>()))
                .Returns<Expression<Func<StockItem, bool>>>(expression => Task.FromResult(_fixture.StockItems.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockStockItemService.Setup(x => x.UpdateStockItemAsync(It.IsAny<StockItem>()));

            var repository = new StockItemRepository(AutoMapperSingleton.Mapper, _fixture.MockStockItemService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.UpdateStockItemAsync(id, _fixture.EditStockItemDto));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound);
            exception.ErrorMessage.Should().Be("Stock item not found.");
            exception.ErrorType.Should().Be(HttpStatusCode.NotFound.ToString());
        }

        [Fact]
        public async void UpdateStockItemAsync_Returns_ConflictException()
        {
            //Arrange
            var id = 1;
            _fixture.MockStockItemService.Setup(x => x.GetStockItemAsync(It.IsAny<Expression<Func<StockItem, bool>>>()))
                .Returns<Expression<Func<StockItem, bool>>>(expression => Task.FromResult(_fixture.StockItems.AsQueryable().FirstOrDefault(expression)));

            var repository = new StockItemRepository(AutoMapperSingleton.Mapper, _fixture.MockStockItemService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.UpdateStockItemAsync(id, new EditStockItemDto { Name = "Chilli Powder", TypeId = 1, ItemUnit = 250, UnitOfMeasureId = 2 }));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.Conflict);
            exception.ErrorMessage.Should().Be("Stock Item is already available.");
            exception.ErrorType.Should().Be(HttpStatusCode.Conflict.ToString());
        }

        [Fact]
        public async void DeleteStockItemAsync_Returns_NoResult()
        {
            //Arrange
            var id = 2;
            _fixture.MockStockItemService.Setup(x => x.GetStockItemAsync(It.IsAny<Expression<Func<StockItem, bool>>>()))
                .Returns<Expression<Func<StockItem, bool>>>(expression => Task.FromResult(_fixture.StockItems.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockStockItemService.Setup(x => x.DeleteStockItemAsync(It.IsAny<StockItem>()));

            var repository = new StockItemRepository(AutoMapperSingleton.Mapper, _fixture.MockStockItemService.Object);

            //Act
            await repository.DeleteStockItemAsync(id);

            // Assert
            _fixture.MockStockItemService.Verify(x => x.DeleteStockItemAsync(It.IsAny<StockItem>()), Times.Once);
        }

        [Fact]
        public async void DeleteStockItemAsync_Returns_NotFoundException()
        {
            //Arrange
            var id = 201;
            _fixture.MockStockItemService.Setup(x => x.GetStockItemAsync(It.IsAny<Expression<Func<StockItem, bool>>>()))
                .Returns<Expression<Func<StockItem, bool>>>(expression => Task.FromResult(_fixture.StockItems.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockStockItemService.Setup(x => x.DeleteStockItemAsync(It.IsAny<StockItem>()));

            var repository = new StockItemRepository(AutoMapperSingleton.Mapper, _fixture.MockStockItemService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.DeleteStockItemAsync(id));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound);
            exception.ErrorMessage.Should().Be("Stock item not found.");
            exception.ErrorType.Should().Be(HttpStatusCode.NotFound.ToString());
        }
    }
}
