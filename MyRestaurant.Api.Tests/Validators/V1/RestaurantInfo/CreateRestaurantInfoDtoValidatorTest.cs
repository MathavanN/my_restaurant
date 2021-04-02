using FluentValidation.TestHelper;
using MyRestaurant.Api.Tests.Validators.V1.Fixtures;
using Xunit;

namespace MyRestaurant.Api.Tests.Validators.V1
{
    public class CreateRestaurantInfoDtoValidatorTest : IClassFixture<CreateRestaurantInfoDtoValidatorFixture>
    {
        private readonly CreateRestaurantInfoDtoValidatorFixture _fixture;

        public CreateRestaurantInfoDtoValidatorTest(CreateRestaurantInfoDtoValidatorFixture fixture)
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
        public void Should_Have_Error_When_Name_Length_Is_MoreThan_256()
        {
            //Arrange
            _fixture.Model.Name = new string('A', 257);

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Name)
                .WithErrorMessage("Name maximum length is 256.");
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("     ")]
        public void Should_Have_Error_When_Address_Is_Invalid(string value)
        {
            //Arrange
            _fixture.Model.Address = value;

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Address)
                .WithErrorMessage("Address is required.");
        }

        [Fact]
        public void Should_Have_Error_When_Address_Length_Is_MoreThan_256()
        {
            //Arrange
            _fixture.Model.Address = new string('A', 257);

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Address)
                .WithErrorMessage("Address maximum length is 256.");
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("     ")]
        public void Should_Have_Error_When_City_Is_Invalid(string value)
        {
            //Arrange
            _fixture.Model.City = value;

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.City)
                .WithErrorMessage("City is required.");
        }

        [Fact]
        public void Should_Have_Error_When_City_Length_Is_MoreThan_256()
        {
            //Arrange
            _fixture.Model.City = new string('A', 257);

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.City)
                .WithErrorMessage("City maximum length is 256.");
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("     ")]
        public void Should_Have_Error_When_Country_Is_Invalid(string value)
        {
            //Arrange
            _fixture.Model.Country = value;

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Country)
                .WithErrorMessage("Country is required.");
        }

        [Fact]
        public void Should_Have_Error_When_Country_Length_Is_MoreThan_256()
        {
            //Arrange
            _fixture.Model.Country = new string('A', 257);

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Country)
                .WithErrorMessage("Country maximum length is 256.");
        }

        [Theory]
        [InlineData("test")]
        [InlineData("     ")]
        public void Should_Have_Error_When_Eamil_Is_Invalid(string value)
        {
            //Arrange
            _fixture.Model.Email = value;

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Email)
                .WithErrorMessage("Email is not a valid email address.");
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Should_Not_Have_Error_When_Eamil_Is_NotSpecified(string value)
        {
            //Arrange
            _fixture.Model.Email = value;

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Eamil_Is_Valid()
        {
            //Arrange
            _fixture.Model.Email = "test@gmail.com";

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Valid_Dto()
        {
            //Arrange
            _fixture.Model.Name = "Golden Dining";
            _fixture.Model.Address = "Kandy Road, Kaithady";
            _fixture.Model.City = "Jaffna";
            _fixture.Model.Country = "Sri Lanka";
            _fixture.Model.LandLine = "+9423454544";
            _fixture.Model.Mobile = " +94567876786";
            _fixture.Model.Email = "test@gmail.com";

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Name);
            result.ShouldNotHaveValidationErrorFor(x => x.Address);
            result.ShouldNotHaveValidationErrorFor(x => x.City);
            result.ShouldNotHaveValidationErrorFor(x => x.Country);
            result.ShouldNotHaveValidationErrorFor(x => x.Email);
        }
    }
}
