using FluentValidation.TestHelper;
using MyRestaurant.Business.Tests.Validators.V1.Fixtures;
using Xunit;

namespace MyRestaurant.Business.Tests.Validators.V1
{
    public class EditStockItemDtoValidatorTest : IClassFixture<EditStockItemDtoValidatorFixture>
    {
        private readonly EditStockItemDtoValidatorFixture _fixture;
        public EditStockItemDtoValidatorTest(EditStockItemDtoValidatorFixture fixture)
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
                .WithErrorMessage("Item name is required.");
        }

        [Fact]
        public void Should_Have_Error_When_Name_Length_Is_MoreThan_250()
        {
            //Arrange
            _fixture.Model.Name = new string('A', 251);

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Name)
                .WithErrorMessage("Item name maximum length is 250.");
        }

        [Fact]
        public void Should_Have_Error_When_TypeId_Is_Invalid()
        {
            //Arrange
            _fixture.Model.TypeId = 0;

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.TypeId)
                .WithErrorMessage("Item type is required.");
        }

        [Fact]
        public void Should_Have_Error_When_UnitOfMeasureId_Is_Invalid()
        {
            //Arrange
            _fixture.Model.UnitOfMeasureId = 0;

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.UnitOfMeasureId)
                .WithErrorMessage("Unit of measure is required.");
        }

        [Fact]
        public void Should_Have_Error_When_ItemUnit_Is_Invalid()
        {
            //Arrange
            _fixture.Model.ItemUnit = -10;

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.ItemUnit)
                .WithErrorMessage("Item unit should be greater than 0.");
        }

        [Fact]
        public void Should_Have_Error_When_Description_Is_MoreThan_500()
        {
            //Arrange
            _fixture.Model.Description = new string('A', 501);

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Description)
                .WithErrorMessage("Description maximum length is 500.");
        }

        [Fact]
        public void Should_Not_Have_Error_When_Valid_Dto()
        {
            //Arrange
            _fixture.Model.TypeId = 1;
            _fixture.Model.Name = "Sugar";
            _fixture.Model.ItemUnit = 10;
            _fixture.Model.UnitOfMeasureId = 1;
            _fixture.Model.Description = "Sugar 10kg bag";

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldNotHaveValidationErrorFor(x => x.TypeId);
            result.ShouldNotHaveValidationErrorFor(x => x.UnitOfMeasureId);
            result.ShouldNotHaveValidationErrorFor(x => x.Name);
            result.ShouldNotHaveValidationErrorFor(x => x.ItemUnit);
            result.ShouldNotHaveValidationErrorFor(x => x.Description);
        }
    }
}
