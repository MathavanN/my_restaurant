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
    public class TransactionTypeControllerTest : IClassFixture<TransactionTypeControllerFixture>
    {
        private readonly TransactionTypeControllerFixture _fixture;

        public TransactionTypeControllerTest(TransactionTypeControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async void GetTransactionTypes_Returns_OkObjectResult()
        {
            //Arrange
            _fixture.MockTransactionTypeRepository.Setup(x => x.GetTransactionTypesAsync())
                .ReturnsAsync(_fixture.TransactionTypes);

            var controller = new TransactionTypeController(_fixture.MockTransactionTypeRepository.Object);

            //Act
            var result = await controller.GetTransactionTypes();

            //Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var transactionTypes = okResult.Value.Should().BeAssignableTo<IEnumerable<GetTransactionTypeDto>>().Subject;

            okResult.StatusCode.Should().Be(200);
            transactionTypes.Should().HaveCount(3);
        }

        [Fact]
        public async void GetTransactionType_Returns_OkObjectResult()
        {
            //Arrange
            var id = 2;
            _fixture.MockTransactionTypeRepository.Setup(x => x.GetTransactionTypeAsync(id))
                .ReturnsAsync(_fixture.TransactionTypes.Single(d => d.Id == id));

            var controller = new TransactionTypeController(_fixture.MockTransactionTypeRepository.Object);

            //Act
            var result = await controller.GetTransactionType(id);

            //Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var transactionType = okResult.Value.Should().BeAssignableTo<GetTransactionTypeDto>().Subject;

            okResult.StatusCode.Should().Be(200);
            transactionType.Id.Should().Be(id);
            transactionType.Type.Should().Be("Extra Income");
        }

        [Fact]
        public async void CreateTransactionType_Returns_CreatedAtRouteResult()
        {
            //Arrange
            _fixture.MockTransactionTypeRepository.Setup(x => x.CreateTransactionTypeAsync(_fixture.ValidCreateTransactionTypeDto))
                .ReturnsAsync(_fixture.CreateTransactionTypeDtoResult);

            var controller = new TransactionTypeController(_fixture.MockTransactionTypeRepository.Object);

            //Act
            var result = await controller.CreateTransactionType(_fixture.ValidCreateTransactionTypeDto, _fixture.ApiVersion);

            //Assert
            var objectResult = result.Should().BeOfType<CreatedAtRouteResult>().Subject;
            objectResult.StatusCode.Should().Be(201);
            objectResult.RouteValues["id"].Should().Be(4);
            objectResult.RouteValues["version"].Should().Be($"{_fixture.ApiVersion}");

            var transactionType = objectResult.Value.Should().BeAssignableTo<GetTransactionTypeDto>().Subject;
            transactionType.Id.Should().Be(_fixture.CreateTransactionTypeDtoResult.Id);
            transactionType.Type.Should().Be(_fixture.CreateTransactionTypeDtoResult.Type);
        }

        [Fact]
        public async void UpdateTransactionType_Returns_OkObjectResult()
        {
            //Arrange
            var id = 2;
            _fixture.MockTransactionTypeRepository.Setup(x => x.UpdateTransactionTypeAsync(id, _fixture.ValidEditTransactionTypeDto))
                .ReturnsAsync(_fixture.EditTransactionTypeDtoResult);

            var controller = new TransactionTypeController(_fixture.MockTransactionTypeRepository.Object);

            //Act
            var result = await controller.UpdateTransactionType(id, _fixture.ValidEditTransactionTypeDto);

            //Assert
            var objectResult = result.Should().BeOfType<OkObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);

            var transactionType = objectResult.Value.Should().BeAssignableTo<GetTransactionTypeDto>().Subject;
            transactionType.Id.Should().Be(_fixture.EditTransactionTypeDtoResult.Id);
            transactionType.Type.Should().Be(_fixture.EditTransactionTypeDtoResult.Type);
        }

        [Fact]
        public async void DeleteTransactionType_Returns_NoContentResult()
        {
            //Arrange
            var id = 2;
            _fixture.MockTransactionTypeRepository.Setup(x => x.DeleteTransactionTypeAsync(id));

            var controller = new TransactionTypeController(_fixture.MockTransactionTypeRepository.Object);

            //Act
            var result = await controller.DeleteTransactionType(id);

            //Assert
            var objectResult = result.Should().BeOfType<NoContentResult>().Subject;
            objectResult.StatusCode.Should().Be(204);
        }
    }
}
