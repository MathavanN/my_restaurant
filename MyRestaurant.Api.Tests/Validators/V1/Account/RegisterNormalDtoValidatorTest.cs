﻿using FluentValidation.TestHelper;
using MyRestaurant.Api.Tests.Validators.V1.Fixtures;
using System.Collections.Generic;
using Xunit;

namespace MyRestaurant.Api.Tests.Validators.V1
{
    public class RegisterNormalDtoValidatorTest : IClassFixture<RegisterNormalDtoValidatorFixture>
    {
        private readonly RegisterNormalDtoValidatorFixture _fixture;
        public RegisterNormalDtoValidatorTest(RegisterNormalDtoValidatorFixture fixture)
        {
            _fixture = fixture;
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("     ")]
        public void Should_Have_Error_When_FirstName_Is_Invalid(string value)
        {
            //Arrange
            _fixture.Model.FirstName = value;

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.FirstName)
                .WithErrorMessage("FirstName is required.");
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("     ")]
        public void Should_Have_Error_When_LastName_Is_Invalid(string value)
        {
            //Arrange
            _fixture.Model.LastName = value;

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.LastName)
                .WithErrorMessage("LastName is required.");
        }

        [Theory]
        [InlineData("")]
        [InlineData("     ")]
        public void Should_Have_Error_When_Email_Is_Empty(string value)
        {
            //Arrange
            _fixture.Model.Email = value;

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Email)
                .WithErrorMessage("Email is required.");
        }

        [Fact]
        public void Should_Have_Error_When_Email_Is_Null()
        {
            //Arrange
            _fixture.Model.Email = null;

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Email)
                .WithErrorMessage("Email must not be empty.");
        }

        [Fact]
        public void Should_Have_Error_When_Email_Is_Invalid()
        {
            //Arrange
            _fixture.Model.Email = "test";

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Email)
                .WithErrorMessage("Email is not a valid email address.");
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("     ")]
        public void Should_Have_Error_When_Password_Is_Invalid(string value)
        {
            //Arrange
            _fixture.Model.Password = value;

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Password)
                .WithErrorMessage("Password is required.");
        }

        [Fact]
        public void Should_Have_Error_When_ConfirmPassword_Is_Invalid()
        {
            //Arrange
            _fixture.Model.Password = "test";
            _fixture.Model.ConfirmPassword = "dtest";

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Password)
                .WithErrorMessage("Password must be same.");
        }

        [Fact]
        public void Should_Have_Error_When_Roles_Is_Null()
        {
            //Arrange
            _fixture.Model.Roles = null;

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Roles)
                .WithErrorMessage("Role is required.");
        }

        [Fact]
        public void Should_Have_Error_When_Roles_Is_Empty()
        {
            // Arrange
            _fixture.Model.Roles = new List<string> { };

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Roles)
                .WithErrorMessage("Role is required.");
        }

        [Fact]
        public void Should_Have_Error_When_Roles_Contain_Invalid()
        {
            // Arrange
            _fixture.Model.Roles = new List<string> { "Normal", "Test" };

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Roles)
                .WithErrorMessage("'Roles' has a range of values which does not include 'Test'.");
        }

        [Fact]
        public void Should_Not_Have_Error_When_Valid_Dto()
        {
            //Arrange
            _fixture.Model.Email = "test@gmail.com";
            _fixture.Model.Password = "test";
            _fixture.Model.ConfirmPassword = "test";
            _fixture.Model.FirstName = "James";
            _fixture.Model.LastName = "Vasanth";
            _fixture.Model.Roles = new List<string> { "Report", "Normal" };

            //Act
            var result = _fixture.Validator.TestValidate(_fixture.Model);

            //Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Email);
            result.ShouldNotHaveValidationErrorFor(x => x.Password);
            result.ShouldNotHaveValidationErrorFor(x => x.FirstName);
            result.ShouldNotHaveValidationErrorFor(x => x.LastName);
            result.ShouldNotHaveValidationErrorFor(x => x.Roles);
        }
    }
}
