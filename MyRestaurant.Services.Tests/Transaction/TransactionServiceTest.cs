using FluentAssertions;
using MyRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MyRestaurant.Services.Tests
{
    public class TransactionServiceTest : MyRestaurantContextTestBase
    {
        public TransactionServiceTest()
        {
            TransactionInitializer.Initialize(_myRestaurantContext);
        }

        [Fact]
        public async Task GetTransactionsAsync_Returns_Transactions()
        {
            //Arrange
            var service = new TransactionService(_myRestaurantContext);

            //Act
            var result = await service.GetTransactionsAsync();

            //Assert
            result.Should().BeAssignableTo<IEnumerable<Transaction>>();
            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetTransactionAsync_Returns_Transaction()
        {
            //Arrange
            var id = 1;
            var service = new TransactionService(_myRestaurantContext);

            //Act
            var result = await service.GetTransactionAsync(d => d.Id == id);

            //Assert
            result.Should().BeAssignableTo<Transaction>();
            result.Id.Should().Be(id);
            result.TransactionTypeId.Should().Be(1);
            result.PaymentTypeId.Should().Be(2);
        }

        [Fact]
        public async Task GetTransactionTypeAsync_Returns_Null()
        {
            //Arrange
            var id = 1001;
            var service = new TransactionService(_myRestaurantContext);

            //Act
            var result = await service.GetTransactionAsync(d => d.Id == id);

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task AddTransactionTypeAsync_Returns_New_TransactionType()
        {
            //Arrange
            var service = new TransactionService(_myRestaurantContext);

            //Act
            var result = await service.AddTransactionAsync(new Transaction
            {
                TransactionTypeId = 10,
                PaymentTypeId = 1,
                Date = DateTime.Now.AddDays(-2),
                Description = "Interest from Deposit",
                Amount = 456.5m,
                Cashflow = Cashflow.Income,
                CreatedAt = DateTime.Now
            });

            //Assert
            result.Should().BeAssignableTo<Transaction>();
            result.TransactionTypeId.Should().Be(10);
            result.TransactionType.Type.Should().Be("Extra Income");
            result.PaymentTypeId.Should().Be(1);
            result.PaymentType.Name.Should().Be("Cash");
            result.Cashflow.Should().Be(Cashflow.Income);

            //Act
            var transactionTypes = await service.GetTransactionsAsync();

            //Assert
            transactionTypes.Should().HaveCount(3);
        }

        [Fact]
        public async Task UpdateTransactionTypeAsync_Successfully_Updated()
        {
            //Arrange
            var id = 2;
            var service = new TransactionService(_myRestaurantContext);

            //Act
            var dbTransaction = await service.GetTransactionAsync(d => d.Id == id);
            dbTransaction.Amount = 567.5m;

            await service.UpdateTransactionAsync(dbTransaction);

            var result = await service.GetTransactionAsync(d => d.Id == id);

            //Assert
            result.Should().BeAssignableTo<Transaction>();
            result.Id.Should().Be(id);
            result.Amount.Should().Be(567.5m);
        }

        [Fact]
        public async Task DeleteTransactionTypeAsync_Successfully_Deleted()
        {
            //Arrange
            var id = 1;
            var service = new TransactionService(_myRestaurantContext);

            //Act
            var dbTransactionType = await service.GetTransactionAsync(d => d.Id == id);

            await service.DeleteTransactionAsync(dbTransactionType);

            var result = await service.GetTransactionAsync(d => d.Id == id);

            //Assert
            result.Should().BeNull();
        }
    }
}
