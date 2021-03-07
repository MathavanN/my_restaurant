using FluentAssertions;
using Moq;
using MyRestaurant.Models;
using System;
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
            var token = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJuYmYiOjE2MDk3NDc5ODAsImV4cCI6MTYwOTc2OTU4MCwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzMDEiLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdDo0NDMwMSJ9.kvX-GbWYLkgEY3Kl9RaRvESbNRkl8NDBxGNTcTFUGBpaLwSM8oWt9U6bKQNoPbcAbNui3ubvbCapkmc3SWVmfg";
            var service = new JwtTokenService(_fixture.MockUserManager.Object, _myRestaurantContext, _fixture.JwtSettings);

            //Act
            var result = await service.GetRefreshTokenAsync(d => d.Token == token);

            //Assert
            result.Should().BeAssignableTo<RefreshToken>();
            result.ReplacedByToken.Should().BeNull();
            result.Id.Should().NotBeEmpty();
            result.User.FirstName.Should().Be("Normal");
            result.Token.Should().Be(token);
            result.CreatedByIp.Should().Be("0.0.0.1");
            result.IsActive.Should().Be(false);
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
            result.Should().BeAssignableTo<RefreshToken>();
            result.Token.Should().Be(refreshToken);
            result.RevokedByIp.Should().Be("127.0.0.0");
            result.IsActive.Should().Be(false);
        }

        [Fact]
        public async void GenerateAccessToken_Returns_AccessToken() 
        {
            //Arrange
            var adminUser = _myRestaurantContext.Users.ToList().First(d => d.FirstName == "Admin");
            var userClaims = _myRestaurantContext.UserClaims.ToList()
                                .Where(d => d.UserId == adminUser.Id)
                                .Select(d => new Claim(d.ClaimType, d.ClaimValue)).ToList();

            _fixture.MockUserManager.Setup(x => x.GetClaimsAsync(adminUser))
                .ReturnsAsync(userClaims);
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
            result.Should().BeAssignableTo<RefreshToken>();
            result.UserId.Should().Be(adminUser.Id);
            result.CreatedByIp.Should().Be("127.0.0.0");
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
        public async void ValidateRefreshToken_InValid_RefreshToken_Returns_False()
        {
            // Arrange
            var adminUser = _myRestaurantContext.Users.ToList().First(d => d.FirstName == "Admin");
            var service = new JwtTokenService(_fixture.MockUserManager.Object, _myRestaurantContext, _fixture.JwtSettings);
            var dbToken = await service.GetRefreshTokenAsync(d => d.Token == "token3");

            //Act
            var result = service.ValidateRefreshToken(dbToken.Token);

            //Assert
            result.Should().Be(false);
        }

        [Fact]
        public async void ValidateRefreshToken_Expired_RefreshToken_Returns_False()
        {
            // Arrange
            var token = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJuYmYiOjE2MDk3NDc5ODAsImV4cCI6MTYwOTc2OTU4MCwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzMDEiLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdDo0NDMwMSJ9.kvX-GbWYLkgEY3Kl9RaRvESbNRkl8NDBxGNTcTFUGBpaLwSM8oWt9U6bKQNoPbcAbNui3ubvbCapkmc3SWVmfg";
            var adminUser = _myRestaurantContext.Users.ToList().First(d => d.FirstName == "Admin");
            var service = new JwtTokenService(_fixture.MockUserManager.Object, _myRestaurantContext, _fixture.JwtSettings);
            var dbToken = await service.GetRefreshTokenAsync(d => d.Token == token);

            //Act
            var result = service.ValidateRefreshToken(dbToken.Token);

            //Assert
            result.Should().Be(false);
        }
    }
}
