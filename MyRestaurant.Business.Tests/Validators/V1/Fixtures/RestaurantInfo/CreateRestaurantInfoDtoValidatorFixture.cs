using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Validators.V1;
using System;

namespace MyRestaurant.Business.Tests.Validators.V1.Fixtures
{
    public class CreateRestaurantInfoDtoValidatorFixture : IDisposable
    {
        public CreateRestaurantInfoDto Model { get; set; }
        public CreateRestaurantInfoDtoValidator Validator { get; private set; }

        public CreateRestaurantInfoDtoValidatorFixture()
        {
            Validator = new CreateRestaurantInfoDtoValidator();
            Model = new CreateRestaurantInfoDto
            {
                Name = "Golden Dining",
                Address = "Kandy Road, Kaithady",
                City = "Jaffna",
                Country = "Sri Lanka",
                LandLine = "+9423454544",
                Mobile = " +94567876786",
                Email = "test@gmail.com"
            };
        }

        public void Dispose()
        {
            Model = null;
            Validator = null;
        }
    }
}
