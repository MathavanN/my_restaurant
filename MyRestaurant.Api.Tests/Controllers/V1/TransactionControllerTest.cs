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
    public class TransactionControllerTest : IClassFixture<TransactionControllerFixture>
    {
        private readonly TransactionControllerFixture _fixture;

        public TransactionControllerTest(TransactionControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GetTransactions_Returns_OkObjectResult()
        {
            //Arrange
            _fixture.MockTransactionRepository.Setup(x => x.GetTransactionsAsync())
                .ReturnsAsync(_fixture.Transactions);

            var controller = new TransactionController(_fixture.MockTransactionRepository.Object);

            //Act
            var result = await controller.GetTransactions();

            //Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var transactions = okResult.Value.Should().BeAssignableTo<IEnumerable<GetTransactionDto>>().Subject;

            okResult.StatusCode.Should().Be(200);
            transactions.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetTransaction_Returns_OkObjectResult()
        {
            //Arrange
            var id = 1;
            _fixture.MockTransactionRepository.Setup(x => x.GetTransactionAsync(It.IsAny<int>()))
                .ReturnsAsync(_fixture.Transactions.Single(d => d.Id == id));

            var controller = new TransactionController(_fixture.MockTransactionRepository.Object);

            //Act
            var result = await controller.GetTransaction(id);

            //Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var transaction = okResult.Value.Should().BeAssignableTo<GetTransactionDto>().Subject;

            okResult.StatusCode.Should().Be(200);
            transaction.Id.Should().Be(id);
            transaction.TransactionType.Should().Be("Food");
            transaction.PaymentType.Should().Be("Credit");
            transaction.Amount.Should().Be(6.5m);
        }

        [Fact]
        public async Task CreateTransaction_Returns_CreatedAtRouteResult()
        {
            //Arrange
            _fixture.MockTransactionRepository.Setup(x => x.CreateTransactionAsync(It.IsAny<CreateTransactionDto>()))
                .ReturnsAsync(_fixture.CreateTransactionDtoResult);

            var controller = new TransactionController(_fixture.MockTransactionRepository.Object);

            //Act
            var result = await controller.CreateTransaction(_fixture.ValidCreateTransactionDto, _fixture.ApiVersion);

            //Assert
            var objectResult = result.Should().BeOfType<CreatedAtRouteResult>().Subject;
            objectResult.StatusCode.Should().Be(201);
            objectResult.RouteValues["id"].Should().Be(3);
            objectResult.RouteValues["version"].Should().Be($"{_fixture.ApiVersion}");

            var transaction = objectResult.Value.Should().BeAssignableTo<GetTransactionDto>().Subject;
            transaction.Id.Should().Be(_fixture.CreateTransactionDtoResult.Id);
            transaction.TransactionType.Should().Be(_fixture.CreateTransactionDtoResult.TransactionType);
            transaction.PaymentType.Should().Be(_fixture.CreateTransactionDtoResult.PaymentType);
            transaction.Amount.Should().Be(_fixture.CreateTransactionDtoResult.Amount);
        }

        [Fact]
        public async Task UpdateTransaction_Returns_OkObjectResult()
        {
            //Arrange
            var id = 2;
            _fixture.MockTransactionRepository.Setup(x => x.UpdateTransactionAsync(It.IsAny<int>(), It.IsAny<EditTransactionDto>()))
                .ReturnsAsync(_fixture.EditTransactionDtoResult);

            var controller = new TransactionController(_fixture.MockTransactionRepository.Object);

            //Act
            var result = await controller.UpdateTransaction(id, _fixture.ValidEditTransactionDto);

            //Assert
            var objectResult = result.Should().BeOfType<OkObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);

            var transaction = objectResult.Value.Should().BeAssignableTo<GetTransactionDto>().Subject;
            transaction.Id.Should().Be(_fixture.EditTransactionDtoResult.Id);
            transaction.TransactionType.Should().Be(_fixture.EditTransactionDtoResult.TransactionType);
            transaction.PaymentType.Should().Be(_fixture.EditTransactionDtoResult.PaymentType);
            transaction.Amount.Should().Be(_fixture.EditTransactionDtoResult.Amount);
        }

        [Fact]
        public async Task DeleteTransaction_Returns_NoContentResult()
        {
            //Arrange
            var id = 2;
            _fixture.MockTransactionRepository.Setup(x => x.DeleteTransactionAsync(It.IsAny<int>()));

            var controller = new TransactionController(_fixture.MockTransactionRepository.Object);

            //Act
            var result = await controller.DeleteTransaction(id);

            //Assert
            var objectResult = result.Should().BeOfType<NoContentResult>().Subject;
            objectResult.StatusCode.Should().Be(204);
        }
    }
}
