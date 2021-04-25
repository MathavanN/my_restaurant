using FluentValidation.TestHelper;
using MyRestaurant.Business.Tests.Validators.V1.Fixtures;
using Xunit;

namespace MyRestaurant.Business.Tests.Validators.V1
{
    public class CreatePurchaseOrderDtoValidatorTest : IClassFixture<CreatePurchaseOrderDtoValidatorFixture>
    {
        private readonly CreatePurchaseOrderDtoValidatorFixture _fixture;

        public CreatePurchaseOrderDtoValidatorTest(CreatePurchaseOrderDtoValidatorFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Should_Have_Error_When_SupplierId_Is_Invalid()
        {
            //Arrange
            _fixture.Model.SupplierId = 0;

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.SupplierId)
                .WithErrorMessage("Supplier is required.");
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
            _fixture.Model.SupplierId = 1;
            _fixture.Model.Description = "Purchase order description";

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldNotHaveValidationErrorFor(x => x.SupplierId);
            result.ShouldNotHaveValidationErrorFor(x => x.Description);
        }
    }
}
