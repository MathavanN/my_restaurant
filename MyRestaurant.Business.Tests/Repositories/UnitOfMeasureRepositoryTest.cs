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
    public class UnitOfMeasureRepositoryTest : IClassFixture<UnitOfMeasureRepositoryFixture>
    {
        private readonly UnitOfMeasureRepositoryFixture _fixture;
        public UnitOfMeasureRepositoryTest(UnitOfMeasureRepositoryFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async void GetUnitOfMeasuresAsync_Returns_GetUnitOfMeasureDtos()
        {
            //Arrange
            _fixture.MockUnitOfMeasureService.Setup(x => x.GetUnitOfMeasuresAsync())
                .ReturnsAsync(_fixture.UnitOfMeasures);

            var repository = new UnitOfMeasureRepository(AutoMapperSingleton.Mapper, _fixture.MockUnitOfMeasureService.Object);

            //Act
            var result = await repository.GetUnitOfMeasuresAsync();

            //Assert
            var uoms = result.Should().BeAssignableTo<IEnumerable<GetUnitOfMeasureDto>>().Subject;
            uoms.Should().HaveCount(3);
        }

        [Fact]
        public async void GetUnitOfMeasureAsync_Returns_GetUnitOfMeasureDto()
        {
            //Arrange
            var id = 1;
            _fixture.MockUnitOfMeasureService.Setup(x => x.GetUnitOfMeasureAsync(It.IsAny<Expression<Func<UnitOfMeasure, bool>>>()))
                .Returns<Expression<Func<UnitOfMeasure, bool>>>(expression => Task.FromResult(_fixture.UnitOfMeasures.AsQueryable().FirstOrDefault(expression)));

            var repository = new UnitOfMeasureRepository(AutoMapperSingleton.Mapper, _fixture.MockUnitOfMeasureService.Object);

            //Act
            var result = await repository.GetUnitOfMeasureAsync(id);

            //Assert
            result.Should().BeOfType(typeof(GetUnitOfMeasureDto));
            result.Id.Should().Be(id);
            result.Code.Should().Be("kg");
            result.Description.Should().Be("kg units");
        }

        [Fact]
        public async void GetUnitOfMeasureAsync_Returns_NotFoundException()
        {
            //Arrange
            var id = 201;
            _fixture.MockUnitOfMeasureService.Setup(x => x.GetUnitOfMeasureAsync(It.IsAny<Expression<Func<UnitOfMeasure, bool>>>()))
                .Returns<Expression<Func<UnitOfMeasure, bool>>>(expression => Task.FromResult(_fixture.UnitOfMeasures.AsQueryable().FirstOrDefault(expression)));

            var repository = new UnitOfMeasureRepository(AutoMapperSingleton.Mapper, _fixture.MockUnitOfMeasureService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.GetUnitOfMeasureAsync(id));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound);
            exception.ErrorMessage.Should().Be("Unit of measure not found.");
            exception.ErrorType.Should().Be(HttpStatusCode.NotFound.ToString());
        }

        [Fact]
        public async void CreateUnitOfMeasureAsync_Returns_New_GetUnitOfMeasureDto()
        {
            //Arrange
            _fixture.MockUnitOfMeasureService.Setup(x => x.AddUnitOfMeasureAsync(It.IsAny<UnitOfMeasure>()))
                .ReturnsAsync(_fixture.CreatedNewUnitOfMeasure);

            var repository = new UnitOfMeasureRepository(AutoMapperSingleton.Mapper, _fixture.MockUnitOfMeasureService.Object);

            //Act
            var result = await repository.CreateUnitOfMeasureAsync(_fixture.CreateUnitOfMeasureDto);

            //Assert
            result.Should().BeOfType(typeof(GetUnitOfMeasureDto));
            result.Id.Should().Be(4);
            result.Code.Should().Be(_fixture.CreateUnitOfMeasureDto.Code);
            result.Description.Should().Be(_fixture.CreateUnitOfMeasureDto.Description);
        }

        [Fact]
        public async void CreateUnitOfMeasureAsync_Throws_ConflictException()
        {
            //Arrange
            _fixture.MockUnitOfMeasureService.Setup(x => x.GetUnitOfMeasureAsync(It.IsAny<Expression<Func<UnitOfMeasure, bool>>>()))
                .Returns<Expression<Func<UnitOfMeasure, bool>>>(expression => Task.FromResult(_fixture.UnitOfMeasures.AsQueryable().FirstOrDefault(expression)));

            var repository = new UnitOfMeasureRepository(AutoMapperSingleton.Mapper, _fixture.MockUnitOfMeasureService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.CreateUnitOfMeasureAsync(new CreateUnitOfMeasureDto { Code = "g", Description = "" }));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.Conflict);
            exception.ErrorMessage.Should().Be("Unit of measure \"g\" is already available.");
            exception.ErrorType.Should().Be(HttpStatusCode.Conflict.ToString());
        }

        [Fact]
        public async void UpdateUnitOfMeasureAsync_Returns_Updated_GetUnitOfMeasureDto()
        {
            //Arrange
            var id = 3;
            _fixture.MockUnitOfMeasureService.Setup(x => x.GetUnitOfMeasureAsync(It.IsAny<Expression<Func<UnitOfMeasure, bool>>>()))
                .Returns<Expression<Func<UnitOfMeasure, bool>>>(expression => Task.FromResult(_fixture.UnitOfMeasures.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockUnitOfMeasureService.Setup(x => x.UpdateUnitOfMeasureAsync(It.IsAny<UnitOfMeasure>()));

            var repository = new UnitOfMeasureRepository(AutoMapperSingleton.Mapper, _fixture.MockUnitOfMeasureService.Object);

            //Act
            var result = await repository.UpdateUnitOfMeasureAsync(id, _fixture.EditUnitOfMeasureDto);

            //Assert
            result.Should().BeOfType(typeof(GetUnitOfMeasureDto));
            result.Id.Should().Be(id);
            result.Code.Should().Be(_fixture.EditUnitOfMeasureDto.Code);
            result.Description.Should().Be(_fixture.EditUnitOfMeasureDto.Description);
        }

        [Fact]
        public async void UpdateUnitOfMeasureAsync_Throws_NotFoundException()
        {
            //Arrange
            var id = 201;
            _fixture.MockUnitOfMeasureService.Setup(x => x.GetUnitOfMeasureAsync(It.IsAny<Expression<Func<UnitOfMeasure, bool>>>()))
                .Returns<Expression<Func<UnitOfMeasure, bool>>>(expression => Task.FromResult(_fixture.UnitOfMeasures.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockUnitOfMeasureService.Setup(x => x.UpdateUnitOfMeasureAsync(It.IsAny<UnitOfMeasure>()));

            var repository = new UnitOfMeasureRepository(AutoMapperSingleton.Mapper, _fixture.MockUnitOfMeasureService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.UpdateUnitOfMeasureAsync(id, _fixture.EditUnitOfMeasureDto));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound);
            exception.ErrorMessage.Should().Be("Unit of measure not found.");
            exception.ErrorType.Should().Be(HttpStatusCode.NotFound.ToString());
        }

        [Fact]
        public async void UpdateUnitOfMeasureAsync_Throws_ConflictException()
        {
            //Arrange
            var id = 2;
            _fixture.MockUnitOfMeasureService.Setup(x => x.GetUnitOfMeasureAsync(It.IsAny<Expression<Func<UnitOfMeasure, bool>>>()))
                .Returns<Expression<Func<UnitOfMeasure, bool>>>(expression => Task.FromResult(_fixture.UnitOfMeasures.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockUnitOfMeasureService.Setup(x => x.UpdateUnitOfMeasureAsync(It.IsAny<UnitOfMeasure>()));

            var repository = new UnitOfMeasureRepository(AutoMapperSingleton.Mapper, _fixture.MockUnitOfMeasureService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.UpdateUnitOfMeasureAsync(id, new EditUnitOfMeasureDto { Code = "ml", Description = "" }));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.Conflict);
            exception.ErrorMessage.Should().Be("Unit of measure \"ml\" is already available.");
            exception.ErrorType.Should().Be(HttpStatusCode.Conflict.ToString());
        }

        [Fact]
        public async void DeleteUnitOfMeasureAsync_Returns_NoResult()
        {
            //Arrange
            var id = 2;
            _fixture.MockUnitOfMeasureService.Setup(x => x.GetUnitOfMeasureAsync(It.IsAny<Expression<Func<UnitOfMeasure, bool>>>()))
                .Returns<Expression<Func<UnitOfMeasure, bool>>>(expression => Task.FromResult(_fixture.UnitOfMeasures.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockUnitOfMeasureService.Setup(x => x.DeleteUnitOfMeasureAsync(It.IsAny<UnitOfMeasure>()));

            var repository = new UnitOfMeasureRepository(AutoMapperSingleton.Mapper, _fixture.MockUnitOfMeasureService.Object);

            //Act
            await repository.DeleteUnitOfMeasureAsync(id);

            // Assert
            _fixture.MockUnitOfMeasureService.Verify(x => x.DeleteUnitOfMeasureAsync(It.IsAny<UnitOfMeasure>()), Times.Once);
        }

        [Fact]
        public async void DeleteUnitOfMeasureAsync_Throws_NotFoundException()
        {
            //Arrange
            var id = 201;
            _fixture.MockUnitOfMeasureService.Setup(x => x.GetUnitOfMeasureAsync(It.IsAny<Expression<Func<UnitOfMeasure, bool>>>()))
                .Returns<Expression<Func<UnitOfMeasure, bool>>>(expression => Task.FromResult(_fixture.UnitOfMeasures.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockUnitOfMeasureService.Setup(x => x.DeleteUnitOfMeasureAsync(It.IsAny<UnitOfMeasure>()));

            var repository = new UnitOfMeasureRepository(AutoMapperSingleton.Mapper, _fixture.MockUnitOfMeasureService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.DeleteUnitOfMeasureAsync(id));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound);
            exception.ErrorMessage.Should().Be("Unit of measure not found.");
            exception.ErrorType.Should().Be(HttpStatusCode.NotFound.ToString());
        }
    }
}
