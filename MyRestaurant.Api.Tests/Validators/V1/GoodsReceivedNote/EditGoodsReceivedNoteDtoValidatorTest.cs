using FluentValidation.TestHelper;
using MyRestaurant.Api.Tests.Validators.V1.Fixtures;
using System;
using Xunit;

namespace MyRestaurant.Api.Tests.Validators.V1
{
    public class EditGoodsReceivedNoteDtoValidatorTest : IClassFixture<EditGoodsReceivedNoteDtoValidatorFixture>
    {
        private readonly EditGoodsReceivedNoteDtoValidatorFixture _fixture;

        public EditGoodsReceivedNoteDtoValidatorTest(EditGoodsReceivedNoteDtoValidatorFixture fixture)
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
                .WithErrorMessage("Purchase order is required.");
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("     ")]
        public void Should_Have_Error_When_InvoiceNumber_Is_Invalid(string value)
        {
            //Arrange
            _fixture.Model.InvoiceNumber = value;

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.InvoiceNumber)
                .WithErrorMessage("Invoice number is required.");
        }

        [Fact]
        public void Should_Have_Error_When_InvoiceNumber_Length_Is_MoreThan_30()
        {
            //Arrange
            _fixture.Model.InvoiceNumber = new string('A', 31);

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.InvoiceNumber)
                .WithErrorMessage("Invoice number maximum length is 30.");
        }

        [Fact]
        public void Should_Have_Error_When_PaymentTypeId_Is_Invalid()
        {
            //Arrange
            _fixture.Model.PaymentTypeId = 0;

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.PaymentTypeId)
                .WithErrorMessage("Payment type is required.");
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
        public void Should_Have_Error_When_ReceivedBy_Is_Invalid()
        {
            //Arrange
            _fixture.Model.ReceivedBy = Guid.Empty;

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.ReceivedBy)
                .WithErrorMessage("Received by is required.");
        }

        [Fact]
        public void Should_Have_Error_When_ReceivedDate_Is_FeatureDate()
        {
            //Arrange
            _fixture.Model.ReceivedDate = DateTime.Now.AddDays(5);

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.ReceivedDate)
                .WithErrorMessage("Received date cannot be a future date.");
        }

        [Fact]
        public void Should_Have_Error_When_ReceivedDate_Is_DefaultDate()
        {
            //Arrange
            _fixture.Model.ReceivedDate = default;

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.ReceivedDate)
                .WithErrorMessage("Received date is required.");
        }

        [Fact]
        public void Should_Not_Have_Error_When_Valid_Dto()
        {
            //Arrange
            _fixture.Model.PurchaseOrderId = 1;
            _fixture.Model.InvoiceNumber = "INV_2021_02_13";
            _fixture.Model.PaymentTypeId = 1;
            _fixture.Model.Nbt = 0.3m;
            _fixture.Model.Vat = 0.5m;
            _fixture.Model.Discount = 1.2m;
            _fixture.Model.ReceivedBy = Guid.NewGuid();
            _fixture.Model.ReceivedDate = DateTime.Now.AddDays(-1);

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldNotHaveValidationErrorFor(x => x.PurchaseOrderId);
            result.ShouldNotHaveValidationErrorFor(x => x.InvoiceNumber);
            result.ShouldNotHaveValidationErrorFor(x => x.PaymentTypeId);
            result.ShouldNotHaveValidationErrorFor(x => x.Nbt);
            result.ShouldNotHaveValidationErrorFor(x => x.Vat);
            result.ShouldNotHaveValidationErrorFor(x => x.Discount);
            result.ShouldNotHaveValidationErrorFor(x => x.ReceivedBy);
            result.ShouldNotHaveValidationErrorFor(x => x.ReceivedDate);
        }
    }
}
