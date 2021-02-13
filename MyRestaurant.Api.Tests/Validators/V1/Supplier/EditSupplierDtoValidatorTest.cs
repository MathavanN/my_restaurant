using FluentValidation.TestHelper;
using MyRestaurant.Api.Tests.Validators.V1.Fixtures;
using Xunit;

namespace MyRestaurant.Api.Tests.Validators.V1
{
    public class EditSupplierDtoValidatorTest : IClassFixture<EditSupplierDtoValidatorFixture>
    {
        private readonly EditSupplierDtoValidatorFixture _fixture;

        public EditSupplierDtoValidatorTest(EditSupplierDtoValidatorFixture fixture)
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
        public void Should_Have_Error_When_Address1_Is_Invalid(string value)
        {
            //Arrange
            _fixture.Model.Address1 = value;

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Address1)
                .WithErrorMessage("Address is required.");
        }

        [Fact]
        public void Should_Have_Error_When_Address1_Length_Is_MoreThan_256()
        {
            //Arrange
            _fixture.Model.Address1 = new string('A', 257);

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Address1)
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
        public void Should_Have_Error_When_City_Length_Is_MoreThan_100()
        {
            //Arrange
            _fixture.Model.City = new string('A', 101);

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.City)
                .WithErrorMessage("City maximum length is 100.");
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
        public void Should_Have_Error_When_Country_Length_Is_MoreThan_100()
        {
            //Arrange
            _fixture.Model.Country = new string('A', 101);

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Country)
                .WithErrorMessage("Country maximum length is 100.");
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
            _fixture.Model.Name = "Test Supplier Pvt Ltd";
            _fixture.Model.Address1 = "#03-81, BLK 227";
            _fixture.Model.Address2 = "Bishan Street 23";
            _fixture.Model.City = "Bishan";
            _fixture.Model.Country = "Singapore";
            _fixture.Model.Telephone1 = "+94666553456";
            _fixture.Model.Telephone2 = "+94888775678";
            _fixture.Model.Fax = "+94666448856";
            _fixture.Model.Email = "test@gmail.com";
            _fixture.Model.ContactPerson = "James";

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Name);
            result.ShouldNotHaveValidationErrorFor(x => x.Address1);
            result.ShouldNotHaveValidationErrorFor(x => x.City);
            result.ShouldNotHaveValidationErrorFor(x => x.Country);
            result.ShouldNotHaveValidationErrorFor(x => x.Email);
        }
    }
}
