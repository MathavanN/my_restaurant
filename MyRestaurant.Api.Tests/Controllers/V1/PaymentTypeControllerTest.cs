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
    public class PaymentTypeControllerTest : IClassFixture<PaymentTypeControllerFixture>
    {
        private readonly PaymentTypeControllerFixture _fixture;
        public PaymentTypeControllerTest(PaymentTypeControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async void GetPaymentTypes_ReturnsOkObjectResult()
        {
            //Arrange
            _fixture.MockPaymentTypeRepository.Setup(x => x.GetPaymentTypesAsync())
                .ReturnsAsync(_fixture.PaymentTypes);

            var controller = new PaymentTypeController(_fixture.MockPaymentTypeRepository.Object);

            //Act
            var result = await controller.GetPaymentTypes();

            //Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var paymentTypes = okResult.Value.Should().BeAssignableTo<IEnumerable<GetPaymentTypeDto>>().Subject;

            okResult.StatusCode.Should().Be(200);
            paymentTypes.Should().HaveCount(2);
        }

        [Fact]
        public async void GetPaymentType_ReturnsOkObjectResult()
        {
            //Arrange
            var id = 2;
            _fixture.MockPaymentTypeRepository.Setup(x => x.GetPaymentTypeAsync(id))
                .ReturnsAsync(_fixture.PaymentTypes.Single(d => d.Id == id));

            var controller = new PaymentTypeController(_fixture.MockPaymentTypeRepository.Object);

            //Act
            var result = await controller.GetPaymentType(id);

            //Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var paymentType = okResult.Value.Should().BeAssignableTo<GetPaymentTypeDto>().Subject;

            okResult.StatusCode.Should().Be(200);
            paymentType.Id.Should().Be(id);
            paymentType.Name.Should().Be("Credit");
            paymentType.CreditPeriod.Should().Be(30);
        }

        [Fact]
        public async void CreatePaymentType_ReturnCreatedAtRouteResult()
        {
            //Arrange
            _fixture.MockPaymentTypeRepository.Setup(x => x.CreatePaymentTypeAsync(_fixture.ValidCreatePaymentTypeDto))
                .ReturnsAsync(_fixture.CreatePaymentTypeDtoResult);

            var controller = new PaymentTypeController(_fixture.MockPaymentTypeRepository.Object);

            //Act
            var result = await controller.CreatePaymentType(_fixture.ValidCreatePaymentTypeDto, _fixture.ApiVersion);

            //Assert
            var objectResult = result.Should().BeOfType<CreatedAtRouteResult>().Subject;
            objectResult.StatusCode.Should().Be(201);
            objectResult.RouteValues["id"].Should().Be(3);
            objectResult.RouteValues["version"].Should().Be($"{_fixture.ApiVersion}");

            var paymentType = objectResult.Value.Should().BeAssignableTo<GetPaymentTypeDto>().Subject;
            paymentType.Id.Should().Be(3);
            paymentType.Name.Should().Be("Credit1");
            paymentType.CreditPeriod.Should().Be(15);
        }

        [Fact]
        public async void UpdatePaymentType_ReturnsOkObjectResult()
        {
            //Arrange
            var id = 2;
            _fixture.MockPaymentTypeRepository.Setup(x => x.UpdatePaymentTypeAsync(id, _fixture.ValidUpdatePaymentTypeDto));

            var controller = new PaymentTypeController(_fixture.MockPaymentTypeRepository.Object);

            //Act
            var result = await controller.UpdatePaymentType(id, _fixture.ValidUpdatePaymentTypeDto);

            //Assert
            var objectResult = result.Should().BeOfType<OkObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);

            var paymentType = objectResult.Value.Should().BeAssignableTo<GetPaymentTypeDto>().Subject;
            paymentType.Id.Should().Be(2);
            paymentType.Name.Should().Be("Credit");
            paymentType.CreditPeriod.Should().Be(20);

        }

        [Fact]
        public async void DeletePaymentTYpe_ReturnNoContentResult()
        {
            //Arrange
            var id = 2;
            _fixture.MockPaymentTypeRepository.Setup(x => x.DeletePaymentTypeAsync(id));

            var controller = new PaymentTypeController(_fixture.MockPaymentTypeRepository.Object);

            //Act
            var result = await controller.DeletePaymentType(id);

            //Assert
            var objectResult = result.Should().BeOfType<NoContentResult>().Subject;
            objectResult.StatusCode.Should().Be(204);
        }
    }
}
