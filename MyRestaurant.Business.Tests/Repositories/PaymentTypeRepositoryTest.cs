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
    public class PaymentTypeRepositoryTest : IClassFixture<PaymentTypeRepositoryFixture>
    {
        private readonly PaymentTypeRepositoryFixture _fixture;
        public PaymentTypeRepositoryTest(PaymentTypeRepositoryFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async void GetPaymentTypesAsync_Returns_GetPaymentTypeDtos()
        {
            //Arrange
            _fixture.MockPaymentTypeService.Setup(x => x.GetPaymentTypesAsync())
                .ReturnsAsync(_fixture.PaymentTypes);

            var repository = new PaymentTypeRepository(AutoMapperSingleton.Mapper, _fixture.MockPaymentTypeService.Object);

            //Act
            var result = await repository.GetPaymentTypesAsync();

            //Assert
            var paymentTypes = result.Should().BeAssignableTo<IEnumerable<GetPaymentTypeDto>>().Subject;
            paymentTypes.Should().HaveCount(2);
        }

        [Fact]
        public async void GetPaymentTypeAsync_Returns_GetPaymentTypeDto()
        {
            //Arrange
            var id = 1;
            _fixture.MockPaymentTypeService.Setup(x => x.GetPaymentTypeAsync(It.IsAny<Expression<Func<PaymentType, bool>>>()))
                .Returns<Expression<Func<PaymentType, bool>>>(expression => Task.FromResult(_fixture.PaymentTypes.AsQueryable().FirstOrDefault(expression)));

            var repository = new PaymentTypeRepository(AutoMapperSingleton.Mapper, _fixture.MockPaymentTypeService.Object);

            //Act
            var result = await repository.GetPaymentTypeAsync(id);

            //Assert
            result.Should().BeOfType(typeof(GetPaymentTypeDto));
            result.Id.Should().Be(id);
            result.Name.Should().Be("Cash");
            result.CreditPeriod.Should().Be(0);
        }

        [Fact]
        public async void GetPaymentTypeAsync_Returns_NotFoundException()
        {
            //Arrange
            var id = 201;
            _fixture.MockPaymentTypeService.Setup(x => x.GetPaymentTypeAsync(It.IsAny<Expression<Func<PaymentType, bool>>>()))
                .Returns<Expression<Func<PaymentType, bool>>>(expression => Task.FromResult(_fixture.PaymentTypes.AsQueryable().FirstOrDefault(expression)));

            var repository = new PaymentTypeRepository(AutoMapperSingleton.Mapper, _fixture.MockPaymentTypeService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.GetPaymentTypeAsync(id));
            
            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound);
            exception.ErrorMessage.Should().Be("Payment type not found.");
            exception.ErrorType.Should().Be(HttpStatusCode.NotFound.ToString());
        }

        [Fact]
        public async void CreatePaymentTypeAsync_Return_New_GetPaymentTypeDto()
        {
            //Arrange
            _fixture.MockPaymentTypeService.Setup(x => x.AddPaymentTypeAsync(It.IsAny<PaymentType>()))
                .ReturnsAsync(_fixture.CreatedNewPaymentType);

            var repository = new PaymentTypeRepository(AutoMapperSingleton.Mapper, _fixture.MockPaymentTypeService.Object);

            //Act
            var result = await repository.CreatePaymentTypeAsync(_fixture.CreatePaymentTypeDto);

            //Assert
            result.Should().BeOfType(typeof(GetPaymentTypeDto));
            result.Id.Should().Be(3);
            result.Name.Should().Be(_fixture.CreatePaymentTypeDto.Name);
            result.CreditPeriod.Should().Be(_fixture.CreatePaymentTypeDto.CreditPeriod);
        }

        [Fact]
        public async void CreatePaymentTypeAsync_Returns_ConflictException()
        {
            //Arrange
            _fixture.MockPaymentTypeService.Setup(x => x.GetPaymentTypeAsync(It.IsAny<Expression<Func<PaymentType, bool>>>()))
                .Returns<Expression<Func<PaymentType, bool>>>(expression => Task.FromResult(_fixture.PaymentTypes.AsQueryable().FirstOrDefault(expression)));

            var repository = new PaymentTypeRepository(AutoMapperSingleton.Mapper, _fixture.MockPaymentTypeService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.CreatePaymentTypeAsync(new CreatePaymentTypeDto { Name = "Credit", CreditPeriod = 10 }));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.Conflict);
            exception.ErrorMessage.Should().Be("Payment type Credit is already available.");
            exception.ErrorType.Should().Be(HttpStatusCode.Conflict.ToString());
        }

        [Fact]
        public async void UpdatePaymentTypeAsync_Return_Updated_GetPaymentTypeDto()
        {
            //Arrange
            var id = 2;
            _fixture.MockPaymentTypeService.Setup(x => x.GetPaymentTypeAsync(It.IsAny<Expression<Func<PaymentType, bool>>>()))
                .Returns<Expression<Func<PaymentType, bool>>>(expression => Task.FromResult(_fixture.PaymentTypes.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockPaymentTypeService.Setup(x => x.UpdatePaymentTypeAsync(It.IsAny<PaymentType>()));

            var repository = new PaymentTypeRepository(AutoMapperSingleton.Mapper, _fixture.MockPaymentTypeService.Object);

            //Act
            var result = await repository.UpdatePaymentTypeAsync(id, _fixture.EditPaymentTypeDto);

            //Assert
            result.Should().BeOfType(typeof(GetPaymentTypeDto));
            result.Id.Should().Be(id);
            result.Name.Should().Be(_fixture.EditPaymentTypeDto.Name);
            result.CreditPeriod.Should().Be(_fixture.EditPaymentTypeDto.CreditPeriod);
        }

        [Fact]
        public async void UpdatePaymentTypeAsync_Returns_NotFoundException()
        {
            //Arrange
            var id = 201;
            _fixture.MockPaymentTypeService.Setup(x => x.GetPaymentTypeAsync(It.IsAny<Expression<Func<PaymentType, bool>>>()))
                .Returns<Expression<Func<PaymentType, bool>>>(expression => Task.FromResult(_fixture.PaymentTypes.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockPaymentTypeService.Setup(x => x.UpdatePaymentTypeAsync(It.IsAny<PaymentType>()));

            var repository = new PaymentTypeRepository(AutoMapperSingleton.Mapper, _fixture.MockPaymentTypeService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.UpdatePaymentTypeAsync(id, _fixture.EditPaymentTypeDto));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound);
            exception.ErrorMessage.Should().Be("Payment type not found.");
            exception.ErrorType.Should().Be(HttpStatusCode.NotFound.ToString());
        }

        [Fact]
        public async void UpdatePaymentTypeAsync_Returns_ConflictException()
        {
            //Arrange
            var id = 2;
            _fixture.MockPaymentTypeService.Setup(x => x.GetPaymentTypeAsync(It.IsAny<Expression<Func<PaymentType, bool>>>()))
                .Returns<Expression<Func<PaymentType, bool>>>(expression => Task.FromResult(_fixture.PaymentTypes.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockPaymentTypeService.Setup(x => x.UpdatePaymentTypeAsync(It.IsAny<PaymentType>()));

            var repository = new PaymentTypeRepository(AutoMapperSingleton.Mapper, _fixture.MockPaymentTypeService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.UpdatePaymentTypeAsync(id, new EditPaymentTypeDto { Name = "Cash", CreditPeriod = 0 }));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.Conflict);
            exception.ErrorMessage.Should().Be("Payment type Cash is already available.");
            exception.ErrorType.Should().Be(HttpStatusCode.Conflict.ToString());
        }

        [Fact]
        public async void DeletePaymentTypeAsync_Return_NoResult()
        {
            //Arrange
            var id = 2;
            _fixture.MockPaymentTypeService.Setup(x => x.GetPaymentTypeAsync(It.IsAny<Expression<Func<PaymentType, bool>>>()))
                .Returns<Expression<Func<PaymentType, bool>>>(expression => Task.FromResult(_fixture.PaymentTypes.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockPaymentTypeService.Setup(x => x.DeletePaymentTypeAsync(It.IsAny<PaymentType>()));

            var repository = new PaymentTypeRepository(AutoMapperSingleton.Mapper, _fixture.MockPaymentTypeService.Object);

            //Act
            await repository.DeletePaymentTypeAsync(id);

            // Assert
            _fixture.MockPaymentTypeService.Verify(x => x.DeletePaymentTypeAsync(It.IsAny<PaymentType>()), Times.Once);
        }

        [Fact]
        public async void DeletePaymentTypeAsync_Returns_NotFoundException()
        {
            //Arrange
            var id = 201;
            _fixture.MockPaymentTypeService.Setup(x => x.GetPaymentTypeAsync(It.IsAny<Expression<Func<PaymentType, bool>>>()))
                .Returns<Expression<Func<PaymentType, bool>>>(expression => Task.FromResult(_fixture.PaymentTypes.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockPaymentTypeService.Setup(x => x.DeletePaymentTypeAsync(It.IsAny<PaymentType>()));

            var repository = new PaymentTypeRepository(AutoMapperSingleton.Mapper, _fixture.MockPaymentTypeService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.DeletePaymentTypeAsync(id));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound);
            exception.ErrorMessage.Should().Be("Payment type not found.");
            exception.ErrorType.Should().Be(HttpStatusCode.NotFound.ToString());
        }
    }
}
