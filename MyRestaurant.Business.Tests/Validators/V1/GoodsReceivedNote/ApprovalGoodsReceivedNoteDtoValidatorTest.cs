using FluentValidation.TestHelper;
using MyRestaurant.Business.Tests.Validators.V1.Fixtures;
using Xunit;

namespace MyRestaurant.Business.Tests.Validators.V1
{
    public class ApprovalGoodsReceivedNoteDtoValidatorTest : IClassFixture<ApprovalGoodsReceivedNoteDtoValidatorFixture>
    {
        private readonly ApprovalGoodsReceivedNoteDtoValidatorFixture _fixture;

        public ApprovalGoodsReceivedNoteDtoValidatorTest(ApprovalGoodsReceivedNoteDtoValidatorFixture fixture)
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

        [Fact]
        public void Should_Not_Have_Error_When_Valid_Dto()
        {
            //Arrange
            _fixture.Model.ApprovalStatus = "Pending";
            _fixture.Model.ApprovalReason = "GRN items are received";

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldNotHaveValidationErrorFor(x => x.ApprovalStatus);
            result.ShouldNotHaveValidationErrorFor(x => x.ApprovalReason);
        }
    }
}
