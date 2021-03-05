using FluentAssertions;
using MyRestaurant.Models;
using System.Collections.Generic;
using Xunit;

namespace MyRestaurant.Services.Tests
{
    public class StockItemServiceTest : MyRestaurantContextTestBase
    {
        public StockItemServiceTest()
        {
            StockItemInitializer.Initialize(_myRestaurantContext);
        }

        [Fact]
        public async void GetStockItemsAsync_Returns_StockItems()
        {
            //Arrange
            var service = new StockItemService(_myRestaurantContext);

            //Act
            var result = await service.GetStockItemsAsync();

            //Assert
            var stockItems = result.Should().BeAssignableTo<IEnumerable<StockItem>>().Subject;
            stockItems.Should().HaveCount(4);
        }

        [Fact]
        public async void GetStockItemAsync_Returns_StockItem()
        {
            //Arrange
            var id = 1;
            var service = new StockItemService(_myRestaurantContext);

            //Act
            var result = await service.GetStockItemAsync(d => d.Id == id);

            //Assert
            var StockItem = result.Should().BeAssignableTo<StockItem>().Subject;
            StockItem.Id.Should().Be(id);
            StockItem.Name.Should().Be("Rice");
            StockItem.Type.Type.Should().Be("Grocery");
        }

        [Fact]
        public async void GetStockItemAsync_Returns_Null()
        {
            //Arrange
            var id = 10;
            var service = new StockItemService(_myRestaurantContext);

            //Act
            var result = await service.GetStockItemAsync(d => d.Id == id);

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async void AddStockItemAsync_Returns_New_StockItem()
        {
            //Arrange
            var service = new StockItemService(_myRestaurantContext);

            //Act
            var result = await service.AddStockItemAsync(new StockItem {
                Name = "Cream Soda",
                TypeId = 2,
                UnitOfMeasureId = 4,
                ItemUnit = 300
            });

            //Assert
            var stockItem = result.Should().BeAssignableTo<StockItem>().Subject;
            stockItem.Name.Should().Be("Cream Soda");
            stockItem.UnitOfMeasure.Code.Should().Be("ml");

            //Act
            var stockItems = await service.GetStockItemsAsync();

            //Assert
            stockItems.Should().HaveCount(5);
        }

        [Fact]
        public async void UpdateStockItemAsync_Successfully_Updated()
        {
            //Arrange
            var id = 1;
            var service = new StockItemService(_myRestaurantContext);

            //Act
            var dbStockItem = await service.GetStockItemAsync(d => d.Id == id);
            dbStockItem.Name = "Rice";
            dbStockItem.ItemUnit = 10;
            dbStockItem.UnitOfMeasureId = 1;
            dbStockItem.Description = "10kg bag";

            await service.UpdateStockItemAsync(dbStockItem);

            var result = await service.GetStockItemAsync(d => d.Id == id);

            //Assert
            var stockItem = result.Should().BeAssignableTo<StockItem>().Subject;
            stockItem.Id.Should().Be(id);
            stockItem.ItemUnit.Should().Be(10);
            stockItem.Description.Should().Be("10kg bag");
        }

        [Fact]
        public async void DeleteStockItemAsync_Successfully_Deleted()
        {
            //Arrange
            var id = 1;
            var service = new StockItemService(_myRestaurantContext);

            //Act
            var dbStockItem = await service.GetStockItemAsync(d => d.Id == id);

            await service.DeleteStockItemAsync(dbStockItem);

            var result = await service.GetStockItemAsync(d => d.Id == id);

            //Assert
            result.Should().BeNull();
        }
    }
}
