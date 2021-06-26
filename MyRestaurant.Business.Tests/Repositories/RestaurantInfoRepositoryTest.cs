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
    public class RestaurantInfoRepositoryTest : IClassFixture<RestaurantInfoRepositoryFixture>
    {
        private readonly RestaurantInfoRepositoryFixture _fixture;
        public RestaurantInfoRepositoryTest(RestaurantInfoRepositoryFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GetRestaurantInfosAsync_Returns_GetRestaurantInfoDtos()
        {
            //Arrange
            _fixture.MockRestaurantInfoService.Setup(x => x.GetRestaurantInfosAsync())
                .ReturnsAsync(_fixture.RestaurantInfos);

            var repository = new RestaurantInfoRepository(AutoMapperSingleton.Mapper, _fixture.MockRestaurantInfoService.Object);

            //Act
            var result = await repository.GetRestaurantInfosAsync();

            //Assert
            var information = result.Should().BeAssignableTo<IEnumerable<GetRestaurantInfoDto>>().Subject;
            information.Should().HaveCount(1);
        }

        [Fact]
        public async Task GetRestaurantInfoAsync_Returns_GetRestaurantInfoDto()
        {
            //Arrange
            var id = 1;
            _fixture.MockRestaurantInfoService.Setup(x => x.GetRestaurantInfoAsync(It.IsAny<Expression<Func<RestaurantInfo, bool>>>()))
                .Returns<Expression<Func<RestaurantInfo, bool>>>(expression => Task.FromResult(_fixture.RestaurantInfos.AsQueryable().FirstOrDefault(expression)));

            var repository = new RestaurantInfoRepository(AutoMapperSingleton.Mapper, _fixture.MockRestaurantInfoService.Object);

            //Act
            var result = await repository.GetRestaurantInfoAsync(id);

            //Assert
            result.Should().BeOfType(typeof(GetRestaurantInfoDto));
            result.Id.Should().Be(id);
            result.Name.Should().Be("Golden Dining");
            result.Address.Should().Be("Kandy Road, Kaithady, Jaffna, Sri Lanka");
            result.LandLine.Should().Be("+9423454544");
            result.Mobile.Should().Be("+94567876786");
            result.Email.Should().Be("test@gmail.com");
        }

        [Fact]
        public async Task GetRestaurantInfoAsync_Throws_NotFoundException()
        {
            //Arrange
            var id = 201;
            _fixture.MockRestaurantInfoService.Setup(x => x.GetRestaurantInfoAsync(It.IsAny<Expression<Func<RestaurantInfo, bool>>>()))
                .Returns<Expression<Func<RestaurantInfo, bool>>>(expression => Task.FromResult(_fixture.RestaurantInfos.AsQueryable().FirstOrDefault(expression)));

            var repository = new RestaurantInfoRepository(AutoMapperSingleton.Mapper, _fixture.MockRestaurantInfoService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.GetRestaurantInfoAsync(id));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound);
            exception.ErrorMessage.Should().Be("Restaurant information not found.");
            exception.ErrorType.Should().Be(HttpStatusCode.NotFound.ToString());
        }

        [Fact]
        public async Task CreateRestaurantInfoAsync_Returns_New_GetRestaurantInfoDto()
        {
            //Arrange
            _fixture.MockRestaurantInfoService.Setup(x => x.AddRestaurantInfoAsync(It.IsAny<RestaurantInfo>()))
                .ReturnsAsync(_fixture.RestaurantInfos.FirstOrDefault(d => d.Id == 1));

            var repository = new RestaurantInfoRepository(AutoMapperSingleton.Mapper, _fixture.MockRestaurantInfoService.Object);

            //Act
            var result = await repository.CreateRestaurantInfoAsync(_fixture.CreateRestaurantInfoDto);

            //Assert
            result.Should().BeOfType(typeof(GetRestaurantInfoDto));
            result.Id.Should().Be(1);
            result.Name.Should().Be("Golden Dining");
            result.Address.Should().Be("Kandy Road, Kaithady, Jaffna, Sri Lanka");
            result.LandLine.Should().Be("+9423454544");
            result.Mobile.Should().Be("+94567876786");
            result.Email.Should().Be("test@gmail.com");
        }
    }
}
