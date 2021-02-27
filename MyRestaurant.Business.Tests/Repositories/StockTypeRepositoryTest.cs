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
    public class StockTypeRepositoryTest : IClassFixture<StockTypeRepositoryFixture>
    {
        private readonly StockTypeRepositoryFixture _fixture;
        public StockTypeRepositoryTest(StockTypeRepositoryFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async void GetStockTypesAsync_Returns_GetStockTypeDtos()
        {
            //Arrange
            _fixture.MockStockTypeService.Setup(x => x.GetStockTypesAsync())
                .ReturnsAsync(_fixture.StockTypes);

            var repository = new StockTypeRepository(AutoMapperSingleton.Mapper, _fixture.MockStockTypeService.Object);

            //Act
            var result = await repository.GetStockTypesAsync();

            //Assert
            var uoms = result.Should().BeAssignableTo<IEnumerable<GetStockTypeDto>>().Subject;
            uoms.Should().HaveCount(2);
        }

        [Fact]
        public async void GetStockTypeAsync_Returns_GetStockTypeDto()
        {
            //Arrange
            var id = 1;
            _fixture.MockStockTypeService.Setup(x => x.GetStockTypeAsync(It.IsAny<Expression<Func<StockType, bool>>>()))
                .Returns<Expression<Func<StockType, bool>>>(expression => Task.FromResult(_fixture.StockTypes.AsQueryable().FirstOrDefault(expression)));

            var repository = new StockTypeRepository(AutoMapperSingleton.Mapper, _fixture.MockStockTypeService.Object);

            //Act
            var result = await repository.GetStockTypeAsync(id);

            //Assert
            result.Should().BeOfType(typeof(GetStockTypeDto));
            result.Id.Should().Be(id);
            result.Type.Should().Be("Grocery");
            result.Description.Should().Be("");
        }

        [Fact]
        public async void GetStockTypeAsync_Returns_NotFoundException()
        {
            //Arrange
            var id = 201;
            _fixture.MockStockTypeService.Setup(x => x.GetStockTypeAsync(It.IsAny<Expression<Func<StockType, bool>>>()))
                .Returns<Expression<Func<StockType, bool>>>(expression => Task.FromResult(_fixture.StockTypes.AsQueryable().FirstOrDefault(expression)));

            var repository = new StockTypeRepository(AutoMapperSingleton.Mapper, _fixture.MockStockTypeService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.GetStockTypeAsync(id));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound);
            exception.ErrorMessage.Should().Be("Stock type not found.");
            exception.ErrorType.Should().Be(HttpStatusCode.NotFound.ToString());
        }

        [Fact]
        public async void CreateStockTypeAsync_Return_New_GetStockTypeDto()
        {
            //Arrange
            _fixture.MockStockTypeService.Setup(x => x.AddStockTypeAsync(It.IsAny<StockType>()))
                .ReturnsAsync(_fixture.CreatedNewStockType);

            var repository = new StockTypeRepository(AutoMapperSingleton.Mapper, _fixture.MockStockTypeService.Object);

            //Act
            var result = await repository.CreateStockTypeAsync(_fixture.CreateStockTypeDto);

            //Assert
            result.Should().BeOfType(typeof(GetStockTypeDto));
            result.Id.Should().Be(3);
            result.Type.Should().Be(_fixture.CreateStockTypeDto.Type);
            result.Description.Should().Be(_fixture.CreateStockTypeDto.Description);
        }

        [Fact]
        public async void CreateStockTypeAsync_Returns_ConflictException()
        {
            //Arrange
            _fixture.MockStockTypeService.Setup(x => x.GetStockTypeAsync(It.IsAny<Expression<Func<StockType, bool>>>()))
                .Returns<Expression<Func<StockType, bool>>>(expression => Task.FromResult(_fixture.StockTypes.AsQueryable().FirstOrDefault(expression)));

            var repository = new StockTypeRepository(AutoMapperSingleton.Mapper, _fixture.MockStockTypeService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.CreateStockTypeAsync(new CreateStockTypeDto { Type = "Beverage", Description = "" }));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.Conflict);
            exception.ErrorMessage.Should().Be("Stock Type Beverage is already available.");
            exception.ErrorType.Should().Be(HttpStatusCode.Conflict.ToString());
        }

        [Fact]
        public async void UpdateStockTypeAsync_Return_Updated_GetStockTypeDto()
        {
            //Arrange
            var id = 2;
            _fixture.MockStockTypeService.Setup(x => x.GetStockTypeAsync(It.IsAny<Expression<Func<StockType, bool>>>()))
                .Returns<Expression<Func<StockType, bool>>>(expression => Task.FromResult(_fixture.StockTypes.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockStockTypeService.Setup(x => x.UpdateStockTypeAsync(It.IsAny<StockType>()));

            var repository = new StockTypeRepository(AutoMapperSingleton.Mapper, _fixture.MockStockTypeService.Object);

            //Act
            var result = await repository.UpdateStockTypeAsync(id, _fixture.EditStockTypeDto);

            //Assert
            result.Should().BeOfType(typeof(GetStockTypeDto));
            result.Id.Should().Be(id);
            result.Type.Should().Be(_fixture.EditStockTypeDto.Type);
            result.Description.Should().Be(_fixture.EditStockTypeDto.Description);
        }

        [Fact]
        public async void UpdateStockTypeAsync_Returns_NotFoundException()
        {
            //Arrange
            var id = 201;
            _fixture.MockStockTypeService.Setup(x => x.GetStockTypeAsync(It.IsAny<Expression<Func<StockType, bool>>>()))
                .Returns<Expression<Func<StockType, bool>>>(expression => Task.FromResult(_fixture.StockTypes.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockStockTypeService.Setup(x => x.UpdateStockTypeAsync(It.IsAny<StockType>()));

            var repository = new StockTypeRepository(AutoMapperSingleton.Mapper, _fixture.MockStockTypeService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.UpdateStockTypeAsync(id, _fixture.EditStockTypeDto));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound);
            exception.ErrorMessage.Should().Be("Stock type not found.");
            exception.ErrorType.Should().Be(HttpStatusCode.NotFound.ToString());
        }

        [Fact]
        public async void UpdateStockTypeAsync_Returns_ConflictException()
        {
            //Arrange
            var id = 2;
            _fixture.MockStockTypeService.Setup(x => x.GetStockTypeAsync(It.IsAny<Expression<Func<StockType, bool>>>()))
                .Returns<Expression<Func<StockType, bool>>>(expression => Task.FromResult(_fixture.StockTypes.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockStockTypeService.Setup(x => x.UpdateStockTypeAsync(It.IsAny<StockType>()));

            var repository = new StockTypeRepository(AutoMapperSingleton.Mapper, _fixture.MockStockTypeService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.UpdateStockTypeAsync(id, new EditStockTypeDto { Type = "Grocery", Description = "" }));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.Conflict);
            exception.ErrorMessage.Should().Be("Stock Type Grocery is already available.");
            exception.ErrorType.Should().Be(HttpStatusCode.Conflict.ToString());
        }

        [Fact]
        public async void DeleteStockTypeAsync_Return_NoResult()
        {
            //Arrange
            var id = 2;
            _fixture.MockStockTypeService.Setup(x => x.GetStockTypeAsync(It.IsAny<Expression<Func<StockType, bool>>>()))
                .Returns<Expression<Func<StockType, bool>>>(expression => Task.FromResult(_fixture.StockTypes.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockStockTypeService.Setup(x => x.DeleteStockTypeAsync(It.IsAny<StockType>()));

            var repository = new StockTypeRepository(AutoMapperSingleton.Mapper, _fixture.MockStockTypeService.Object);

            //Act
            await repository.DeleteStockTypeAsync(id);

            // Assert
            _fixture.MockStockTypeService.Verify(x => x.DeleteStockTypeAsync(It.IsAny<StockType>()), Times.Once);
        }

        [Fact]
        public async void DeleteStockTypeAsync_Returns_NotFoundException()
        {
            //Arrange
            var id = 201;
            _fixture.MockStockTypeService.Setup(x => x.GetStockTypeAsync(It.IsAny<Expression<Func<StockType, bool>>>()))
                .Returns<Expression<Func<StockType, bool>>>(expression => Task.FromResult(_fixture.StockTypes.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockStockTypeService.Setup(x => x.DeleteStockTypeAsync(It.IsAny<StockType>()));

            var repository = new StockTypeRepository(AutoMapperSingleton.Mapper, _fixture.MockStockTypeService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.DeleteStockTypeAsync(id));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound);
            exception.ErrorMessage.Should().Be("Stock type not found.");
            exception.ErrorType.Should().Be(HttpStatusCode.NotFound.ToString());
        }
    }
}
