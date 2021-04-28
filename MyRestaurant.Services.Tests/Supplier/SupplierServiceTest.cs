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
        public async void GetSuppliersAsync_Returns_First_Paged_Suppliers()
        {
            //Arrange
            var service = new SupplierService(_myRestaurantContext);

            //Act
            var result = await service.GetSuppliersAsync("", "", "", 0, 10);

            //Assert
            result.Should().BeAssignableTo<CollectionEnvelop<Supplier>>();
            result.Items.Should().BeAssignableTo<IEnumerable<Supplier>>();
            result.Items.Should().HaveCount(10);
            result.TotalItems.Should().Be(27);
            result.ItemsPerPage.Should().Be(10);
            result.TotalPages().Should().Be(3);
        }

        [Fact]
        public async void GetSuppliersAsync_Returns_Next_Paged_Suppliers()
        {
            //Arrange
            var service = new SupplierService(_myRestaurantContext);

            //Act
            var result = await service.GetSuppliersAsync("", "", "", 1, 10);

            //Assert
            result.Should().BeAssignableTo<CollectionEnvelop<Supplier>>();
            result.Items.Should().BeAssignableTo<IEnumerable<Supplier>>();
            result.Items.Should().HaveCount(10);
            result.TotalItems.Should().Be(27);
            result.ItemsPerPage.Should().Be(10);
            result.TotalPages().Should().Be(3);
        }

        [Fact]
        public async void GetSuppliersAsync_By_Name_Returns_Suppliers()
        {
            //Arrange
            var service = new SupplierService(_myRestaurantContext);

            //Act
            var result = await service.GetSuppliersAsync("Dairy", "", "", 0, 10);

            //Assert
            result.Should().BeAssignableTo<CollectionEnvelop<Supplier>>();
            result.Items.Should().BeAssignableTo<IEnumerable<Supplier>>();
            result.Items.Should().HaveCount(1);
            result.TotalItems.Should().Be(1);
            result.ItemsPerPage.Should().Be(10);
            result.TotalPages().Should().Be(1);
        }

        [Fact]
        public async void GetSuppliersAsync_By_City_Returns_Suppliers()
        {
            //Arrange
            var service = new SupplierService(_myRestaurantContext);

            //Act
            var result = await service.GetSuppliersAsync("", "Charleston", "", 0, 10);

            //Assert
            result.Should().BeAssignableTo<CollectionEnvelop<Supplier>>();
            result.Items.Should().BeAssignableTo<IEnumerable<Supplier>>();
            result.Items.Should().HaveCount(2);
            result.TotalItems.Should().Be(2);
            result.ItemsPerPage.Should().Be(10);
            result.TotalPages().Should().Be(1);
        }

        [Fact]
        public async void GetSuppliersAsync_By_Contact_Person_Returns_Suppliers()
        {
            //Arrange
            var service = new SupplierService(_myRestaurantContext);

            //Act
            var result = await service.GetSuppliersAsync("", "", "Emmalee", 0, 10);

            //Assert
            result.Should().BeAssignableTo<CollectionEnvelop<Supplier>>();
            result.Items.Should().BeAssignableTo<IEnumerable<Supplier>>();
            result.Items.Should().HaveCount(1);
            result.TotalItems.Should().Be(1);
            result.ItemsPerPage.Should().Be(10);
            result.TotalPages().Should().Be(1);
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
            result.Should().BeAssignableTo<Supplier>();
            result.Id.Should().Be(id);
            result.Name.Should().Be("ABC Pvt Ltd");
        }

        [Fact]
        public async void GetSupplierAsync_Returns_Null()
        {
            //Arrange
            var id = 10001;
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
            var result = await service.AddSupplierAsync(new Supplier
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
            });

            //Assert
            result.Should().BeAssignableTo<Supplier>();
            result.Name.Should().Be("Jaffna Supplier Pvt Ltd");
            result.ContactPerson.Should().Be("Aathavan");
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
            result.Should().BeAssignableTo<Supplier>();
            result.Id.Should().Be(id);
            result.Fax.Should().Be("0777113644");
            result.Telephone2.Should().Be("0777113644");
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
