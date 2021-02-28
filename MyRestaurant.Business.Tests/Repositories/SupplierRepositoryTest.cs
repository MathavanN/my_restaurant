using FluentAssertions;
using Moq;
using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Errors;
using MyRestaurant.Business.Repositories;
using MyRestaurant.Business.Tests.Repositories.Fixtures;
using MyRestaurant.Models;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace MyRestaurant.Business.Tests.Repositories
{
    public class SupplierRepositoryTest : IClassFixture<SupplierRepositoryFixture>
    {
        private readonly SupplierRepositoryFixture _fixture;
        public SupplierRepositoryTest(SupplierRepositoryFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async void GetSuppliersAsync_Returns_GetSupplierEnvelop()
        {
            //Arrange
            _fixture.MockSupplierService.Setup(x => x.GetSuppliersAsync("", "", "", 0, 10))
                .ReturnsAsync(_fixture.CollectionEnvelop);

            var repository = new SupplierRepository(AutoMapperSingleton.Mapper, _fixture.MockSupplierService.Object);

            //Act
            var result = await repository.GetSuppliersAsync(10, 0, "", "", "");

            //Assert
            var supplierEnvelop = result.Should().BeAssignableTo<SupplierEnvelop>().Subject;
            supplierEnvelop.SupplierCount.Should().Be(2);
            supplierEnvelop.Suppliers.Should().HaveCount(2);
            supplierEnvelop.ItemsPerPage.Should().Be(10);
            supplierEnvelop.TotalPages.Should().Be(1);
        }

        [Fact]
        public async void GetSuppliersAsync_With_Empty_Paged_Params_Returns_GetSupplierEnvelop()
        {
            //Arrange
            _fixture.MockSupplierService.Setup(x => x.GetSuppliersAsync("", "", "", 0, 10))
                .ReturnsAsync(_fixture.CollectionEnvelop);

            var repository = new SupplierRepository(AutoMapperSingleton.Mapper, _fixture.MockSupplierService.Object);

            //Act
            var result = await repository.GetSuppliersAsync(null, null, "", "", "");

            //Assert
            var supplierEnvelop = result.Should().BeAssignableTo<SupplierEnvelop>().Subject;
            supplierEnvelop.SupplierCount.Should().Be(2);
            supplierEnvelop.Suppliers.Should().HaveCount(2);
            supplierEnvelop.ItemsPerPage.Should().Be(10);
            supplierEnvelop.TotalPages.Should().Be(1);
        }

        [Fact]
        public async void GetSupplierAsync_Returns_GetSupplierDto()
        {
            //Arrange
            var id = 1;
            _fixture.MockSupplierService.Setup(x => x.GetSupplierAsync(It.IsAny<Expression<Func<Supplier, bool>>>()))
                .Returns<Expression<Func<Supplier, bool>>>(expression => Task.FromResult(_fixture.Suppliers.AsQueryable().FirstOrDefault(expression)));

            var repository = new SupplierRepository(AutoMapperSingleton.Mapper, _fixture.MockSupplierService.Object);

            //Act
            var result = await repository.GetSupplierAsync(id);

            //Assert
            result.Should().BeOfType(typeof(GetSupplierDto));
            result.Id.Should().Be(id);
            result.Name.Should().Be("ABC Pvt Ltd");
            result.ContactPerson.Should().Be("James");
        }

        [Fact]
        public async void GetSupplierAsync_Throws_NotFoundException()
        {
            //Arrange
            var id = 201;
            _fixture.MockSupplierService.Setup(x => x.GetSupplierAsync(It.IsAny<Expression<Func<Supplier, bool>>>()))
                .Returns<Expression<Func<Supplier, bool>>>(expression => Task.FromResult(_fixture.Suppliers.AsQueryable().FirstOrDefault(expression)));

            var repository = new SupplierRepository(AutoMapperSingleton.Mapper, _fixture.MockSupplierService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.GetSupplierAsync(id));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound);
            exception.ErrorMessage.Should().Be("Supplier not found.");
            exception.ErrorType.Should().Be(HttpStatusCode.NotFound.ToString());
        }

        [Fact]
        public async void CreateSupplierAsync_Returns_New_GetSupplierDto()
        {
            //Arrange
            _fixture.MockSupplierService.Setup(x => x.AddSupplierAsync(It.IsAny<Supplier>()))
                .ReturnsAsync(_fixture.CreatedNewSupplier);

            var repository = new SupplierRepository(AutoMapperSingleton.Mapper, _fixture.MockSupplierService.Object);

            //Act
            var result = await repository.CreateSupplierAsync(_fixture.CreateSupplierDto);

            //Assert
            result.Should().BeOfType(typeof(GetSupplierDto));
            result.Id.Should().Be(3);
            result.Name.Should().Be(_fixture.CreateSupplierDto.Name);
            result.ContactPerson.Should().Be(_fixture.CreateSupplierDto.ContactPerson);
        }

        [Fact]
        public async void CreateSupplierAsync_Throws_ConflictException()
        {
            //Arrange
            _fixture.MockSupplierService.Setup(x => x.GetSupplierAsync(It.IsAny<Expression<Func<Supplier, bool>>>()))
                .Returns<Expression<Func<Supplier, bool>>>(expression => Task.FromResult(_fixture.Suppliers.AsQueryable().FirstOrDefault(expression)));

            var repository = new SupplierRepository(AutoMapperSingleton.Mapper, _fixture.MockSupplierService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.CreateSupplierAsync(new CreateSupplierDto {
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
            }));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.Conflict);
            exception.ErrorMessage.Should().Be("Supplier VBT Pvt Ltd is already available.");
            exception.ErrorType.Should().Be(HttpStatusCode.Conflict.ToString());
        }

        [Fact]
        public async void UpdateSupplierAsync_Returns_Updated_GetSupplierDto()
        {
            //Arrange
            long id = 2;
            _fixture.MockSupplierService.Setup(x => x.GetSupplierAsync(It.IsAny<Expression<Func<Supplier, bool>>>()))
                .Returns<Expression<Func<Supplier, bool>>>(expression => Task.FromResult(_fixture.Suppliers.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockSupplierService.Setup(x => x.UpdateSupplierAsync(It.IsAny<Supplier>()));

            var repository = new SupplierRepository(AutoMapperSingleton.Mapper, _fixture.MockSupplierService.Object);

            //Act
            var result = await repository.UpdateSupplierAsync(id, _fixture.EditSupplierDto);

            //Assert
            result.Should().BeOfType(typeof(GetSupplierDto));
            result.Id.Should().Be(id);
            result.Name.Should().Be(_fixture.EditSupplierDto.Name);
            result.Address1.Should().Be(_fixture.EditSupplierDto.Address1);
        }

        [Fact]
        public async void UpdateSupplierAsync_Throws_NotFoundException()
        {
            //Arrange
            var id = 201;
            _fixture.MockSupplierService.Setup(x => x.GetSupplierAsync(It.IsAny<Expression<Func<Supplier, bool>>>()))
                .Returns<Expression<Func<Supplier, bool>>>(expression => Task.FromResult(_fixture.Suppliers.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockSupplierService.Setup(x => x.UpdateSupplierAsync(It.IsAny<Supplier>()));

            var repository = new SupplierRepository(AutoMapperSingleton.Mapper, _fixture.MockSupplierService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.UpdateSupplierAsync(id, _fixture.EditSupplierDto));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound);
            exception.ErrorMessage.Should().Be("Supplier not found.");
            exception.ErrorType.Should().Be(HttpStatusCode.NotFound.ToString());
        }

        [Fact]
        public async void UpdateSupplierAsync_Throws_ConflictException()
        {
            //Arrange
            var id = 2;
            _fixture.MockSupplierService.Setup(x => x.GetSupplierAsync(It.IsAny<Expression<Func<Supplier, bool>>>()))
                .Returns<Expression<Func<Supplier, bool>>>(expression => Task.FromResult(_fixture.Suppliers.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockSupplierService.Setup(x => x.UpdateSupplierAsync(It.IsAny<Supplier>()));

            var repository = new SupplierRepository(AutoMapperSingleton.Mapper, _fixture.MockSupplierService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.UpdateSupplierAsync(id, new EditSupplierDto { 
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
            }));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.Conflict);
            exception.ErrorMessage.Should().Be("Supplier ABC Pvt Ltd is already available.");
            exception.ErrorType.Should().Be(HttpStatusCode.Conflict.ToString());
        }

        [Fact]
        public async void DeleteSupplierAsync_Returns_NoResult()
        {
            //Arrange
            var id = 2;
            _fixture.MockSupplierService.Setup(x => x.GetSupplierAsync(It.IsAny<Expression<Func<Supplier, bool>>>()))
                .Returns<Expression<Func<Supplier, bool>>>(expression => Task.FromResult(_fixture.Suppliers.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockSupplierService.Setup(x => x.DeleteSupplierAsync(It.IsAny<Supplier>()));

            var repository = new SupplierRepository(AutoMapperSingleton.Mapper, _fixture.MockSupplierService.Object);

            //Act
            await repository.DeleteSupplierAsync(id);

            // Assert
            _fixture.MockSupplierService.Verify(x => x.DeleteSupplierAsync(It.IsAny<Supplier>()), Times.Once);
        }

        [Fact]
        public async void DeleteSupplierAsync_Throws_NotFoundException()
        {
            //Arrange
            var id = 201;
            _fixture.MockSupplierService.Setup(x => x.GetSupplierAsync(It.IsAny<Expression<Func<Supplier, bool>>>()))
                .Returns<Expression<Func<Supplier, bool>>>(expression => Task.FromResult(_fixture.Suppliers.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockSupplierService.Setup(x => x.DeleteSupplierAsync(It.IsAny<Supplier>()));

            var repository = new SupplierRepository(AutoMapperSingleton.Mapper, _fixture.MockSupplierService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.DeleteSupplierAsync(id));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound);
            exception.ErrorMessage.Should().Be("Supplier not found.");
            exception.ErrorType.Should().Be(HttpStatusCode.NotFound.ToString());
        }
    }
}
