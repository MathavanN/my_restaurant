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
    public class TransactionRepositoryTest : IClassFixture<TransactionRepositoryFixture>
    {
        private readonly TransactionRepositoryFixture _fixture;
        public TransactionRepositoryTest(TransactionRepositoryFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GetTransactionsAsync_Returns_GetTransactionDtos()
        {
            //Arrange
            _fixture.MockTransactionService.Setup(x => x.GetTransactionsAsync())
                .ReturnsAsync(_fixture.Transactions);

            var repository = new TransactionRepository(AutoMapperSingleton.Mapper, _fixture.MockTransactionService.Object);

            //Act
            var result = await repository.GetTransactionsAsync();

            //Assert
            var transactions = result.Should().BeAssignableTo<IEnumerable<GetTransactionDto>>().Subject;
            transactions.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetTransactionAsync_Returns_GetTransactionDto()
        {
            //Arrange
            var id = 1;
            _fixture.MockTransactionService.Setup(x => x.GetTransactionAsync(It.IsAny<Expression<Func<Transaction, bool>>>()))
                .Returns<Expression<Func<Transaction, bool>>>(expression => Task.FromResult(_fixture.Transactions.AsQueryable().FirstOrDefault(expression)));

            var repository = new TransactionRepository(AutoMapperSingleton.Mapper, _fixture.MockTransactionService.Object);

            //Act
            var result = await repository.GetTransactionAsync(id);

            //Assert
            result.Should().BeOfType(typeof(GetTransactionDto));
            result.Id.Should().Be(id);
            result.PaymentType.Should().Be("Credit");
            result.TransactionType.Should().Be("Food");
            result.Amount.Should().Be(6.5m);
        }

        [Fact]
        public async Task GetTransactionAsync_Throws_NotFoundException()
        {
            //Arrange
            var id = 201;
            _fixture.MockTransactionService.Setup(x => x.GetTransactionAsync(It.IsAny<Expression<Func<Transaction, bool>>>()))
                .Returns<Expression<Func<Transaction, bool>>>(expression => Task.FromResult(_fixture.Transactions.AsQueryable().FirstOrDefault(expression)));

            var repository = new TransactionRepository(AutoMapperSingleton.Mapper, _fixture.MockTransactionService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.GetTransactionAsync(id));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound);
            exception.ErrorMessage.Should().Be("Transaction not found.");
            exception.ErrorType.Should().Be(HttpStatusCode.NotFound.ToString());
        }

        [Fact]
        public async Task CreateTransactionAsync_Returns_New_GetTransactionDto()
        {
            //Arrange
            _fixture.MockTransactionService.Setup(x => x.AddTransactionAsync(It.IsAny<Transaction>()))
                .ReturnsAsync(_fixture.CreatedNewTransaction);

            var repository = new TransactionRepository(AutoMapperSingleton.Mapper, _fixture.MockTransactionService.Object);

            //Act
            var result = await repository.CreateTransactionAsync(_fixture.CreateTransactionDto);

            //Assert
            result.Should().BeOfType(typeof(GetTransactionDto));
            result.Id.Should().Be(3);
            result.PaymentType.Should().Be("Cash");
            result.TransactionType.Should().Be("Extra Income");
            result.Amount.Should().Be(456.5m);
        }

        [Fact]
        public async Task UpdateTransactionAsync_Returns_Updated_GetTransactionDto()
        {
            //Arrange
            var id = 2;
            _fixture.MockTransactionService.Setup(x => x.GetTransactionAsync(It.IsAny<Expression<Func<Transaction, bool>>>()))
                .Returns<Expression<Func<Transaction, bool>>>(expression => Task.FromResult(_fixture.Transactions.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockTransactionService.Setup(x => x.UpdateTransactionAsync(It.IsAny<Transaction>()));

            var repository = new TransactionRepository(AutoMapperSingleton.Mapper, _fixture.MockTransactionService.Object);

            //Act
            var result = await repository.UpdateTransactionAsync(id, _fixture.EditTransactionDto);

            //Assert
            result.Should().BeOfType(typeof(GetTransactionDto));
            result.Id.Should().Be(id);
            result.PaymentType.Should().Be("Cash");
            result.TransactionType.Should().Be("Extra Income");
            result.Amount.Should().Be(10110.5m);
        }

        [Fact]
        public async Task UpdateTransactionAsync_Throws_NotFoundException()
        {
            //Arrange
            var id = 201;
            _fixture.MockTransactionService.Setup(x => x.GetTransactionAsync(It.IsAny<Expression<Func<Transaction, bool>>>()))
                .Returns<Expression<Func<Transaction, bool>>>(expression => Task.FromResult(_fixture.Transactions.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockTransactionService.Setup(x => x.UpdateTransactionAsync(It.IsAny<Transaction>()));

            var repository = new TransactionRepository(AutoMapperSingleton.Mapper, _fixture.MockTransactionService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.UpdateTransactionAsync(id, _fixture.EditTransactionDto));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound);
            exception.ErrorMessage.Should().Be("Transaction not found.");
            exception.ErrorType.Should().Be(HttpStatusCode.NotFound.ToString());
        }

        [Fact]
        public async Task DeleteTransactionAsync_Returns_NoResult()
        {
            //Arrange
            var id = 2;
            _fixture.MockTransactionService.Setup(x => x.GetTransactionAsync(It.IsAny<Expression<Func<Transaction, bool>>>()))
                .Returns<Expression<Func<Transaction, bool>>>(expression => Task.FromResult(_fixture.Transactions.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockTransactionService.Setup(x => x.DeleteTransactionAsync(It.IsAny<Transaction>()));

            var repository = new TransactionRepository(AutoMapperSingleton.Mapper, _fixture.MockTransactionService.Object);

            //Act
            await repository.DeleteTransactionAsync(id);

            // Assert
            _fixture.MockTransactionService.Verify(x => x.DeleteTransactionAsync(It.IsAny<Transaction>()), Times.Once);
        }

        [Fact]
        public async Task DeleteTransactionAsync_Throws_NotFoundException()
        {
            //Arrange
            var id = 201;
            _fixture.MockTransactionService.Setup(x => x.GetTransactionAsync(It.IsAny<Expression<Func<Transaction, bool>>>()))
                .Returns<Expression<Func<Transaction, bool>>>(expression => Task.FromResult(_fixture.Transactions.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockTransactionService.Setup(x => x.DeleteTransactionAsync(It.IsAny<Transaction>()));

            var repository = new TransactionRepository(AutoMapperSingleton.Mapper, _fixture.MockTransactionService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.DeleteTransactionAsync(id));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound);
            exception.ErrorMessage.Should().Be("Transaction not found.");
            exception.ErrorType.Should().Be(HttpStatusCode.NotFound.ToString());
        }
    }
}
