using FluentValidation.TestHelper;
using MyRestaurant.Business.Tests.Validators.V1.Fixtures;
using System;
using Xunit;

namespace MyRestaurant.Business.Tests.Validators.V1
{
    public class EditTransactionDtoValidatorTest : IClassFixture<EditTransactionDtoValidatorFixture>
    {
        private readonly EditTransactionDtoValidatorFixture _fixture;

        public EditTransactionDtoValidatorTest(EditTransactionDtoValidatorFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Should_Have_Error_When_TransactionTypeId_Is_Invalid()
        {
            //Arrange
            _fixture.Model.TransactionTypeId = 0;

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.TransactionTypeId)
                .WithErrorMessage("Transaction type is required.");
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

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("     ")]
        public void Should_Have_Error_When_Cashflow_Is_Invalid(string value)
        {
            //Arrange
            _fixture.Model.Cashflow = value;

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Cashflow)
                .WithErrorMessage("Cash flow is required.");
        }

        [Fact]
        public void Should_Have_Error_When_Cashflow_Is_InvalidCashflow()
        {
            //Arrange
            _fixture.Model.Cashflow = "invalid";

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Cashflow)
                .WithErrorMessage("'Cashflow' has a range of values which does not include 'invalid'.");
        }

        [Fact]
        public void Should_Have_Error_When_Amount_Is_Invalid()
        {
            //Arrange
            _fixture.Model.Amount = -0.5m;

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Amount)
                .WithErrorMessage("Amount must be a positive value.");
        }

        [Fact]
        public void Should_Have_Error_When_Date_Is_FeatureDate()
        {
            //Arrange
            _fixture.Model.Date = DateTime.Now.AddDays(5);

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Date)
                .WithErrorMessage("Transaction date cannot be a future date.");
        }

        [Fact]
        public void Should_Have_Error_When_ReceivedDate_Is_DefaultDate()
        {
            //Arrange
            _fixture.Model.Date = default;

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Date)
                .WithErrorMessage("Transaction date is required.");
        }

        [Fact]
        public void Should_Not_Have_Error_When_Valid_Dto()
        {
            //Arrange
            _fixture.Model.TransactionTypeId = 2;
            _fixture.Model.PaymentTypeId = 1;
            _fixture.Model.Date = DateTime.Now.AddDays(-5);
            _fixture.Model.Description = "Income from sale";
            _fixture.Model.Amount = 10110.5m;
            _fixture.Model.Cashflow = "Income";

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldNotHaveValidationErrorFor(x => x.TransactionTypeId);
            result.ShouldNotHaveValidationErrorFor(x => x.PaymentTypeId);
            result.ShouldNotHaveValidationErrorFor(x => x.Date);
            result.ShouldNotHaveValidationErrorFor(x => x.Description);
            result.ShouldNotHaveValidationErrorFor(x => x.Amount);
            result.ShouldNotHaveValidationErrorFor(x => x.Cashflow);
        }
    }
}
