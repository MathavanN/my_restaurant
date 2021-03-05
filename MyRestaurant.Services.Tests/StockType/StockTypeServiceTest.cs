using FluentAssertions;
using MyRestaurant.Models;
using System.Collections.Generic;
using Xunit;

namespace MyRestaurant.Services.Tests
{
    public class StockTypeServiceTest : MyRestaurantContextTestBase
    {
        public StockTypeServiceTest()
        {
            StockTypeInitializer.Initialize(_myRestaurantContext);
        }

        [Fact]
        public async void GetStockTypesAsync_Returns_StockTypes()
        {
            //Arrange
            var service = new StockTypeService(_myRestaurantContext);

            //Act
            var result = await service.GetStockTypesAsync();

            //Assert
            var stockTypes = result.Should().BeAssignableTo<IEnumerable<StockType>>().Subject;
            stockTypes.Should().HaveCount(2);
        }

        [Fact]
        public async void GetStockTypeAsync_Returns_StockType()
        {
            //Arrange
            var id = 1;
            var service = new StockTypeService(_myRestaurantContext);

            //Act
            var result = await service.GetStockTypeAsync(d => d.Id == id);

            //Assert
            var stockType = result.Should().BeAssignableTo<StockType>().Subject;
            stockType.Id.Should().Be(id);
            stockType.Type.Should().Be("Grocery");
            stockType.Description.Should().Be("");
        }

        [Fact]
        public async void GetStockTypeAsync_Returns_Null()
        {
            //Arrange
            var id = 10;
            var service = new StockTypeService(_myRestaurantContext);

            //Act
            var result = await service.GetStockTypeAsync(d => d.Id == id);

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async void AddStockTypeAsync_Returns_New_StockType()
        {
            //Arrange
            var service = new StockTypeService(_myRestaurantContext);

            //Act
            var result = await service.AddStockTypeAsync(new StockType { Type = "Office", Description = "" });

            //Assert
            var stockType = result.Should().BeAssignableTo<StockType>().Subject;
            stockType.Type.Should().Be("Office");
            stockType.Description.Should().Be("");

            //Act
            var stockTypes = await service.GetStockTypesAsync();

            //Assert
            stockTypes.Should().HaveCount(3);
        }

        [Fact]
        public async void UpdateStockTypeAsync_Successfully_Updated()
        {
            //Arrange
            var id = 2;
            var service = new StockTypeService(_myRestaurantContext);

            //Act
            var dbStockType = await service.GetStockTypeAsync(d => d.Id == id);
            dbStockType.Type = "Beverage";
            dbStockType.Description = "Drinks items";

            await service.UpdateStockTypeAsync(dbStockType);

            var result = await service.GetStockTypeAsync(d => d.Id == id);

            //Assert
            var stockType = result.Should().BeAssignableTo<StockType>().Subject;
            stockType.Id.Should().Be(id);
            stockType.Type.Should().Be("Beverage");
            stockType.Description.Should().Be("Drinks items");
        }

        [Fact]
        public async void DeleteStockTypeAsync_Successfully_Deleted()
        {
            //Arrange
            var id = 1;
            var service = new StockTypeService(_myRestaurantContext);

            //Act
            var dbStockType = await service.GetStockTypeAsync(d => d.Id == id);

            await service.DeleteStockTypeAsync(dbStockType);

            var result = await service.GetStockTypeAsync(d => d.Id == id);

            //Assert
            result.Should().BeNull();
        }
    }
}
