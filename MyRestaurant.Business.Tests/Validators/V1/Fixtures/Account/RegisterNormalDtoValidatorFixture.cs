using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Validators.V1;
using System;
using System.Collections.Generic;

namespace MyRestaurant.Business.Tests.Validators.V1.Fixtures
{
    public class RegisterNormalDtoValidatorFixture : IDisposable
    {
        public RegisterNormalDto Model { get; set; }
        public RegisterNormalDtoValidator Validator { get; private set; }

        public RegisterNormalDtoValidatorFixture()
        {
            Validator = new RegisterNormalDtoValidator();

            Model = new RegisterNormalDto
            {
                Email = "test@gmail.com",
                Password = "test",
                ConfirmPassword = "test",
                FirstName = "James",
                LastName = "Vasanth",
                Roles = new List<string> { "Report", "Normal" }
            };
        }
        public void Dispose()
        {
            Model = null;
            Validator = null;
        }
    }
}
