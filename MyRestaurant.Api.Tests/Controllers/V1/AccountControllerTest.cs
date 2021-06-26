using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MyRestaurant.Api.Controllers.V1;
using MyRestaurant.Api.Tests.Controllers.V1.Fixtures;
using MyRestaurant.Business.Dtos.V1;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace MyRestaurant.Api.Tests.Controllers.V1
{
    public class AccountControllerTest : IClassFixture<AccountControllerFixture>
    {
        private readonly AccountControllerFixture _fixture;
        public AccountControllerTest(AccountControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task RegisterAdmin_Returns_Success_OkObjectResult()
        {
            //Arrange
            _fixture.MockAccountRepository.Setup(x => x.RegisterAdminAsync(It.IsAny<RegisterAdminDto>()))
                .ReturnsAsync(_fixture.SuccessAdminRegisterResultDto);

            var controller = new AccountController(_fixture.MockAccountRepository.Object);

            //Act
            var result = await controller.RegisterAdmin(_fixture.ValidRegisterAdminDto);

            //Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var registerResult = okResult.Value.Should().BeAssignableTo<RegisterResultDto>().Subject;

            okResult.StatusCode.Should().Be(200);
            registerResult.Status.Should().Be("Success");
            registerResult.Message.Should().Be("User created successfully, grant Admin access.");
        }

        [Fact]
        public async Task RegisterAdmin_Returns_Failed_OkObjectResult()
        {
            //Arrange
            _fixture.MockAccountRepository.Setup(x => x.RegisterAdminAsync(It.IsAny<RegisterAdminDto>()))
                .ReturnsAsync(_fixture.FailedRegisterResultDto);

            var controller = new AccountController(_fixture.MockAccountRepository.Object);

            //Act
            var result = await controller.RegisterAdmin(_fixture.ValidRegisterAdminDto);

            //Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var registerResult = okResult.Value.Should().BeAssignableTo<RegisterResultDto>().Subject;

            okResult.StatusCode.Should().Be(200);
            registerResult.Status.Should().Be("Failed");
            registerResult.Message.Should().Be("Failed to create new user.");
        }

        [Fact]
        public async Task RegisterNormalUser_Returns_Success_OkObjectResult()
        {
            //Arrange
            _fixture.MockAccountRepository.Setup(x => x.RegisterNormalAsync(It.IsAny<RegisterNormalDto>()))
                .ReturnsAsync(_fixture.SuccessNormalRegisterResultDto);

            var controller = new AccountController(_fixture.MockAccountRepository.Object);

            //Act
            var result = await controller.RegisterNormalUser(_fixture.ValidRegisterNormalDto);

            //Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var registerResult = okResult.Value.Should().BeAssignableTo<RegisterResultDto>().Subject;

            okResult.StatusCode.Should().Be(200);
            registerResult.Status.Should().Be("Success");
            registerResult.Message.Should().Be("User created successfully, grant Report, Normal access.");
        }

        [Fact]
        public async Task RegisterNormalUser_Returns_Failed_OkObjectResult()
        {
            //Arrange
            _fixture.MockAccountRepository.Setup(x => x.RegisterNormalAsync(It.IsAny<RegisterNormalDto>()))
                .ReturnsAsync(_fixture.FailedRegisterResultDto);

            var controller = new AccountController(_fixture.MockAccountRepository.Object);

            //Act
            var result = await controller.RegisterNormalUser(_fixture.ValidRegisterNormalDto);

            //Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var registerResult = okResult.Value.Should().BeAssignableTo<RegisterResultDto>().Subject;

            okResult.StatusCode.Should().Be(200);
            registerResult.Status.Should().Be("Failed");
            registerResult.Message.Should().Be("Failed to create new user.");
        }

        [Fact]
        public async Task Login_Using_IpAddress_From_Request_Headers_Returns_OkObjectResult()
        {
            //Arrange
            _fixture.MockAccountRepository.Setup(x => x.LoginAsync(It.IsAny<LoginDto>(), It.IsAny<string>()))
                .ReturnsAsync(_fixture.ValidTokenResultDtoResult);

            _fixture.HttpContext.Request.Headers["X-Forwarded-For"] = "0.0.0.0";

            var controller = new AccountController(_fixture.MockAccountRepository.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = _fixture.HttpContext
                }
            };

            //Act
            var result = await controller.Login(_fixture.ValidLoginDto);

            //Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var tokenResult = okResult.Value.Should().BeAssignableTo<TokenResultDto>().Subject;

            okResult.StatusCode.Should().Be(200);
            tokenResult.AccessToken.Should().Be("this will be a JWT access token");
            tokenResult.RefreshToken.Should().Be("this will be a JWT refresh token");
        }

        [Fact]
        public async Task Login_Using_IpAddress_From_RemoteIpAddress_Returns_OkObjectResult()
        {
            //Arrange
            _fixture.MockAccountRepository.Setup(x => x.LoginAsync(It.IsAny<LoginDto>(), It.IsAny<string>()))
                .ReturnsAsync(_fixture.ValidTokenResultDtoResult);

            _fixture.HttpContext.Request.Headers["test"] = "0.0.0.0";
            _fixture.HttpContext.Connection.RemoteIpAddress = IPAddress.Parse("127.0.0.1");

            var controller = new AccountController(_fixture.MockAccountRepository.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = _fixture.HttpContext
                }
            };

            //Act
            var result = await controller.Login(_fixture.ValidLoginDto);

            //Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var tokenResult = okResult.Value.Should().BeAssignableTo<TokenResultDto>().Subject;

            okResult.StatusCode.Should().Be(200);
            tokenResult.AccessToken.Should().Be("this will be a JWT access token");
            tokenResult.RefreshToken.Should().Be("this will be a JWT refresh token");
        }

        [Fact]
        public async Task Refresh_Using_IpAddress_From_Request_Headers_Returns_OkObjectResult()
        {
            //Arrange
            _fixture.MockAccountRepository.Setup(x => x.RefreshToken(It.IsAny<RefreshDto>(), It.IsAny<string>()))
                .ReturnsAsync(_fixture.ValidTokenResultDtoResult);

            _fixture.HttpContext.Request.Headers["X-Forwarded-For"] = "0.0.0.0";

            var controller = new AccountController(_fixture.MockAccountRepository.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = _fixture.HttpContext
                }
            };

            //Act
            var result = await controller.Refresh(_fixture.ValidRefreshDto);

            //Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var tokenResult = okResult.Value.Should().BeAssignableTo<TokenResultDto>().Subject;

            okResult.StatusCode.Should().Be(200);
            tokenResult.AccessToken.Should().Be("this will be a JWT access token");
            tokenResult.RefreshToken.Should().Be("this will be a JWT refresh token");
        }

        [Fact]
        public async Task Refresh_Using_IpAddress_From_RemoteIpAddress_Returns_OkObjectResult()
        {
            //Arrange
            _fixture.MockAccountRepository.Setup(x => x.RefreshToken(It.IsAny<RefreshDto>(), It.IsAny<string>()))
                .ReturnsAsync(_fixture.ValidTokenResultDtoResult);

            _fixture.HttpContext.Request.Headers["test"] = "0.0.0.0";
            _fixture.HttpContext.Connection.RemoteIpAddress = IPAddress.Parse("127.0.0.1");

            var controller = new AccountController(_fixture.MockAccountRepository.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = _fixture.HttpContext
                }
            };

            //Act
            var result = await controller.Refresh(_fixture.ValidRefreshDto);

            //Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var tokenResult = okResult.Value.Should().BeAssignableTo<TokenResultDto>().Subject;

            okResult.StatusCode.Should().Be(200);
            tokenResult.AccessToken.Should().Be("this will be a JWT access token");
            tokenResult.RefreshToken.Should().Be("this will be a JWT refresh token");
        }

        [Fact]
        public async Task Revoke_Using_IpAddress_From_Request_Headers_Returns_OkObjectResult()
        {
            //Arrange
            _fixture.MockAccountRepository.Setup(x => x.RevokeToken(It.IsAny<RevokeDto>(), It.IsAny<string>()));

            _fixture.HttpContext.Request.Headers["X-Forwarded-For"] = "0.0.0.0";

            var controller = new AccountController(_fixture.MockAccountRepository.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = _fixture.HttpContext
                }
            };

            //Act
            var result = await controller.Revoke(_fixture.ValidRevokeDto);

            //Assert
            var objectResult = result.Should().BeOfType<NoContentResult>().Subject;
            objectResult.StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task Revoke_Using_IpAddress_From_RemoteIpAddres_Returns_OkObjectResult()
        {
            //Arrange
            _fixture.MockAccountRepository.Setup(x => x.RevokeToken(It.IsAny<RevokeDto>(), It.IsAny<string>()));

            _fixture.HttpContext.Request.Headers["test"] = "0.0.0.0";
            _fixture.HttpContext.Connection.RemoteIpAddress = IPAddress.Parse("127.0.0.1");

            var controller = new AccountController(_fixture.MockAccountRepository.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = _fixture.HttpContext
                }
            };

            //Act
            var result = await controller.Revoke(_fixture.ValidRevokeDto);

            //Assert
            var objectResult = result.Should().BeOfType<NoContentResult>().Subject;
            objectResult.StatusCode.Should().Be(204);
        }

        [Fact]
        public void GetCurrentUser_Returns_OkObjectResult()
        {
            //Arrange
            _fixture.MockAccountRepository.Setup(x => x.GetCurrentUser())
                .Returns(_fixture.CurrentUserDtoResult);

            var controller = new AccountController(_fixture.MockAccountRepository.Object);

            //Act
            var result = controller.GetCurrentUser();

            //Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var currentUser = okResult.Value.Should().BeAssignableTo<CurrentUserDto>().Subject;

            okResult.StatusCode.Should().Be(200);
            currentUser.FirstName.Should().Be("Simon");
            currentUser.LastName.Should().Be("Sais");
            currentUser.Email.Should().Be("simon@gmail.com");
            currentUser.FullName.Should().Be("Simon Sais");
            currentUser.UserId.Should().Be("77d8500b-dd97-4b6d-ce43-08d8aa3916b9");
            currentUser.Roles.Should().Contain("Admin");
        }

        [Fact]
        public async Task GetUsers_Returns_OkObjectResult()
        {
            //Arrange
            _fixture.MockAccountRepository.Setup(x => x.GetUsersAsync())
                .ReturnsAsync(_fixture.Users);

            var controller = new AccountController(_fixture.MockAccountRepository.Object);

            //Act
            var result = await controller.GetUsers();

            //Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var users = okResult.Value.Should().BeAssignableTo<IEnumerable<GetUserDto>>().Subject;

            okResult.StatusCode.Should().Be(200);
            users.Should().HaveCount(4);
        }
    }
}
