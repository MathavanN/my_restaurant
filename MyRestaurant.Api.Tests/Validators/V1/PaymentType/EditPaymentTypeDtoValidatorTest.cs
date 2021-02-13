using FluentValidation.TestHelper;
using MyRestaurant.Api.Tests.Validators.V1.Fixtures;
using Xunit;

namespace MyRestaurant.Api.Tests.Validators.V1
{
    public class EditPaymentTypeDtoValidatorTest : IClassFixture<EditPaymentTypeDtoValidatorFixture>
    {
        private readonly EditPaymentTypeDtoValidatorFixture _fixture;
        public EditPaymentTypeDtoValidatorTest(EditPaymentTypeDtoValidatorFixture fixture)
        {
            _fixture = fixture;
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("     ")]
        public void Should_Have_Error_When_Name_Is_Invalid(string value)
        {
            //Arrange
            _fixture.Model.Name = value;

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Name)
                .WithErrorMessage("Name is required.");
        }

        [Fact]
        public void Should_Have_Error_When_Name_Length_Is_MoreThan_30()
        {
            //Arrange
            _fixture.Model.Name = new string('A', 31);

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Name)
                .WithErrorMessage("Name maximum length is 30.");
        }

        [Fact]
        public void Should_Not_Have_Error_When_Name_Is_Valid()
        {
            //Arrange
            _fixture.Model.Name = "Cash";

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Name);
        }
    }
}
