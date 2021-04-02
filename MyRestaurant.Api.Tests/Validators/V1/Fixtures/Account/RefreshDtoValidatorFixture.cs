using MyRestaurant.Api.Validators.V1;
using MyRestaurant.Business.Dtos.V1;
using System;

namespace MyRestaurant.Api.Tests.Validators.V1.Fixtures
{
    public class RefreshDtoValidatorFixture : IDisposable
    {
        public RefreshDto Model { get; set; }
        public RefreshDtoValidator Validator { get; private set; }

        public RefreshDtoValidatorFixture()
        {
            Validator = new RefreshDtoValidator();

            Model = new RefreshDto
            {
                RefreshToken = "473ed8ba-2292-49e1-a930-5129a002e753"
            };
        }
        public void Dispose()
        {
            Model = null;
            Validator = null;
        }
    }
}
