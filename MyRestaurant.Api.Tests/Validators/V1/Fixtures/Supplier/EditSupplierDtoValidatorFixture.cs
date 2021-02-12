using MyRestaurant.Api.Validators.V1;
using MyRestaurant.Business.Dtos.V1;
using System;


namespace MyRestaurant.Api.Tests.Validators.V1.Fixtures
{
    public class EditSupplierDtoValidatorFixture : IDisposable
    {
        public EditSupplierDto Model { get; set; }
        public EditSupplierDtoValidator Validator { get; private set; }

        public EditSupplierDtoValidatorFixture()
        {
            Validator = new EditSupplierDtoValidator();
            Model = new EditSupplierDto
            {
                Name = "Test Supplier Pvt Ltd",
                Address1 = "#03-81, BLK 227",
                Address2 = "Bishan Street 23",
                City = "Bishan",
                Country = "Singapore",
                Telephone1 = "+94666553456",
                Telephone2 = "+94888775678",
                Fax = "+94666448856",
                Email = "test@gmail.com",
                ContactPerson = "James"
            };
        }

        public void Dispose()
        {
            Model = null;
            Validator = null;
        }
    }
}
