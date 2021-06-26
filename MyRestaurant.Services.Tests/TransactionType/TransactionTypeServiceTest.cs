using FluentAssertions;
using MyRestaurant.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MyRestaurant.Services.Tests
{
    public class TransactionTypeServiceTest : MyRestaurantContextTestBase
    {
        public TransactionTypeServiceTest()
        {
            TransactionTypeInitializer.Initialize(_myRestaurantContext);
        }

        [Fact]
        public async Task GetTransactionTypesAsync_Returns_TransactionTypes()
        {
            //Arrange
            var service = new TransactionTypeService(_myRestaurantContext);

            //Act
            var result = await service.GetTransactionTypesAsync();

            //Assert
            result.Should().BeAssignableTo<IEnumerable<TransactionType>>();
            result.Should().HaveCount(17);
        }

        [Fact]
        public async Task GetTransactionTypeAsync_Returns_TransactionType()
        {
            //Arrange
            var id = 1;
            var service = new TransactionTypeService(_myRestaurantContext);

            //Act
            var result = await service.GetTransactionTypeAsync(d => d.Id == id);

            //Assert
            result.Should().BeAssignableTo<TransactionType>();
            result.Id.Should().Be(id);
            result.Type.Should().Be("Food");
        }

        [Fact]
        public async Task GetTransactionTypeAsync_Returns_Null()
        {
            //Arrange
            var id = 1001;
            var service = new TransactionTypeService(_myRestaurantContext);

            //Act
            var result = await service.GetTransactionTypeAsync(d => d.Id == id);

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task AddTransactionTypeAsync_Returns_New_TransactionType()
        {
            //Arrange
            var service = new TransactionTypeService(_myRestaurantContext);

            //Act
            var result = await service.AddTransactionTypeAsync(new TransactionType { Type = "Salary" });

            //Assert
            result.Should().BeAssignableTo<TransactionType>();
            result.Type.Should().Be("Salary");

            //Act
            var transactionTypes = await service.GetTransactionTypesAsync();

            //Assert
            transactionTypes.Should().HaveCount(18);
        }

        [Fact]
        public async Task UpdateTransactionTypeAsync_Successfully_Updated()
        {
            //Arrange
            var id = 4;
            var service = new TransactionTypeService(_myRestaurantContext);

            //Act
            var dbTransactionType = await service.GetTransactionTypeAsync(d => d.Id == id);
            dbTransactionType.Type = "Mortgage/Rent";

            await service.UpdateTransactionTypeAsync(dbTransactionType);

            var result = await service.GetTransactionTypeAsync(d => d.Id == id);

            //Assert
            result.Should().BeAssignableTo<TransactionType>();
            result.Id.Should().Be(id);
            result.Type.Should().Be("Mortgage/Rent");
        }

        [Fact]
        public async Task DeleteTransactionTypeAsync_Successfully_Deleted()
        {
            //Arrange
            var id = 1;
            var service = new TransactionTypeService(_myRestaurantContext);

            //Act
            var dbTransactionType = await service.GetTransactionTypeAsync(d => d.Id == id);

            await service.DeleteTransactionTypeAsync(dbTransactionType);

            var result = await service.GetTransactionTypeAsync(d => d.Id == id);

            //Assert
            result.Should().BeNull();
        }
    }
}
