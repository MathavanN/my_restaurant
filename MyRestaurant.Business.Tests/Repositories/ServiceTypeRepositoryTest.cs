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
    public class ServiceTypeRepositoryTest : IClassFixture<ServiceTypeRepositoryFixture>
    {
        private readonly ServiceTypeRepositoryFixture _fixture;
        public ServiceTypeRepositoryTest(ServiceTypeRepositoryFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async void GetServiceTypesAsync_Returns_GetServiceTypeDtos()
        {
            //Arrange
            _fixture.MockServiceTypeService.Setup(x => x.GetServiceTypesAsync())
                .ReturnsAsync(_fixture.ServiceTypes);

            var repository = new ServiceTypeRepository(AutoMapperSingleton.Mapper, _fixture.MockServiceTypeService.Object);

            //Act
            var result = await repository.GetServiceTypesAsync();

            //Assert
            var uoms = result.Should().BeAssignableTo<IEnumerable<GetServiceTypeDto>>().Subject;
            uoms.Should().HaveCount(2);
        }

        [Fact]
        public async void GetServiceTypeAsync_Returns_GetServiceTypeDto()
        {
            //Arrange
            var id = 2;
            _fixture.MockServiceTypeService.Setup(x => x.GetServiceTypeAsync(It.IsAny<Expression<Func<ServiceType, bool>>>()))
                .Returns<Expression<Func<ServiceType, bool>>>(expression => Task.FromResult(_fixture.ServiceTypes.AsQueryable().FirstOrDefault(expression)));

            var repository = new ServiceTypeRepository(AutoMapperSingleton.Mapper, _fixture.MockServiceTypeService.Object);

            //Act
            var result = await repository.GetServiceTypeAsync(id);

            //Assert
            result.Should().BeOfType(typeof(GetServiceTypeDto));
            result.Id.Should().Be(id);
            result.Type.Should().Be("Dine In");
        }

        [Fact]
        public async void GetServiceTypeAsync_Throws_NotFoundException()
        {
            //Arrange
            var id = 201;
            _fixture.MockServiceTypeService.Setup(x => x.GetServiceTypeAsync(It.IsAny<Expression<Func<ServiceType, bool>>>()))
                .Returns<Expression<Func<ServiceType, bool>>>(expression => Task.FromResult(_fixture.ServiceTypes.AsQueryable().FirstOrDefault(expression)));

            var repository = new ServiceTypeRepository(AutoMapperSingleton.Mapper, _fixture.MockServiceTypeService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.GetServiceTypeAsync(id));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound);
            exception.ErrorMessage.Should().Be("Service type not found.");
            exception.ErrorType.Should().Be(HttpStatusCode.NotFound.ToString());
        }

        [Fact]
        public async void CreateServiceTypeAsync_Returns_New_GetServiceTypeDto()
        {
            //Arrange
            _fixture.MockServiceTypeService.Setup(x => x.AddServiceTypeAsync(It.IsAny<ServiceType>()))
                .ReturnsAsync(_fixture.CreatedNewServiceType);

            var repository = new ServiceTypeRepository(AutoMapperSingleton.Mapper, _fixture.MockServiceTypeService.Object);

            //Act
            var result = await repository.CreateServiceTypeAsync(_fixture.CreateServiceTypeDto);

            //Assert
            result.Should().BeOfType(typeof(GetServiceTypeDto));
            result.Id.Should().Be(3);
            result.Type.Should().Be(_fixture.CreateServiceTypeDto.Type);
        }

        [Fact]
        public async void CreateServiceTypeAsync_Throws_ConflictException()
        {
            //Arrange
            _fixture.MockServiceTypeService.Setup(x => x.GetServiceTypeAsync(It.IsAny<Expression<Func<ServiceType, bool>>>()))
                .Returns<Expression<Func<ServiceType, bool>>>(expression => Task.FromResult(_fixture.ServiceTypes.AsQueryable().FirstOrDefault(expression)));

            var repository = new ServiceTypeRepository(AutoMapperSingleton.Mapper, _fixture.MockServiceTypeService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.CreateServiceTypeAsync(new CreateServiceTypeDto { Type = "dine in" }));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.Conflict);
            exception.ErrorMessage.Should().Be("Service type \"dine in\" is already available.");
            exception.ErrorType.Should().Be(HttpStatusCode.Conflict.ToString());
        }

        [Fact]
        public async void UpdateServiceTypeAsync_Returns_Updated_GetServiceTypeDto()
        {
            //Arrange
            var id = 1;
            _fixture.MockServiceTypeService.Setup(x => x.GetServiceTypeAsync(It.IsAny<Expression<Func<ServiceType, bool>>>()))
                .Returns<Expression<Func<ServiceType, bool>>>(expression => Task.FromResult(_fixture.ServiceTypes.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockServiceTypeService.Setup(x => x.UpdateServiceTypeAsync(It.IsAny<ServiceType>()));

            var repository = new ServiceTypeRepository(AutoMapperSingleton.Mapper, _fixture.MockServiceTypeService.Object);

            //Act
            var result = await repository.UpdateServiceTypeAsync(id, _fixture.EditServiceTypeDto);

            //Assert
            result.Should().BeOfType(typeof(GetServiceTypeDto));
            result.Id.Should().Be(id);
            result.Type.Should().Be(_fixture.EditServiceTypeDto.Type);
        }

        [Fact]
        public async void UpdateServiceTypeAsync_Throws_NotFoundException()
        {
            //Arrange
            var id = 201;
            _fixture.MockServiceTypeService.Setup(x => x.GetServiceTypeAsync(It.IsAny<Expression<Func<ServiceType, bool>>>()))
                .Returns<Expression<Func<ServiceType, bool>>>(expression => Task.FromResult(_fixture.ServiceTypes.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockServiceTypeService.Setup(x => x.UpdateServiceTypeAsync(It.IsAny<ServiceType>()));

            var repository = new ServiceTypeRepository(AutoMapperSingleton.Mapper, _fixture.MockServiceTypeService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.UpdateServiceTypeAsync(id, _fixture.EditServiceTypeDto));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound);
            exception.ErrorMessage.Should().Be("Service type not found.");
            exception.ErrorType.Should().Be(HttpStatusCode.NotFound.ToString());
        }

        [Fact]
        public async void UpdateServiceTypeAsync_Throws_ConflictException()
        {
            //Arrange
            var id = 1;
            _fixture.MockServiceTypeService.Setup(x => x.GetServiceTypeAsync(It.IsAny<Expression<Func<ServiceType, bool>>>()))
                .Returns<Expression<Func<ServiceType, bool>>>(expression => Task.FromResult(_fixture.ServiceTypes.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockServiceTypeService.Setup(x => x.UpdateServiceTypeAsync(It.IsAny<ServiceType>()));

            var repository = new ServiceTypeRepository(AutoMapperSingleton.Mapper, _fixture.MockServiceTypeService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.UpdateServiceTypeAsync(id, new EditServiceTypeDto { Type = "Dine In" }));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.Conflict);
            exception.ErrorMessage.Should().Be("Service type \"Dine In\" is already available.");
            exception.ErrorType.Should().Be(HttpStatusCode.Conflict.ToString());
        }

        [Fact]
        public async void DeleteServiceTypeAsync_Returns_NoResult()
        {
            //Arrange
            var id = 2;
            _fixture.MockServiceTypeService.Setup(x => x.GetServiceTypeAsync(It.IsAny<Expression<Func<ServiceType, bool>>>()))
                .Returns<Expression<Func<ServiceType, bool>>>(expression => Task.FromResult(_fixture.ServiceTypes.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockServiceTypeService.Setup(x => x.DeleteServiceTypeAsync(It.IsAny<ServiceType>()));

            var repository = new ServiceTypeRepository(AutoMapperSingleton.Mapper, _fixture.MockServiceTypeService.Object);

            //Act
            await repository.DeleteServiceTypeAsync(id);

            // Assert
            _fixture.MockServiceTypeService.Verify(x => x.DeleteServiceTypeAsync(It.IsAny<ServiceType>()), Times.Once);
        }

        [Fact]
        public async void DeleteServiceTypeAsync_Throws_NotFoundException()
        {
            //Arrange
            var id = 201;
            _fixture.MockServiceTypeService.Setup(x => x.GetServiceTypeAsync(It.IsAny<Expression<Func<ServiceType, bool>>>()))
                .Returns<Expression<Func<ServiceType, bool>>>(expression => Task.FromResult(_fixture.ServiceTypes.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockServiceTypeService.Setup(x => x.DeleteServiceTypeAsync(It.IsAny<ServiceType>()));

            var repository = new ServiceTypeRepository(AutoMapperSingleton.Mapper, _fixture.MockServiceTypeService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.DeleteServiceTypeAsync(id));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound);
            exception.ErrorMessage.Should().Be("Service type not found.");
            exception.ErrorType.Should().Be(HttpStatusCode.NotFound.ToString());
        }
    }
}
