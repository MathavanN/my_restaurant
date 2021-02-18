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
    public class SupplierControllerTest : IClassFixture<SupplierControllerFixture>
    {
        private readonly SupplierControllerFixture _fixture;

        public SupplierControllerTest(SupplierControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async void GetSuppliers_ReturnsOkObjectResult()
        {
            //Arrange
            _fixture.MockSupplierRepository.Setup(x => x.GetSuppliersAsync(10, 0, "", "", ""))
                .ReturnsAsync(_fixture.SupplierEnvelop); 

             var controller = new SupplierController(_fixture.MockSupplierRepository.Object);

            //Act
            var result = await controller.GetSuppliers(10, 0, "", "", "");

            //Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var supplierEnvelop = okResult.Value.Should().BeAssignableTo<SupplierEnvelop>().Subject;

            okResult.StatusCode.Should().Be(200);
            supplierEnvelop.SupplierCount.Should().Be(1);
            supplierEnvelop.Suppliers.Should().HaveCount(1);
        }

        [Fact]
        public async void GetSupplier_ReturnsOkObjectResult()
        {
            //Arrange
            var id = 1;
            _fixture.MockSupplierRepository.Setup(x => x.GetSupplierAsync(id))
                .ReturnsAsync(_fixture.Suppliers.Single(d => d.Id == id));

            var controller = new SupplierController(_fixture.MockSupplierRepository.Object);

            //Act
            var result = await controller.GetSupplier(id);

            //Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var supplier = okResult.Value.Should().BeAssignableTo<GetSupplierDto>().Subject;

            okResult.StatusCode.Should().Be(200);
            supplier.Id.Should().Be(id);
            supplier.Name.Should().Be("Test Supplier Pvt Ltd");
            supplier.City.Should().Be("Bishan");
            supplier.Country.Should().Be("Singapore");
        }

        [Fact]
        public async void CreateSupplier_ReturnCreatedAtRouteResult()
        {
            //Arrange
            _fixture.MockSupplierRepository.Setup(x => x.CreateSupplierAsync(_fixture.ValidCreateSupplierDto))
                .ReturnsAsync(_fixture.CreateSupplierDtoResult);

            var controller = new SupplierController(_fixture.MockSupplierRepository.Object);

            //Act
            var result = await controller.CreateSupplier(_fixture.ValidCreateSupplierDto, _fixture.ApiVersion);

            //Assert
            var objectResult = result.Should().BeOfType<CreatedAtRouteResult>().Subject;
            objectResult.StatusCode.Should().Be(201);
            objectResult.RouteValues["id"].Should().Be(2);
            objectResult.RouteValues["version"].Should().Be($"{_fixture.ApiVersion}");

            var supplier = objectResult.Value.Should().BeAssignableTo<GetSupplierDto>().Subject;
            supplier.Id.Should().Be(2);
            supplier.Name.Should().Be("Jaffna Supplier Pvt Ltd");
            supplier.City.Should().Be("Jaffna");
            supplier.Country.Should().Be("Sri Lanka");
        }

        [Fact]
        public async void UpdateSupplier_ReturnsOkObjectResult()
        {
            //Arrange
            var id = 1;
            _fixture.MockSupplierRepository.Setup(x => x.UpdateSupplierAsync(id, _fixture.ValidEditSupplierDto))
                .ReturnsAsync(_fixture.EditSupplierDtoResult);

            var controller = new SupplierController(_fixture.MockSupplierRepository.Object);

            //Act
            var result = await controller.UpdateSupplier(id, _fixture.ValidEditSupplierDto);

            //Assert
            var objectResult = result.Should().BeOfType<OkObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);

            var supplier = objectResult.Value.Should().BeAssignableTo<GetSupplierDto>().Subject;
            supplier.Id.Should().Be(id);
            supplier.Name.Should().Be("Colombo Supplier Pvt Ltd");
            supplier.City.Should().Be("Colombo");
            supplier.Country.Should().Be("Sri Lanka");

        }

        [Fact]
        public async void DeleteSupplier_ReturnNoContentResult()
        {
            //Arrange
            var id = 1;
            _fixture.MockSupplierRepository.Setup(x => x.DeleteSupplierAsync(id));

            var controller = new SupplierController(_fixture.MockSupplierRepository.Object);

            //Act
            var result = await controller.DeleteSupplier(id);

            //Assert
            var objectResult = result.Should().BeOfType<NoContentResult>().Subject;
            objectResult.StatusCode.Should().Be(204);
        }
    }
}
