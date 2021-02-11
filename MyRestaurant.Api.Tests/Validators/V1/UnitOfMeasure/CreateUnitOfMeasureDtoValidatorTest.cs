using FluentValidation.TestHelper;
using MyRestaurant.Api.Tests.Validators.V1.Fixtures;
using Xunit;

namespace MyRestaurant.Api.Tests.Validators.V1
{
    public class CreateUnitOfMeasureDtoValidatorTest : IClassFixture<CreateUnitOfMeasureDtoValidatorFixture>
    {
        private readonly CreateUnitOfMeasureDtoValidatorFixture _fixture;
        public CreateUnitOfMeasureDtoValidatorTest(CreateUnitOfMeasureDtoValidatorFixture fixture)
        {
            _fixture = fixture;
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("     ")]
        public void Should_Have_Error_When_Code_Is_Invalid(string value)
        {
            //Arrange
            _fixture.Model.Code = value;

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Code)
                .WithErrorMessage("Code is required.");
        }

        [Fact]
        public void Should_Have_Error_When_Code_Length_Is_MoreThan_20()
        {
            //Arrange
            _fixture.Model.Code = new string('A', 21);

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Code)
                .WithErrorMessage("Code maximum length is 20.");
        }

        [Fact]
        public void Should_Not_Have_Error_When_Code_Is_Valid()
        {
            //Arrange
            _fixture.Model.Code = "kg";

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Code);
        }

        [Fact]
        public void Should_Have_Error_When_Description_Is_MoreThan_50()
        {
            //Arrange
            _fixture.Model.Description = new string('A', 51);

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Description)
                .WithErrorMessage("Description maximum length is 50.");
        }
    }
}
