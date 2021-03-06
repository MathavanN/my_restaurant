using FluentAssertions;
using MyRestaurant.Models;
using MyRestaurant.Services.Common;
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
            stockItems.Should().HaveCount(29);
        }

        [Fact]
        public async void GetStockItemsAsync_Returns_First_Paged_StockItems()
        {
            //Arrange
            var service = new StockItemService(_myRestaurantContext);

            //Act
            var result = await service.GetStockItemsAsync(d => d.TypeId == 1, 0, 10);

            //Assert
            var stockItemEnvelop = result.Should().BeAssignableTo<CollectionEnvelop<StockItem>>().Subject;
            stockItemEnvelop.Items.Should().BeAssignableTo<IEnumerable<StockItem>>();
            stockItemEnvelop.Items.Should().HaveCount(10);
            stockItemEnvelop.TotalItems.Should().Be(16);
            stockItemEnvelop.ItemsPerPage.Should().Be(10);
            stockItemEnvelop.TotalPages().Should().Be(2);
        }

        [Fact]
        public async void GetStockItemsAsync_Returns_Next_Paged_StockItems()
        {
            //Arrange
            var service = new StockItemService(_myRestaurantContext);

            //Act
            var result = await service.GetStockItemsAsync(d => d.TypeId == 1, 1, 10);

            //Assert
            var stockItemEnvelop = result.Should().BeAssignableTo<CollectionEnvelop<StockItem>>().Subject;
            stockItemEnvelop.Items.Should().BeAssignableTo<IEnumerable<StockItem>>();
            stockItemEnvelop.Items.Should().HaveCount(6);
            stockItemEnvelop.TotalItems.Should().Be(16);
            stockItemEnvelop.ItemsPerPage.Should().Be(10);
            stockItemEnvelop.TotalPages().Should().Be(2);
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
            var stockItem = result.Should().BeAssignableTo<StockItem>().Subject;
            stockItem.Id.Should().Be(id);
            stockItem.Name.Should().Be("Rice");
            stockItem.Type.Type.Should().Be("Grocery");
        }

        [Fact]
        public async void GetStockItemAsync_Returns_Null()
        {
            //Arrange
            var id = 10001;
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
                UnitOfMeasureId = 3,
                ItemUnit = 300
            });

            //Assert
            var stockItem = result.Should().BeAssignableTo<StockItem>().Subject;
            stockItem.Name.Should().Be("Cream Soda");
            stockItem.UnitOfMeasure.Code.Should().Be("ml");

            //Act
            var stockItems = await service.GetStockItemsAsync();

            //Assert
            stockItems.Should().HaveCount(30);
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
