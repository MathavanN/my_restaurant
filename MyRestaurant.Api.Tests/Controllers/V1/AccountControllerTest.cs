using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MyRestaurant.Api.Controllers.V1.Controllers;
using MyRestaurant.Api.Tests.Controllers.V1.Fixtures;
using MyRestaurant.Business.Dtos.V1;
using System.Collections.Generic;
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
        public async void RegisterAdmin_Returns_Success_OkObjectResult()
        {
            //Arrange
            _fixture.MockAccountRepository.Setup(x => x.RegisterAdminAsync(_fixture.ValidRegisterAdminDto))
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
        public async void RegisterAdmin_Returns_Failed_OkObjectResult()
        {
            //Arrange
            _fixture.MockAccountRepository.Setup(x => x.RegisterAdminAsync(_fixture.ValidRegisterAdminDto))
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
        public async void RegisterNormalUser_Returns_Success_OkObjectResult()
        {
            //Arrange
            _fixture.MockAccountRepository.Setup(x => x.RegisterNormalAsync(_fixture.ValidRegisterNormalDto))
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
        public async void RegisterNormalUser_Returns_Failed_OkObjectResult()
        {
            //Arrange
            _fixture.MockAccountRepository.Setup(x => x.RegisterNormalAsync(_fixture.ValidRegisterNormalDto))
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
        public async void Login_Returns_OkObjectResult()
        {
            //Arrange
            _fixture.MockAccountRepository.Setup(x => x.LoginAsync(_fixture.ValidLoginDto, "0.0.0.0"))
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
        public async void Refresh_Returns_OkObjectResult()
        {
            //Arrange
            _fixture.MockAccountRepository.Setup(x => x.RefreshToken(_fixture.ValidRefreshDto, "0.0.0.0"))
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
        public async void Revoke_Returns_OkObjectResult()
        {
            //Arrange
            _fixture.MockAccountRepository.Setup(x => x.RevokeToken(_fixture.ValidRevokeDto, "0.0.0.0"));

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
        public async void GetUsers_Returns_OkObjectResult()
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
