using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Validators.V1;
using System;

namespace MyRestaurant.Business.Tests.Validators.V1.Fixtures
{
    public class LoginDtoValidatorFixture : IDisposable
    {
        public LoginDto Model { get; set; }
        public LoginDtoValidator Validator { get; private set; }

        public LoginDtoValidatorFixture()
        {
            Validator = new LoginDtoValidator();

            Model = new LoginDto
            {
                Email = "test@gmail.com",
                Password = "test"
            };
        }
        public void Dispose()
        {
            Model = null;
            Validator = null;
        }
    }
}
