using FluentValidation.TestHelper;
using MyRestaurant.Business.Tests.Validators.V1.Fixtures;
using Xunit;

namespace MyRestaurant.Business.Tests.Validators.V1
{
    public class EditPurchaseOrderItemDtoValidatorTest : IClassFixture<EditPurchaseOrderItemDtoValidatorFixture>
    {
        private readonly EditPurchaseOrderItemDtoValidatorFixture _fixture;

        public EditPurchaseOrderItemDtoValidatorTest(EditPurchaseOrderItemDtoValidatorFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Should_Have_Error_When_PurchaseOrderId_Is_Invalid()
        {
            //Arrange
            _fixture.Model.PurchaseOrderId = 0;

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.PurchaseOrderId)
                .WithErrorMessage("Purchase order id is required.");
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
        public void Should_Not_Have_Error_When_Valid_Dto()
        {
            //Arrange
            _fixture.Model.PurchaseOrderId = 1;
            _fixture.Model.ItemId = 1;
            _fixture.Model.ItemUnitPrice = 1250.50m;
            _fixture.Model.Quantity = 1;

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldNotHaveValidationErrorFor(x => x.PurchaseOrderId);
            result.ShouldNotHaveValidationErrorFor(x => x.ItemId);
            result.ShouldNotHaveValidationErrorFor(x => x.ItemUnitPrice);
            result.ShouldNotHaveValidationErrorFor(x => x.Quantity);
        }
    }
}
