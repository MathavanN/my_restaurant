using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using MyRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Xunit;

namespace MyRestaurant.Services.Tests
{
    public class JwtTokenServiceTest : MyRestaurantContextTestBase, IClassFixture<JwtTokenServiceFixture>
    {
        private readonly JwtTokenServiceFixture _fixture;
        public JwtTokenServiceTest(JwtTokenServiceFixture fixture)
        {
            UserInitializer.Initialize(_myRestaurantContext);
            TokenInitializer.Initialize(_myRestaurantContext);
            _fixture = fixture;
        }

        [Fact]
        public async void GetRefreshTokenAsync_Returns_RefreshToken()
        {
            //Arrange
            var service = new JwtTokenService(_fixture.MockUserManager.Object, _myRestaurantContext, _fixture.JwtSettings);

            //Act
            var result = await service.GetRefreshTokenAsync(d => d.Token == "token4");

            //Assert
            var refreshToken = result.Should().BeAssignableTo<RefreshToken>().Subject;
            refreshToken.Token.Should().Be("token4");
            refreshToken.CreatedByIp.Should().Be("0.0.0.1");
        }

        [Fact]
        public async void GetRefreshTokenAsync_Returns_Null()
        {
            //Arrange
            var service = new JwtTokenService(_fixture.MockUserManager.Object, _myRestaurantContext, _fixture.JwtSettings);

            //Act
            var result = await service.GetRefreshTokenAsync(d => d.Token == "token44444");

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async void UpdateRefreshTokenAsync_Successfully_Updated()
        {
            //Arrange
            var refreshToken = "token4";
            var service = new JwtTokenService(_fixture.MockUserManager.Object, _myRestaurantContext, _fixture.JwtSettings);

            //Act
            var dbToken = await service.GetRefreshTokenAsync(d => d.Token == refreshToken);
            dbToken.Revoked = DateTime.UtcNow;
            dbToken.RevokedByIp = "127.0.0.0";

            await service.UpdateRefreshTokenAsync(dbToken);

            var result = await service.GetRefreshTokenAsync(d => d.Token == refreshToken);

            //Assert
            var token = result.Should().BeAssignableTo<RefreshToken>().Subject;
            token.Token.Should().Be(refreshToken);
            token.RevokedByIp.Should().Be("127.0.0.0");
            token.IsActive.Should().Be(false);
        }

        [Fact]
        public async void GenerateAccessToken_Returns_AccessToken() 
        {
            //Arrange
            var adminUser = _myRestaurantContext.Users.ToList().First(d => d.FirstName == "Admin");

            _fixture.MockUserManager.Setup(x => x.GetClaimsAsync(adminUser))
                .ReturnsAsync(_fixture.Claims);
            _fixture.MockUserManager.Setup(x => x.GetRolesAsync(adminUser))
                .ReturnsAsync(adminUser.UserRoles.Select(d => d.Role.Name).ToList());

            var service = new JwtTokenService(_fixture.MockUserManager.Object, _myRestaurantContext, _fixture.JwtSettings);

            //Act
            var result = await service.GenerateAccessToken(adminUser);

            //Assert
            result.Should().BeAssignableTo<string>();
        }

        [Fact]
        public async void GenerateRefreshToken_Returns_RefreshToken()
        {
            //Arrange
            var adminUser = _myRestaurantContext.Users.ToList().First(d => d.FirstName == "Admin");
            var service = new JwtTokenService(_fixture.MockUserManager.Object, _myRestaurantContext, _fixture.JwtSettings);

            //Act
            var result = await service.GenerateRefreshToken(adminUser.Id, "127.0.0.0");

            //Assert
            var refreshToken = result.Should().BeAssignableTo<RefreshToken>().Subject;
            refreshToken.UserId.Should().Be(adminUser.Id);
            refreshToken.CreatedByIp.Should().Be("127.0.0.0");
        }

        [Fact]
        public async void ValidateRefreshToken_Valid_RefreshToken_Returns_True()
        {
            // Arrange
            var adminUser = _myRestaurantContext.Users.ToList().First(d => d.FirstName == "Admin");
            var service = new JwtTokenService(_fixture.MockUserManager.Object, _myRestaurantContext, _fixture.JwtSettings);
            var createdToken = await service.GenerateRefreshToken(adminUser.Id, "127.0.0.0");

            //Act
            var result = service.ValidateRefreshToken(createdToken.Token);

            //Assert
            result.Should().Be(true);
        }

        [Fact]
        public async void ValidateRefreshToken_InValid_RefreshToken_Returns_True()
        {
            // Arrange
            var adminUser = _myRestaurantContext.Users.ToList().First(d => d.FirstName == "Admin");
            var service = new JwtTokenService(_fixture.MockUserManager.Object, _myRestaurantContext, _fixture.JwtSettings);
            var dbToken = await service.GetRefreshTokenAsync(d => d.Token == "token2");

            //Act
            var result = service.ValidateRefreshToken(dbToken.Token);

            //Assert
            result.Should().Be(false);
        }
    }
}
