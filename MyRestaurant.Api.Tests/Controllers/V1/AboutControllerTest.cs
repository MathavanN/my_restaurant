using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using MyRestaurant.Api.Controllers.V1;
using Xunit;

namespace MyRestaurant.Api.Tests.Controllers.V1
{
    public class AboutControllerTest
    {
        [Fact]
        public void Get_ReturnOkObjectResult()
        {
            //Arrange
            var controller = new AboutController();

            //Act
            var result = controller.Get();

            //Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(new { data = "This is About Details v1" });
        }
    }
}
