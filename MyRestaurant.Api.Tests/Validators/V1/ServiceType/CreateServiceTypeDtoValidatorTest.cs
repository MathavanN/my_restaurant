using FluentValidation.TestHelper;
using MyRestaurant.Api.Tests.Validators.V1.Fixtures;
using Xunit;

namespace MyRestaurant.Api.Tests.Validators.V1
{
    public class CreateServiceTypeDtoValidatorTest : IClassFixture<CreateServiceTypeDtoValidatorFixture>
    {
        private readonly CreateServiceTypeDtoValidatorFixture _fixture;

        public CreateServiceTypeDtoValidatorTest(CreateServiceTypeDtoValidatorFixture fixture)
        {
            _fixture = fixture;
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("     ")]
        public void Should_Have_Error_When_Type_Is_Invalid(string value)
        {
            //Arrange
            _fixture.Model.Type = value;

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Type)
                .WithErrorMessage("Service type is required.");
        }

        [Fact]
        public void Should_Have_Error_When_Type_Length_Is_MoreThan_20()
        {
            //Arrange
            _fixture.Model.Type = new string('A', 21); ;

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Type)
                .WithErrorMessage("Service type maximum length is 20.");
        }

        [Fact]
        public void Should_Not_Have_Error_When_Type_Is_Valid()
        {
            //Arrange
            _fixture.Model.Type = "Take Away";

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Type);
        }

    }
}
