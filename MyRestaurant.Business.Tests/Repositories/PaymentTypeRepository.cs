using FluentAssertions;
using Moq;
using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Repositories;
using MyRestaurant.Business.Tests.Repositories.Fixtures;
using System;
using System.Collections.Generic;
using System.Text;
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
    }
}
