using FluentAssertions;
using Microsoft.EntityFrameworkCore;
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
    public class AccountRepositoryTest : IClassFixture<AccountRepositoryFixture>
    {
        private readonly AccountRepositoryFixture _fixture;
        public AccountRepositoryTest(AccountRepositoryFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void GetCurrentUser_Returns_CurrentUserDto()
        {
            //Arrange
            _fixture.MockUserAccessorService.Setup(x => x.GetCurrentUser())
                .Returns(_fixture.CurrentUser);

            var repository = new AccountRepository(AutoMapperSingleton.Mapper, _fixture.MockUserManager.Object,
                _fixture.MockJwtTokenService.Object, _fixture.MockUserAccessorService.Object);

            //Act
            var result = repository.GetCurrentUser();

            //Assert
            var currentUser = result.Should().BeAssignableTo<CurrentUserDto>().Subject;
            currentUser.LastName.Should().Be("Dining");
            currentUser.FirstName.Should().Be("Golden");
            currentUser.UserId.Should().Be(Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa3916b9"));
            currentUser.Email.Should().Be("abc@gmail.com");
        }

        //[Fact]
        //public async void GetUsersAsync_Returns_GetUserDtos()
        //{
        //    //Arrange
        //    _fixture.MockUserManager.Setup(x => x.Users)
        //        .Returns(_fixture.Users.AsQueryable());

        //    var repository = new AccountRepository(AutoMapperSingleton.Mapper, _fixture.MockUserManager.Object,
        //        _fixture.MockJwtTokenService.Object, _fixture.MockUserAccessorService.Object);

        //    //Act
        //    var result = await repository.GetUsersAsync();

        //    //Assert
        //    var users = result.Should().BeAssignableTo<IEnumerable<GetUserDto>>().Subject;
        //    users.Should().HaveCount(4);
        //}

        [Fact]
        public async void RevokeToken_Returns_NoContent()
        {
            //Arrange
            _fixture.MockJwtTokenService.Setup(x => x.GetRefreshTokenAsync(It.IsAny<Expression<Func<RefreshToken, bool>>>()))
                .Returns<Expression<Func<RefreshToken, bool>>>(expression => Task.FromResult(_fixture.RefreshTokens.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockJwtTokenService.Setup(x => x.UpdateRefreshTokenAsync(It.IsAny<RefreshToken>()));

            var repository = new AccountRepository(AutoMapperSingleton.Mapper, _fixture.MockUserManager.Object,
                _fixture.MockJwtTokenService.Object, _fixture.MockUserAccessorService.Object);

            //Act
            await repository.RevokeToken(new RevokeDto { RefreshToken = "token1" }, "0.0.0.1");

            //Assert
            _fixture.MockJwtTokenService.Verify(x => x.UpdateRefreshTokenAsync(It.IsAny<RefreshToken>()), Times.Once);
        }

        [Fact]
        public async void RevokeToken_Throws_NotFoundException()
        {
            //Arrange
            _fixture.MockJwtTokenService.Setup(x => x.GetRefreshTokenAsync(It.IsAny<Expression<Func<RefreshToken, bool>>>()))
                .Returns<Expression<Func<RefreshToken, bool>>>(expression => Task.FromResult(_fixture.RefreshTokens.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockJwtTokenService.Setup(x => x.UpdateRefreshTokenAsync(It.IsAny<RefreshToken>()));

            var repository = new AccountRepository(AutoMapperSingleton.Mapper, _fixture.MockUserManager.Object,
                _fixture.MockJwtTokenService.Object, _fixture.MockUserAccessorService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.RevokeToken(new RevokeDto { RefreshToken = "token101" }, "0.0.0.1"));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound);
            exception.ErrorMessage.Should().Be("RefreshToken not found.");
            exception.ErrorType.Should().Be(HttpStatusCode.NotFound.ToString());
        }

        [Fact]
        public async void RefreshToken_Throws_UnauthorizedException()
        {
            //Arrange
            var refreshToken = "I am invalid refresh token";
            _fixture.MockJwtTokenService.Setup(x => x.ValidateRefreshToken(refreshToken))
                .Returns(false);

            var repository = new AccountRepository(AutoMapperSingleton.Mapper, _fixture.MockUserManager.Object,
                _fixture.MockJwtTokenService.Object, _fixture.MockUserAccessorService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.RefreshToken(new RefreshDto { RefreshToken = refreshToken }, "0.0.0.1"));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.Unauthorized);
            exception.ErrorMessage.Should().Be("Invalid Refresh token.");
            exception.ErrorType.Should().Be(HttpStatusCode.Unauthorized.ToString());
        }

        [Fact]
        public async void RefreshToken_Throws_NotFoundException()
        {
            //Arrange
            var refreshToken = "token101";
            _fixture.MockJwtTokenService.Setup(x => x.ValidateRefreshToken(refreshToken))
                .Returns(true);

            _fixture.MockJwtTokenService.Setup(x => x.GetRefreshTokenAsync(It.IsAny<Expression<Func<RefreshToken, bool>>>()))
                .Returns<Expression<Func<RefreshToken, bool>>>(expression => Task.FromResult(_fixture.RefreshTokens.AsQueryable().FirstOrDefault(expression)));

            var repository = new AccountRepository(AutoMapperSingleton.Mapper, _fixture.MockUserManager.Object,
                _fixture.MockJwtTokenService.Object, _fixture.MockUserAccessorService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.RefreshToken(new RefreshDto { RefreshToken = refreshToken }, "0.0.0.1"));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound);
            exception.ErrorMessage.Should().Be("RefreshToken not found.");
            exception.ErrorType.Should().Be(HttpStatusCode.NotFound.ToString());
        }

        [Fact]
        public async void RefreshToken_Throws_BadRequestException()
        {
            //Arrange
            var refreshToken = "token2";
            _fixture.MockJwtTokenService.Setup(x => x.ValidateRefreshToken(refreshToken))
                .Returns(true);

            _fixture.MockJwtTokenService.Setup(x => x.GetRefreshTokenAsync(It.IsAny<Expression<Func<RefreshToken, bool>>>()))
                .Returns<Expression<Func<RefreshToken, bool>>>(expression => Task.FromResult(_fixture.RefreshTokens.AsQueryable().FirstOrDefault(expression)));

            var repository = new AccountRepository(AutoMapperSingleton.Mapper, _fixture.MockUserManager.Object,
                _fixture.MockJwtTokenService.Object, _fixture.MockUserAccessorService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.RefreshToken(new RefreshDto { RefreshToken = refreshToken }, "0.0.0.1"));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.BadRequest);
            exception.ErrorMessage.Should().Be("RefreshToken revoked by admin.");
            exception.ErrorType.Should().Be(HttpStatusCode.BadRequest.ToString());
        }

        [Fact]
        public async void RefreshToken_Throws_User_NotFoundException()
        {
            //Arrange
            var refreshToken = "token4";
            var userId = Guid.Parse("77d8500b-dd97-4b6d-9e43-48d8aa3916b9");
            _fixture.MockJwtTokenService.Setup(x => x.ValidateRefreshToken(refreshToken))
                .Returns(true);

            _fixture.MockJwtTokenService.Setup(x => x.GetRefreshTokenAsync(It.IsAny<Expression<Func<RefreshToken, bool>>>()))
                .Returns<Expression<Func<RefreshToken, bool>>>(expression => Task.FromResult(_fixture.RefreshTokens.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockUserManager.Setup(x => x.FindByIdAsync(userId.ToString()))
                .ReturnsAsync(_fixture.Users.FirstOrDefault(d => d.Id == userId));

            var repository = new AccountRepository(AutoMapperSingleton.Mapper, _fixture.MockUserManager.Object,
                _fixture.MockJwtTokenService.Object, _fixture.MockUserAccessorService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.RefreshToken(new RefreshDto { RefreshToken = refreshToken }, "0.0.0.1"));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound);
            exception.ErrorMessage.Should().Be("User associated for this token not found.");
            exception.ErrorType.Should().Be(HttpStatusCode.NotFound.ToString());
        }

        //[Fact]
        //public async void RefreshToken_Returns_TokenResultDto()
        //{
        //    //Arrange
        //    var refreshToken = "token3";
        //    var userId = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa391612");
        //    _fixture.MockJwtTokenService.Setup(x => x.ValidateRefreshToken(refreshToken))
        //        .Returns(true);

        //    _fixture.MockJwtTokenService.Setup(x => x.GetRefreshTokenAsync(It.IsAny<Expression<Func<RefreshToken, bool>>>()))
        //        .Returns<Expression<Func<RefreshToken, bool>>>(expression => Task.FromResult(_fixture.RefreshTokens.AsQueryable().FirstOrDefault(expression)));

        //    _fixture.MockUserManager.Setup(x => x.FindByIdAsync(userId.ToString()))
        //        .ReturnsAsync(_fixture.Users.FirstOrDefault(d => d.Id == userId));

        //    _fixture.MockJwtTokenService.Setup(x => x.GenerateAccessToken(It.IsAny<User>()))
        //        .ReturnsAsync("token5");

        //    _fixture.MockJwtTokenService.Setup(x => x.GenerateRefreshToken(userId, It.IsAny<string>()))
        //        .ReturnsAsync(new RefreshToken
        //        {
        //            Id = Guid.Parse("FC1779B6-5BC5-531E-B803-08D8AED9DA7D"),
        //            UserId = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa391612"),
        //            Token = "Rtoken5",
        //            Expires = DateTime.Now.AddHours(2),
        //            Created = DateTime.Now,
        //            CreatedByIp = "0.0.0.1"
        //        });

        //    _fixture.MockJwtTokenService.Setup(x => x.UpdateRefreshTokenAsync(It.IsAny<RefreshToken>()));

        //    var repository = new AccountRepository(AutoMapperSingleton.Mapper, _fixture.MockUserManager.Object,
        //        _fixture.MockJwtTokenService.Object, _fixture.MockUserAccessorService.Object);

        //    //Act
        //    var result = await repository.RefreshToken(new RefreshDto { RefreshToken = refreshToken }, "0.0.0.2");

        //    //Assert
        //    var tokenResult = result.Should().BeAssignableTo<TokenResultDto>().Subject;
        //    tokenResult.AccessToken.Should().Be("token5");
        //    tokenResult.RefreshToken.Should().Be("Rtoken5");
        //}
    }
}
