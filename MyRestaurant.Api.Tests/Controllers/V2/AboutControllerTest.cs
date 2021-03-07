using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using MyRestaurant.Api.Controllers.V2;
using Xunit;

namespace MyRestaurant.Api.Tests.Controllers.V2
{
    public class AboutControllerTest
    {
        [Fact]
        public void Get_Returns_OkObjectResult()
        {
            //Arrange
            var controller = new AboutController();

            //Act
            var result = controller.Get();

            //Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(new { data = "This is About Details v2" });
        }

        [Fact]
        public void GetSuperAdmin_Returns_OkObjectResult()
        {
            //Arrange
            var controller = new AboutController();

            //Act
            var result = controller.GetSuperAdmin();

            //Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(new { data = "This is About Details v2 Super Admin" });
        }

        [Fact]
        public void GetAdmin_Returns_OkObjectResult()
        {
            //Arrange
            var controller = new AboutController();

            //Act
            var result = controller.GetAdmin();

            //Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(new { data = "This is About Details v2 Admin" });
        }

        [Fact]
        public void GetAdminSpecific_Returns_OkObjectResult()
        {
            //Arrange
            var controller = new AboutController();

            //Act
            var result = controller.GetAdminSpecific();

            //Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(new { data = "This is About Details v2 Admin Specific" });
        }

        [Fact]
        public void GetNormal_Returns_OkObjectResult()
        {
            //Arrange
            var controller = new AboutController();

            //Act
            var result = controller.GetNormal();

            //Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(new { data = "This is About Details v2 Normal" });
        }

        [Fact]
        public void GetReport_Returns_OkObjectResult()
        {
            //Arrange
            var controller = new AboutController();

            //Act
            var result = controller.GetReport();

            //Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(new { data = "This is About Details v2 Report" });
        }
    }
}
