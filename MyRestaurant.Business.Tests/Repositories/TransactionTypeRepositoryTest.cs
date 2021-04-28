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
    public class TransactionTypeRepositoryTest : IClassFixture<TransactionTypeRepositoryFixture>
    {
        private readonly TransactionTypeRepositoryFixture _fixture;
        public TransactionTypeRepositoryTest(TransactionTypeRepositoryFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async void GetTransactionTypesAsync_Returns_GetTransactionTypeDtos()
        {
            //Arrange
            _fixture.MockTransactionTypeService.Setup(x => x.GetTransactionTypesAsync())
                .ReturnsAsync(_fixture.TransactionTypes);

            var repository = new TransactionTypeRepository(AutoMapperSingleton.Mapper, _fixture.MockTransactionTypeService.Object);

            //Act
            var result = await repository.GetTransactionTypesAsync();

            //Assert
            var transactionTypes = result.Should().BeAssignableTo<IEnumerable<GetTransactionTypeDto>>().Subject;
            transactionTypes.Should().HaveCount(5);
        }

        [Fact]
        public async void GetTransactionTypeAsync_Returns_GetTransactionTypeDto()
        {
            //Arrange
            var id = 1;
            _fixture.MockTransactionTypeService.Setup(x => x.GetTransactionTypeAsync(It.IsAny<Expression<Func<TransactionType, bool>>>()))
                .Returns<Expression<Func<TransactionType, bool>>>(expression => Task.FromResult(_fixture.TransactionTypes.AsQueryable().FirstOrDefault(expression)));

            var repository = new TransactionTypeRepository(AutoMapperSingleton.Mapper, _fixture.MockTransactionTypeService.Object);

            //Act
            var result = await repository.GetTransactionTypeAsync(id);

            //Assert
            result.Should().BeOfType(typeof(GetTransactionTypeDto));
            result.Id.Should().Be(id);
            result.Type.Should().Be("Food");
        }

        [Fact]
        public async void GetTransactionTypeAsync_Throws_NotFoundException()
        {
            //Arrange
            var id = 201;
            _fixture.MockTransactionTypeService.Setup(x => x.GetTransactionTypeAsync(It.IsAny<Expression<Func<TransactionType, bool>>>()))
                .Returns<Expression<Func<TransactionType, bool>>>(expression => Task.FromResult(_fixture.TransactionTypes.AsQueryable().FirstOrDefault(expression)));

            var repository = new TransactionTypeRepository(AutoMapperSingleton.Mapper, _fixture.MockTransactionTypeService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.GetTransactionTypeAsync(id));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound);
            exception.ErrorMessage.Should().Be("Transaction type not found.");
            exception.ErrorType.Should().Be(HttpStatusCode.NotFound.ToString());
        }

        [Fact]
        public async void CreateTransactionTypeAsync_Returns_New_GetTransactionTypeDto()
        {
            //Arrange
            _fixture.MockTransactionTypeService.Setup(x => x.AddTransactionTypeAsync(It.IsAny<TransactionType>()))
                .ReturnsAsync(_fixture.CreatedNewTransactionType);

            var repository = new TransactionTypeRepository(AutoMapperSingleton.Mapper, _fixture.MockTransactionTypeService.Object);

            //Act
            var result = await repository.CreateTransactionTypeAsync(_fixture.CreateTransactionTypeDto);

            //Assert
            result.Should().BeOfType(typeof(GetTransactionTypeDto));
            result.Id.Should().Be(6);
            result.Type.Should().Be(_fixture.CreateTransactionTypeDto.Type);
        }

        [Fact]
        public async void CreateTransactionTypeAsync_Throws_ConflictException()
        {
            //Arrange
            _fixture.MockTransactionTypeService.Setup(x => x.GetTransactionTypeAsync(It.IsAny<Expression<Func<TransactionType, bool>>>()))
                .Returns<Expression<Func<TransactionType, bool>>>(expression => Task.FromResult(_fixture.TransactionTypes.AsQueryable().FirstOrDefault(expression)));

            var repository = new TransactionTypeRepository(AutoMapperSingleton.Mapper, _fixture.MockTransactionTypeService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.CreateTransactionTypeAsync(new CreateTransactionTypeDto { Type = "Food" }));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.Conflict);
            exception.ErrorMessage.Should().Be("Transaction type \"Food\" is already available.");
            exception.ErrorType.Should().Be(HttpStatusCode.Conflict.ToString());
        }

        [Fact]
        public async void UpdateTransactionTypeAsync_Returns_Updated_GetTransactionTypeDto()
        {
            //Arrange
            var id = 2;
            _fixture.MockTransactionTypeService.Setup(x => x.GetTransactionTypeAsync(It.IsAny<Expression<Func<TransactionType, bool>>>()))
                .Returns<Expression<Func<TransactionType, bool>>>(expression => Task.FromResult(_fixture.TransactionTypes.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockTransactionTypeService.Setup(x => x.UpdateTransactionTypeAsync(It.IsAny<TransactionType>()));

            var repository = new TransactionTypeRepository(AutoMapperSingleton.Mapper, _fixture.MockTransactionTypeService.Object);

            //Act
            var result = await repository.UpdateTransactionTypeAsync(id, _fixture.EditTransactionTypeDto);

            //Assert
            result.Should().BeOfType(typeof(GetTransactionTypeDto));
            result.Id.Should().Be(id);
            result.Type.Should().Be(_fixture.EditTransactionTypeDto.Type);
        }

        [Fact]
        public async void UpdateTransactionTypeAsync_Throws_NotFoundException()
        {
            //Arrange
            var id = 201;
            _fixture.MockTransactionTypeService.Setup(x => x.GetTransactionTypeAsync(It.IsAny<Expression<Func<TransactionType, bool>>>()))
                .Returns<Expression<Func<TransactionType, bool>>>(expression => Task.FromResult(_fixture.TransactionTypes.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockTransactionTypeService.Setup(x => x.UpdateTransactionTypeAsync(It.IsAny<TransactionType>()));

            var repository = new TransactionTypeRepository(AutoMapperSingleton.Mapper, _fixture.MockTransactionTypeService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.UpdateTransactionTypeAsync(id, _fixture.EditTransactionTypeDto));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound);
            exception.ErrorMessage.Should().Be("Transaction type not found.");
            exception.ErrorType.Should().Be(HttpStatusCode.NotFound.ToString());
        }

        [Fact]
        public async void UpdateTransactionTypeAsync_Throws_ConflictException()
        {
            //Arrange
            var id = 2;
            _fixture.MockTransactionTypeService.Setup(x => x.GetTransactionTypeAsync(It.IsAny<Expression<Func<TransactionType, bool>>>()))
                .Returns<Expression<Func<TransactionType, bool>>>(expression => Task.FromResult(_fixture.TransactionTypes.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockTransactionTypeService.Setup(x => x.UpdateTransactionTypeAsync(It.IsAny<TransactionType>()));

            var repository = new TransactionTypeRepository(AutoMapperSingleton.Mapper, _fixture.MockTransactionTypeService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.UpdateTransactionTypeAsync(id, new EditTransactionTypeDto { Type = "Shopping" }));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.Conflict);
            exception.ErrorMessage.Should().Be("Transaction type \"Shopping\" is already available.");
            exception.ErrorType.Should().Be(HttpStatusCode.Conflict.ToString());
        }

        [Fact]
        public async void DeleteTransactionTypeAsync_Returns_NoResult()
        {
            //Arrange
            var id = 2;
            _fixture.MockTransactionTypeService.Setup(x => x.GetTransactionTypeAsync(It.IsAny<Expression<Func<TransactionType, bool>>>()))
                .Returns<Expression<Func<TransactionType, bool>>>(expression => Task.FromResult(_fixture.TransactionTypes.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockTransactionTypeService.Setup(x => x.DeleteTransactionTypeAsync(It.IsAny<TransactionType>()));

            var repository = new TransactionTypeRepository(AutoMapperSingleton.Mapper, _fixture.MockTransactionTypeService.Object);

            //Act
            await repository.DeleteTransactionTypeAsync(id);

            // Assert
            _fixture.MockTransactionTypeService.Verify(x => x.DeleteTransactionTypeAsync(It.IsAny<TransactionType>()), Times.Once);
        }

        [Fact]
        public async void DeleteTransactionTypeAsync_Throws_NotFoundException()
        {
            //Arrange
            var id = 201;
            _fixture.MockTransactionTypeService.Setup(x => x.GetTransactionTypeAsync(It.IsAny<Expression<Func<TransactionType, bool>>>()))
                .Returns<Expression<Func<TransactionType, bool>>>(expression => Task.FromResult(_fixture.TransactionTypes.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockTransactionTypeService.Setup(x => x.DeleteTransactionTypeAsync(It.IsAny<TransactionType>()));

            var repository = new TransactionTypeRepository(AutoMapperSingleton.Mapper, _fixture.MockTransactionTypeService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.DeleteTransactionTypeAsync(id));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound);
            exception.ErrorMessage.Should().Be("Transaction type not found.");
            exception.ErrorType.Should().Be(HttpStatusCode.NotFound.ToString());
        }
    }
}
