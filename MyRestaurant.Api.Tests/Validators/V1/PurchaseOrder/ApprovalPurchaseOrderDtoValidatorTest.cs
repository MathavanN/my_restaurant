using FluentValidation.TestHelper;
using MyRestaurant.Api.Tests.Validators.V1.Fixtures;
using Xunit;

namespace MyRestaurant.Api.Tests.Validators.V1
{
    public class ApprovalPurchaseOrderDtoValidatorTest : IClassFixture<ApprovalPurchaseOrderDtoValidatorFixture>
    {
        private readonly ApprovalPurchaseOrderDtoValidatorFixture _fixture;

        public ApprovalPurchaseOrderDtoValidatorTest(ApprovalPurchaseOrderDtoValidatorFixture fixture)
        {
            _fixture = fixture;
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("     ")]
        public void Should_Have_Error_When_ApprovalStatus_Is_Invalid(string value)
        {
            //Arrange
            _fixture.Model.ApprovalStatus = value;

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.ApprovalStatus)
                .WithErrorMessage("Approval status is required.");
        }

        [Fact]
        public void Should_Have_Error_When_ApprovalStatus_Is_InvalidStaus()
        {
            //Arrange
            _fixture.Model.ApprovalStatus = "invalid";

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.ApprovalStatus)
                .WithErrorMessage("'Approval Status' has a range of values which does not include 'invalid'.");
        }

        [Fact]
        public void Should_Have_Error_When_Description_Is_MoreThan_500()
        {
            //Arrange
            _fixture.Model.ApprovalReason = new string('A', 501);

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.ApprovalReason)
                .WithErrorMessage("Approval reason maximum length is 500.");
        }
    }
}
