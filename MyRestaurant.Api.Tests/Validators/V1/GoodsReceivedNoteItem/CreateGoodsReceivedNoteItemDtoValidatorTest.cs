using FluentValidation.TestHelper;
using MyRestaurant.Api.Tests.Validators.V1.Fixtures;
using Xunit;

namespace MyRestaurant.Api.Tests.Validators.V1
{
    public class CreateGoodsReceivedNoteItemDtoValidatorTest : IClassFixture<CreateGoodsReceivedNoteItemDtoValidatorFixture>
    {
        private readonly CreateGoodsReceivedNoteItemDtoValidatorFixture _fixture;

        public CreateGoodsReceivedNoteItemDtoValidatorTest(CreateGoodsReceivedNoteItemDtoValidatorFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Should_Have_Error_When_GoodsReceivedNoteId_Is_Invalid()
        {
            //Arrange
            _fixture.Model.GoodsReceivedNoteId = 0;

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.GoodsReceivedNoteId)
                .WithErrorMessage("GRN is required.");
        }

        [Fact]
        public void Should_Have_Error_When_ItemId_Is_Invalid()
        {
            //Arrange
            _fixture.Model.ItemId = 0;

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.ItemId)
                .WithErrorMessage("Item type is required.");
        }

        [Fact]
        public void Should_Have_Error_When_ItemUnitPrice_Is_Invalid()
        {
            //Arrange
            _fixture.Model.ItemUnitPrice = -10;

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.ItemUnitPrice)
                .WithErrorMessage("Item unit price must be greater than zero.");
        }

        [Fact]
        public void Should_Have_Error_When_Quantity_Is_Invalid()
        {
            //Arrange
            _fixture.Model.Quantity = -10;

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Quantity)
                .WithErrorMessage("Quantity must be grater than zero.");
        }

        [Fact]
        public void Should_Have_Error_When_NBT_Is_Invalid()
        {
            //Arrange
            _fixture.Model.Nbt = -0.5m;

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Nbt)
                .WithErrorMessage("NBT must be a positive value.");
        }

        [Fact]
        public void Should_Have_Error_When_VAT_Is_Invalid()
        {
            //Arrange
            _fixture.Model.Vat = -0.5m;

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Vat)
                .WithErrorMessage("VAT must be a positive value.");
        }

        [Fact]
        public void Should_Have_Error_When_Discount_Is_Invalid()
        {
            //Arrange
            _fixture.Model.Discount = -0.5m;

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Discount)
                .WithErrorMessage("Discount must be a positive value.");
        }

        [Fact]
        public void Should_Not_Have_Error_When_Valid_Dto()
        {
            //Arrange
            _fixture.Model.GoodsReceivedNoteId = 1;
            _fixture.Model.ItemId = 1;
            _fixture.Model.ItemUnitPrice = 1450.50m;
            _fixture.Model.Quantity = 2;
            _fixture.Model.Nbt = 0.3m;
            _fixture.Model.Vat = 0.5m;
            _fixture.Model.Discount = 1.2m;

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldNotHaveValidationErrorFor(x => x.GoodsReceivedNoteId);
            result.ShouldNotHaveValidationErrorFor(x => x.ItemId);
            result.ShouldNotHaveValidationErrorFor(x => x.ItemUnitPrice);
            result.ShouldNotHaveValidationErrorFor(x => x.Quantity);
            result.ShouldNotHaveValidationErrorFor(x => x.Nbt);
            result.ShouldNotHaveValidationErrorFor(x => x.Vat);
            result.ShouldNotHaveValidationErrorFor(x => x.Discount);
        }
    }
}
