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
    public class UnitOfMeasureControllerTest : IClassFixture<UnitOfMeasureControllerFixture>
    {
        private readonly UnitOfMeasureControllerFixture _fixture;

        public UnitOfMeasureControllerTest(UnitOfMeasureControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async void GetUnitOfMeasures_Returns_OkObjectResult()
        {
            //Arrange
            _fixture.MockUnitOfMeasureRepository.Setup(x => x.GetUnitOfMeasuresAsync())
                .ReturnsAsync(_fixture.UnitOfMeasures);

            var controller = new UnitOfMeasureController(_fixture.MockUnitOfMeasureRepository.Object);

            //Act
            var result = await controller.GetUnitOfMeasures();

            //Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var uoms = okResult.Value.Should().BeAssignableTo<IEnumerable<GetUnitOfMeasureDto>>().Subject;

            okResult.StatusCode.Should().Be(200);
            uoms.Should().HaveCount(2);
        }

        [Fact]
        public async void GetUnitOfMeasure_Returns_OkObjectResult()
        {
            //Arrange
            var id = 2;
            _fixture.MockUnitOfMeasureRepository.Setup(x => x.GetUnitOfMeasureAsync(It.IsAny<int>()))
                .ReturnsAsync(_fixture.UnitOfMeasures.Single(d => d.Id == id));

            var controller = new UnitOfMeasureController(_fixture.MockUnitOfMeasureRepository.Object);

            //Act
            var result = await controller.GetUnitOfMeasure(id);

            //Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var uom = okResult.Value.Should().BeAssignableTo<GetUnitOfMeasureDto>().Subject;

            okResult.StatusCode.Should().Be(200);
            uom.Id.Should().Be(id);
            uom.Code.Should().Be("g");
            uom.Description.Should().Be("Gram");
        }

        [Fact]
        public async void CreateUnitOfMeasure_Returns_CreatedAtRouteResult()
        {
            //Arrange
            _fixture.MockUnitOfMeasureRepository.Setup(x => x.CreateUnitOfMeasureAsync(It.IsAny<CreateUnitOfMeasureDto>()))
                .ReturnsAsync(_fixture.CreateUnitOfMeasureDtoResult);

            var controller = new UnitOfMeasureController(_fixture.MockUnitOfMeasureRepository.Object);

            //Act
            var result = await controller.CreateUnitOfMeasure(_fixture.ValidCreateUnitOfMeasureDto, _fixture.ApiVersion);

            //Assert
            var objectResult = result.Should().BeOfType<CreatedAtRouteResult>().Subject;
            objectResult.StatusCode.Should().Be(201);
            objectResult.RouteValues["id"].Should().Be(3);
            objectResult.RouteValues["version"].Should().Be($"{_fixture.ApiVersion}");

            var uom = objectResult.Value.Should().BeAssignableTo<GetUnitOfMeasureDto>().Subject;
            uom.Id.Should().Be(3);
            uom.Code.Should().Be("l");
            uom.Description.Should().Be("Liter");
        }

        [Fact]
        public async void UpdateUnitOfMeasure_Returns_OkObjectResult()
        {
            //Arrange
            var id = 2;
            _fixture.MockUnitOfMeasureRepository.Setup(x => x.UpdateUnitOfMeasureAsync(It.IsAny<int>(), It.IsAny<EditUnitOfMeasureDto>()))
                .ReturnsAsync(_fixture.EditUnitOfMeasureDtoResult);

            var controller = new UnitOfMeasureController(_fixture.MockUnitOfMeasureRepository.Object);

            //Act
            var result = await controller.UpdateUnitOfMeasure(id, _fixture.ValidEditUnitOfMeasureDto);

            //Assert
            var objectResult = result.Should().BeOfType<OkObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);

            var uom = objectResult.Value.Should().BeAssignableTo<GetUnitOfMeasureDto>().Subject;
            uom.Id.Should().Be(2);
            uom.Code.Should().Be("g");
            uom.Description.Should().Be("Gram test");
        }

        [Fact]
        public async void DeleteUnitOfMeasure_Returns_NoContentResult()
        {
            //Arrange
            var id = 2;
            _fixture.MockUnitOfMeasureRepository.Setup(x => x.DeleteUnitOfMeasureAsync(It.IsAny<int>()));

            var controller = new UnitOfMeasureController(_fixture.MockUnitOfMeasureRepository.Object);

            //Act
            var result = await controller.DeleteUnitOfMeasure(id);

            //Assert
            var objectResult = result.Should().BeOfType<NoContentResult>().Subject;
            objectResult.StatusCode.Should().Be(204);
        }
    }
}
