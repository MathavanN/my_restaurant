using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using Xunit;

namespace MyRestaurant.Services.Tests
{
    public class UserAccessorServiceTest
    {
        [Fact]
        public void GetCurrentUser_Returns_CurrentUser()
        {
            //Arrange
            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            var claims = new List<Claim>
            {
                new Claim ("id", Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Email, "abc@gmail.com"),
                new Claim("firstName", "Normal"),
                new Claim(ClaimTypes.Role, "Normal"),
                new Claim("lastName", "Access"),
            };
            mockHttpContextAccessor.Setup(d => d.HttpContext.User.Claims).Returns(claims);
            var service = new UserAccessorService(mockHttpContextAccessor.Object);

            //Act
            var result = service.GetCurrentUser();

            //Assert
            result.Should().NotBeNull().And.BeOfType<CurrentUser>();
        }

        [Fact]
        public void GetCurrentUser_Throws_ArgumentNullException()
        {
            //Arrange
            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            ClaimsPrincipal claimsPrincipal = null;
            mockHttpContextAccessor.Setup(d => d.HttpContext.User).Returns(claimsPrincipal);
            var service = new UserAccessorService(mockHttpContextAccessor.Object);

            //Act
            var exception = Assert.Throws<ArgumentNullException>(() => service.GetCurrentUser());

            //Assert
            exception.Message.Should().Contain("Value cannot be null.");
        }
    }
}
