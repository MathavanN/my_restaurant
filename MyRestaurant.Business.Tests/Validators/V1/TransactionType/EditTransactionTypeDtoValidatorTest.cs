using FluentValidation.TestHelper;
using MyRestaurant.Business.Tests.Validators.V1.Fixtures;
using Xunit;

namespace MyRestaurant.Business.Tests.Validators.V1
{
    public class EditTransactionTypeDtoValidatorTest : IClassFixture<EditTransactionTypeDtoValidatorFixture>
    {
        private readonly EditTransactionTypeDtoValidatorFixture _fixture;

        public EditTransactionTypeDtoValidatorTest(EditTransactionTypeDtoValidatorFixture fixture)
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
                .WithErrorMessage("Type is required.");
        }

        [Fact]
        public void Should_Have_Error_When_Type_Length_Is_MoreThan_50()
        {
            //Arrange
            _fixture.Model.Type = new string('A', 51);

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Type)
                .WithErrorMessage("Type maximum length is 50.");
        }

        [Fact]
        public void Should_Not_Have_Error_When_Type_Is_Valid()
        {
            //Arrange
            _fixture.Model.Type = "Extra Income";

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Type);
        }
    }
}
