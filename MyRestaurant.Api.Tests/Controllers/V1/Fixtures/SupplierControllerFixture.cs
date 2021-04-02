using Microsoft.AspNetCore.Mvc;
using Moq;
using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Repositories.Contracts;
using System;
using System.Collections.Generic;

namespace MyRestaurant.Api.Tests.Controllers.V1.Fixtures
{
    public class SupplierControllerFixture : IDisposable
    {
        public ApiVersion ApiVersion { get; private set; }
        public Mock<ISupplierRepository> MockSupplierRepository { get; private set; }
        public IEnumerable<GetSupplierDto> Suppliers { get; private set; }
        public SupplierEnvelop SupplierEnvelop { get; private set; }
        public CreateSupplierDto ValidCreateSupplierDto { get; private set; }
        public GetSupplierDto CreateSupplierDtoResult { get; private set; }
        public EditSupplierDto ValidEditSupplierDto { get; private set; }
        public GetSupplierDto EditSupplierDtoResult { get; private set; }

        public SupplierControllerFixture()
        {
            ApiVersion = new ApiVersion(1, 0);

            MockSupplierRepository = new Mock<ISupplierRepository>();

            Suppliers = new List<GetSupplierDto>
            {
                new GetSupplierDto { Id = 1, Name = "Test Supplier Pvt Ltd",
                Address1 = "#03-81, BLK 227",
                Address2 = "Bishan Street 23",
                City = "Bishan",
                Country = "Singapore",
                Telephone1 = "+94666553456",
                Telephone2 = "+94888775678",
                Fax = "+94666448856",
                Email = "test@gmail.com",
                ContactPerson = "James" },
            };

            SupplierEnvelop = new SupplierEnvelop
            {
                SupplierCount = 1,
                Suppliers = Suppliers,
                ItemsPerPage = 10,
                TotalPages = 1
            };

            ValidCreateSupplierDto = new CreateSupplierDto
            {
                Name = "Jaffna Supplier Pvt Ltd",
                Address1 = "Kandy Road",
                Address2 = "Kaithady",
                City = "Jaffna",
                Country = "Sri Lanka",
                Telephone1 = "+94666553456",
                Telephone2 = "+94888775678",
                Fax = "+94666448856",
                Email = "test@gmail.com",
                ContactPerson = "Aathavan"
            };

            CreateSupplierDtoResult = new GetSupplierDto
            {
                Id = 2,
                Name = "Jaffna Supplier Pvt Ltd",
                Address1 = "Kandy Road",
                Address2 = "Kaithady",
                City = "Jaffna",
                Country = "Sri Lanka",
                Telephone1 = "+94666553456",
                Telephone2 = "+94888775678",
                Fax = "+94666448856",
                Email = "test@gmail.com",
                ContactPerson = "Aathavan"
            };

            ValidEditSupplierDto = new EditSupplierDto
            {
                Name = "Colombo Supplier Pvt Ltd",
                Address1 = "#06-02, Perea Lane",
                Address2 = "Wellawatta",
                City = "Colombo",
                Country = "Sri Lanka"
            };

            EditSupplierDtoResult = new GetSupplierDto
            {
                Id = 1,
                Name = "Colombo Supplier Pvt Ltd",
                Address1 = "#06-02, Perea Lane",
                Address2 = "Wellawatta",
                City = "Colombo",
                Country = "Sri Lanka",
                Telephone1 = "+94666553456",
                Telephone2 = "+94888775678",
                Fax = "+94666448856",
                Email = "test@gmail.com",
                ContactPerson = "James"
            };
        }
        public void Dispose()
        {
            MockSupplierRepository = null;
        }
    }
}
