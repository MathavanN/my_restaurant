using FluentValidation.TestHelper;
using MyRestaurant.Api.Tests.Validators.V1.Fixtures;
using Xunit;

namespace MyRestaurant.Api.Tests.Validators.V1
{
    public class RefreshDtoValidatorTest : IClassFixture<RefreshDtoValidatorFixture>
    {
        private readonly RefreshDtoValidatorFixture _fixture;

        public RefreshDtoValidatorTest(RefreshDtoValidatorFixture fixture)
        {
            _fixture = fixture;
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("     ")]
        public void Should_Have_Error_When_RefreshToken_Is_Invalid(string value)
        {
            //Arrange
            _fixture.Model.RefreshToken = value;

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.RefreshToken)
                .WithErrorMessage("RefreshToken is required.");
        }

        [Fact]
        public void Should_Not_Have_Error_When_Valid_Dto()
        {
            //Arrange
            _fixture.Model.RefreshToken = "473ed8ba-2292-49e1-a930-5129a002e753";

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldNotHaveValidationErrorFor(x => x.RefreshToken);
        }
    }
}
