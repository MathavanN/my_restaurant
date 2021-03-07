using FluentAssertions;
using Microsoft.AspNetCore.Authorization;
using MyRestaurant.Api.PolicyHandlers;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace MyRestaurant.Api.Tests.PolicyHandlers
{
    public class MyRestaurantAccessHandlerTest
    {
        [Fact]
        public async Task AuthorizationHandler_Should_Succeed()
        {
            //Arrange    
            var requirements = new List<MyRestaurantAccessRequirement> { new MyRestaurantAccessRequirement(ApplicationClaimType.Admin) };
            var claims = new List<Claim>
            {
                new Claim ("id", Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Email, "abc@gmail.com"),
                new Claim("firstName", "Admin"),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(ClaimTypes.Role, "Report"),
                new Claim("lastName", "Access"),
            };

            var user = new ClaimsPrincipal(new ClaimsIdentity(claims));
            var context = new AuthorizationHandlerContext(requirements, user, null);
            var subject = new MyRestaurantAccessHandler();

            //Act
            await subject.HandleAsync(context);

            //Assert
            context.HasSucceeded.Should().BeTrue();
        }

        [Fact]
        public async Task AuthorizationHandler_Should_Failed()
        {
            //Arrange    
            var requirements = new List<MyRestaurantAccessRequirement> { new MyRestaurantAccessRequirement(ApplicationClaimType.SuperAdmin) };
            var claims = new List<Claim>
            {
                new Claim ("id", Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Email, "abc@gmail.com"),
                new Claim("firstName", "Admin"),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(ClaimTypes.Role, "Report"),
                new Claim("lastName", "Access"),
            };

            var user = new ClaimsPrincipal(new ClaimsIdentity(claims));
            var context = new AuthorizationHandlerContext(requirements, user, null);
            var subject = new MyRestaurantAccessHandler();

            //Act
            await subject.HandleAsync(context);

            //Assert
            context.HasSucceeded.Should().BeFalse();
        }
    }
}
