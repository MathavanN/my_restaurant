using FluentAssertions;
using MyRestaurant.Models;
using MyRestaurant.Services.Common;
using System.Collections.Generic;
using Xunit;

namespace MyRestaurant.Services.Tests
{
    public class SupplierServiceTest : MyRestaurantContextTestBase
    {
        public SupplierServiceTest()
        {
            SupplierInitializer.Initialize(_myRestaurantContext);
        }

        [Fact]
        public async void GetSuppliersAsync_Returns_Suppliers()
        {
            //Arrange
            var service = new SupplierService(_myRestaurantContext);

            //Act
            var result = await service.GetSuppliersAsync("", "", "", 0, 10);

            //Assert
            var supplierEnvelop = result.Should().BeAssignableTo<CollectionEnvelop<Supplier>>().Subject;
            supplierEnvelop.Items.Should().BeAssignableTo<IEnumerable<Supplier>>();
            supplierEnvelop.Items.Should().HaveCount(2);
            supplierEnvelop.TotalItems.Should().Be(2);
            supplierEnvelop.ItemsPerPage.Should().Be(10);
            supplierEnvelop.TotalPages().Should().Be(1);
        }

        [Fact]
        public async void GetSupplierAsync_Returns_Supplier()
        {
            //Arrange
            var id = 1;
            var service = new SupplierService(_myRestaurantContext);

            //Act
            var result = await service.GetSupplierAsync(d => d.Id == id);

            //Assert
            var supplier = result.Should().BeAssignableTo<Supplier>().Subject;
            supplier.Id.Should().Be(id);
            supplier.Name.Should().Be("ABC Pvt Ltd");
        }

        [Fact]
        public async void GetSupplierAsync_Returns_Null()
        {
            //Arrange
            var id = 10;
            var service = new SupplierService(_myRestaurantContext);

            //Act
            var result = await service.GetSupplierAsync(d => d.Id == id);

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async void AddSupplierAsync_Returns_New_Supplier()
        {
            //Arrange
            var service = new SupplierService(_myRestaurantContext);

            //Act
            var result = await service.AddSupplierAsync(new Supplier {
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
            });

            //Assert
            var supplier = result.Should().BeAssignableTo<Supplier>().Subject;
            supplier.Name.Should().Be("Jaffna Supplier Pvt Ltd");
            supplier.ContactPerson.Should().Be("Aathavan");
        }

        [Fact]
        public async void UpdateSupplierAsync_Successfully_Updated()
        {
            //Arrange
            var id = 2;
            var service = new SupplierService(_myRestaurantContext);

            //Act
            var dbSupplier = await service.GetSupplierAsync(d => d.Id == id);
            dbSupplier.Name = "VBT Pvt Ltd";
            dbSupplier.Address1 = "#03-46, Blk 687";
            dbSupplier.Address2 = "Hindu College Road";
            dbSupplier.City = "Jaffna";
            dbSupplier.Country = "Sri Lanka";
            dbSupplier.Telephone1 = "0777113644";
            dbSupplier.Telephone2 = "0777113644";
            dbSupplier.Fax = "0777113644";
            dbSupplier.Email = "test@test.com";
            dbSupplier.ContactPerson = "James";

            await service.UpdateSupplierAsync(dbSupplier);

            var result = await service.GetSupplierAsync(d => d.Id == id);

            //Assert
            var supplier = result.Should().BeAssignableTo<Supplier>().Subject;
            supplier.Id.Should().Be(id);
            supplier.Fax.Should().Be("0777113644");
            supplier.Telephone2.Should().Be("0777113644");
        }

        [Fact]
        public async void DeleteSupplierAsync_Successfully_Deleted()
        {
            //Arrange
            var id = 1;
            var service = new SupplierService(_myRestaurantContext);

            //Act
            var dbSupplier = await service.GetSupplierAsync(d => d.Id == id);

            await service.DeleteSupplierAsync(dbSupplier);

            var result = await service.GetSupplierAsync(d => d.Id == id);

            //Assert
            result.Should().BeNull();
        }
    }
}
