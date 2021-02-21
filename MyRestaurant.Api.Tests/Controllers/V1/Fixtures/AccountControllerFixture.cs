using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Repositories.Contracts;
using System;
using System.Collections.Generic;

namespace MyRestaurant.Api.Tests.Controllers.V1.Fixtures
{
    public class AccountControllerFixture : IDisposable
    {
        public ApiVersion ApiVersion { get; private set; }
        public Mock<IAccountRepository> MockAccountRepository { get; private set; }
        public IEnumerable<GetUserDto> Users { get; private set; }
        public RegisterAdminDto ValidRegisterAdminDto { get; private set; }
        public RegisterNormalDto ValidRegisterNormalDto { get; private set; }
        public LoginDto ValidLoginDto { get; private set; }
        public RefreshDto ValidRefreshDto { get; private set; }
        public RevokeDto ValidRevokeDto { get; private set; }
        public CurrentUserDto CurrentUserDtoResult { get; private set; }
        public TokenResultDto ValidTokenResultDtoResult { get; private set; }
        public RegisterResultDto SuccessAdminRegisterResultDto { get; private set; }
        public RegisterResultDto FailedRegisterResultDto { get; private set; }
        public RegisterResultDto SuccessNormalRegisterResultDto { get; private set; }
        public DefaultHttpContext HttpContext { get; set; }

        public AccountControllerFixture()
        {
            ApiVersion = new ApiVersion(1, 0);

            MockAccountRepository = new Mock<IAccountRepository>();

            HttpContext = new DefaultHttpContext();
            Users = new List<GetUserDto>
            {
                new GetUserDto
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Anna",
                    LastName = "Domino",
                    Email = "anna@gmail.com",
                    PhoneNumber = "",
                    Roles = new List<string> { "SuperAdmin", "Admin", "Report", "Normal" }
                },
                new GetUserDto
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Neil",
                    LastName = "Down",
                    Email = "neil@gmail.com",
                    PhoneNumber = "",
                    Roles = new List<string> { "Admin" }
                },
                new GetUserDto
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Mark",
                    LastName = "Ateer",
                    Email = "mark@gmail.com",
                    PhoneNumber = "",
                    Roles = new List<string> { "Report", "Normal" }
                },
                new GetUserDto
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Dave",
                    LastName = "Allippa",
                    Email = "dave@gmail.com",
                    PhoneNumber = "",
                    Roles = new List<string> { "Normal" }
                }
            };

            ValidRegisterAdminDto = new RegisterAdminDto
            {
                FirstName = "John",
                LastName = "Quil",
                ConfirmPassword = "password1234",
                Password = "password1234",
                Email = "john@gmail.com"
            };

            ValidRegisterNormalDto = new RegisterNormalDto
            {
                FirstName = "Rose",
                LastName = "Bush",
                ConfirmPassword = "password1234",
                Password = "password1234",
                Email = "rose@gmail.com",
                Roles = new List<string> { "Report", "Normal" }
            };

            ValidLoginDto = new LoginDto
            {
                Email = "test@gmail.com",
                Password = "password1234"
            };

            ValidRevokeDto = new RevokeDto
            {
                RefreshToken = "this is refresh JWT token"
            };

            SuccessAdminRegisterResultDto = new RegisterResultDto
            {
                Status = "Success",
                Message = "User created successfully, grant Admin access."
            };

            FailedRegisterResultDto = new RegisterResultDto
            {
                Status = "Failed",
                Message = "Failed to create new user."
            };

            SuccessNormalRegisterResultDto = new RegisterResultDto
            {
                Status = "Success",
                Message = $"User created successfully, grant {string.Join(", ", ValidRegisterNormalDto.Roles)} access."
            };

            ValidTokenResultDtoResult = new TokenResultDto
            {
                AccessToken = "this will be a JWT access token",
                RefreshToken = "this will be a JWT refresh token",
            };

            CurrentUserDtoResult = new CurrentUserDto
            {
                FirstName = "Simon",
                LastName = "Sais",
                Email = "simon@gmail.com",
                Roles = new List<string> { "SuperAdmin", "Admin" },
                FullName = "Simon Sais",
                UserId = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa3916b9")
            };
        }

        public void Dispose()
        {
            MockAccountRepository = null;
        }
    }
}
