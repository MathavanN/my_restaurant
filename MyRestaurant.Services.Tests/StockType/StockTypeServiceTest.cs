using FluentAssertions;
using MyRestaurant.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        public async Task GetStockTypesAsync_Returns_StockTypes()
        {
            //Arrange
            var service = new StockTypeService(_myRestaurantContext);

            //Act
            var result = await service.GetStockTypesAsync();

            //Assert
            result.Should().BeAssignableTo<IEnumerable<StockType>>();
            result.Should().HaveCount(3);
        }

        [Fact]
        public async Task GetStockTypeAsync_Returns_StockType()
        {
            //Arrange
            var id = 1;
            var service = new StockTypeService(_myRestaurantContext);

            //Act
            var result = await service.GetStockTypeAsync(d => d.Id == id);

            //Assert
            result.Should().BeAssignableTo<StockType>();
            result!.Id.Should().Be(id);
            result.Type.Should().Be("Grocery");
            result.Description.Should().Be("");
        }

        [Fact]
        public async Task GetStockTypeAsync_Returns_Null()
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
        public async Task AddStockTypeAsync_Returns_New_StockType()
        {
            //Arrange
            var service = new StockTypeService(_myRestaurantContext);

            //Act
            var result = await service.AddStockTypeAsync(new StockType { Type = "Office", Description = "" });

            //Assert
            result.Should().BeAssignableTo<StockType>();
            result.Type.Should().Be("Office");
            result.Description.Should().Be("");

            //Act
            var stockTypes = await service.GetStockTypesAsync();

            //Assert
            stockTypes.Should().HaveCount(4);
        }

        [Fact]
        public async Task UpdateStockTypeAsync_Successfully_Updated()
        {
            //Arrange
            var id = 2;
            var service = new StockTypeService(_myRestaurantContext);

            //Act
            var dbStockType = await service.GetStockTypeAsync(d => d.Id == id);
            dbStockType!.Type = "Beverage";
            dbStockType.Description = "Drinks items";

            await service.UpdateStockTypeAsync(dbStockType);

            var result = await service.GetStockTypeAsync(d => d.Id == id);

            //Assert
            result.Should().BeAssignableTo<StockType>();
            result!.Id.Should().Be(id);
            result.Type.Should().Be("Beverage");
            result.Description.Should().Be("Drinks items");
        }

        [Fact]
        public async Task DeleteStockTypeAsync_Successfully_Deleted()
        {
            //Arrange
            var id = 1;
            var service = new StockTypeService(_myRestaurantContext);

            //Act
            var dbStockType = await service.GetStockTypeAsync(d => d.Id == id);

            await service.DeleteStockTypeAsync(dbStockType!);

            var result = await service.GetStockTypeAsync(d => d.Id == id);

            //Assert
            result.Should().BeNull();
        }
    }
}
