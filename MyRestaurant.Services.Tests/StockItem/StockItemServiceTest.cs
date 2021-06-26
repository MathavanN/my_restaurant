using FluentAssertions;
using MyRestaurant.Models;
using MyRestaurant.Services.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        public async Task GetStockItemsAsync_Returns_StockItems()
        {
            //Arrange
            var service = new StockItemService(_myRestaurantContext);

            //Act
            var result = await service.GetStockItemsAsync();

            //Assert
            result.Should().BeAssignableTo<IEnumerable<StockItem>>();
            result.Should().HaveCount(29);
        }

        [Fact]
        public async Task GetStockItemsAsync_Returns_First_Paged_StockItems()
        {
            //Arrange
            var service = new StockItemService(_myRestaurantContext);

            //Act
            var result = await service.GetStockItemsAsync(d => d.TypeId == 1, 0, 10);

            //Assert
            result.Should().BeAssignableTo<CollectionEnvelop<StockItem>>();
            result.Items.Should().BeAssignableTo<IEnumerable<StockItem>>();
            result.Items.Should().HaveCount(10);
            result.TotalItems.Should().Be(16);
            result.ItemsPerPage.Should().Be(10);
            result.TotalPages().Should().Be(2);
        }

        [Fact]
        public async Task GetStockItemsAsync_Returns_Next_Paged_StockItems()
        {
            //Arrange
            var service = new StockItemService(_myRestaurantContext);

            //Act
            var result = await service.GetStockItemsAsync(d => d.TypeId == 1, 1, 10);

            //Assert
            result.Should().BeAssignableTo<CollectionEnvelop<StockItem>>();
            result.Items.Should().BeAssignableTo<IEnumerable<StockItem>>();
            result.Items.Should().HaveCount(6);
            result.TotalItems.Should().Be(16);
            result.ItemsPerPage.Should().Be(10);
            result.TotalPages().Should().Be(2);
        }

        [Fact]
        public async Task GetStockItemAsync_Returns_StockItem()
        {
            //Arrange
            var id = 1;
            var service = new StockItemService(_myRestaurantContext);

            //Act
            var result = await service.GetStockItemAsync(d => d.Id == id);

            //Assert
            result.Should().BeAssignableTo<StockItem>();
            result.Id.Should().Be(id);
            result.Name.Should().Be("Rice");
            result.Type.Type.Should().Be("Grocery");
        }

        [Fact]
        public async Task GetStockItemAsync_Returns_Null()
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
        public async Task AddStockItemAsync_Returns_New_StockItem()
        {
            //Arrange
            var service = new StockItemService(_myRestaurantContext);

            //Act
            var result = await service.AddStockItemAsync(new StockItem
            {
                Name = "Cream Soda",
                TypeId = 2,
                UnitOfMeasureId = 3,
                ItemUnit = 300
            });

            //Assert
            result.Should().BeAssignableTo<StockItem>();
            result.Name.Should().Be("Cream Soda");
            result.UnitOfMeasure.Code.Should().Be("ml");

            //Act
            var stockItems = await service.GetStockItemsAsync();

            //Assert
            stockItems.Should().HaveCount(30);
        }

        [Fact]
        public async Task UpdateStockItemAsync_Successfully_Updated()
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
            result.Should().BeAssignableTo<StockItem>();
            result.Id.Should().Be(id);
            result.ItemUnit.Should().Be(10);
            result.Description.Should().Be("10kg bag");
        }

        [Fact]
        public async Task DeleteStockItemAsync_Successfully_Deleted()
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
