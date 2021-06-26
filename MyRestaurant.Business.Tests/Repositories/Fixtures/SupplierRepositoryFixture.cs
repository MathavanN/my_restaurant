using Moq;
using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Models;
using MyRestaurant.Services;
using MyRestaurant.Services.Common;
using System;
using System.Collections.Generic;

namespace MyRestaurant.Business.Tests.Repositories.Fixtures
{
    public class SupplierRepositoryFixture : IDisposable
    {
        private bool _disposed;
        public Mock<ISupplierService> MockSupplierService { get; private set; }
        public IEnumerable<Supplier> Suppliers { get; private set; }
        public CollectionEnvelop<Supplier> CollectionEnvelop { get; private set; }
        public CreateSupplierDto CreateSupplierDto { get; private set; }
        public EditSupplierDto EditSupplierDto { get; private set; }
        public Supplier CreatedNewSupplier { get; private set; }

        public SupplierRepositoryFixture()
        {
            MockSupplierService = new Mock<ISupplierService>();

            Suppliers = new List<Supplier>
            {
                new Supplier
                {
                    Id = 1,
                    Name = "ABC Pvt Ltd",
                    Address1 = "American Mission School Road",
                    Address2 = "Madduvil South",
                    City = "Chavakachcheri",
                    Country = "Sri Lanka",
                    Telephone1 = "0765554345",
                    Telephone2 = "0766554567",
                    Fax = "",
                    Email = "goldendining2010@gmail.com",
                    ContactPerson = "James"
                },
                new Supplier
                {
                    Id = 2,
                    Name = "VBT Pvt Ltd",
                    Address1 = "VBT Road",
                    Address2 = "VBTt",
                    City = "Jaffna",
                    Country = "Sri Lanka",
                    Telephone1 = "0777113644",
                    Telephone2 = "",
                    Fax = "",
                    Email = "test@test.com",
                    ContactPerson = "James"
                }
            };

            CollectionEnvelop = new CollectionEnvelop<Supplier>
            {
                Items = Suppliers,
                ItemsPerPage = 10,
                TotalItems = 2,
            };

            CreateSupplierDto = new CreateSupplierDto
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

            CreatedNewSupplier = new Supplier
            {
                Id = 3,
                Name = CreateSupplierDto.Name,
                Address1 = CreateSupplierDto.Address1,
                Address2 = CreateSupplierDto.Address2,
                City = CreateSupplierDto.City,
                Country = CreateSupplierDto.Country,
                Telephone1 = CreateSupplierDto.Telephone1,
                Telephone2 = CreateSupplierDto.Telephone2,
                Fax = CreateSupplierDto.Fax,
                Email = CreateSupplierDto.Email,
                ContactPerson = CreateSupplierDto.ContactPerson
            };

            EditSupplierDto = new EditSupplierDto
            {
                Name = "VBT Pvt Ltd",
                Address1 = "#03-46, Blk 687",
                Address2 = "Hindu College Road",
                City = "Jaffna",
                Country = "Sri Lanka",
                Telephone1 = "0777113644",
                Telephone2 = "0777113644",
                Fax = "0777113644",
                Email = "test@test.com",
                ContactPerson = "James"
            };
        }
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    MockSupplierService = null;
                }

                _disposed = true;
            }
        }
    }
}
