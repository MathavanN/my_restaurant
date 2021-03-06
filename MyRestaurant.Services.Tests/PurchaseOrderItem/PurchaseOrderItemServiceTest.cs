using FluentAssertions;
using MyRestaurant.Models;
using System.Collections.Generic;
using Xunit;

namespace MyRestaurant.Services.Tests
{
    public class PurchaseOrderItemServiceTest : MyRestaurantContextTestBase
    {

        public PurchaseOrderItemServiceTest()
        {
            PurchaseOrderItemInitializer.Initialize(_myRestaurantContext);
        }

        [Fact]
        public async void GetPurchaseOrderItemsAsync_Returns_PurchaseOrderItems()
        {
            //Arrange
            var service = new PurchaseOrderItemService(_myRestaurantContext);

            //Act
            var result = await service.GetPurchaseOrderItemsAsync(d => d.PurchaseOrderId == 1);

            //Assert
            var uoms = result.Should().BeAssignableTo<IEnumerable<PurchaseOrderItem>>().Subject;
            uoms.Should().HaveCount(2);
        }

        [Fact]
        public async void GetPurchaseOrderItemAsync_Returns_PurchaseOrderItem()
        {
            //Arrange
            var id = 1;
            var service = new PurchaseOrderItemService(_myRestaurantContext);

            //Act
            var result = await service.GetPurchaseOrderItemAsync(d => d.Id == id);

            //Assert
            var item = result.Should().BeAssignableTo<PurchaseOrderItem>().Subject;
            item.Id.Should().Be(id);
            item.Item.Name.Should().Be("Rice");
            item.PurchaseOrder.OrderNumber.Should().Be("PO_20210130_8d8c510caee6a4b");
            item.ItemUnitPrice.Should().Be(540);
            item.Quantity.Should().Be(5);
        }

        [Fact]
        public async void GetPurchaseOrderItemAsync_Returns_Null()
        {
            //Arrange
            var id = 1001;
            var service = new PurchaseOrderItemService(_myRestaurantContext);

            //Act
            var result = await service.GetPurchaseOrderItemAsync(d => d.Id == id);

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async void AddPurchaseOrderItemAsync_Returns_New_PurchaseOrderItem()
        {
            //Arrange
            var service = new PurchaseOrderItemService(_myRestaurantContext);

            //Act
            var result = await service.AddPurchaseOrderItemAsync(new PurchaseOrderItem {
                PurchaseOrderId = 2,
                ItemId = 7,
                ItemUnitPrice = 350,
                Quantity = 5
            });

            //Assert
            var item = result.Should().BeAssignableTo<PurchaseOrderItem>().Subject;
            item.Item.Name.Should().Be("Pasta");
            item.PurchaseOrder.OrderNumber.Should().Be("PO_20210130_8d8c512f7cd7920");
            item.ItemUnitPrice.Should().Be(350);
            item.Quantity.Should().Be(5);

            //Act
            var items = await service.GetPurchaseOrderItemsAsync(d => d.PurchaseOrderId == 2);

            //Assert
            items.Should().HaveCount(3);
        }

        [Fact]
        public async void UpdatePurchaseOrderItemAsync_Successfully_Updated()
        {
            //Arrange
            var id = 2;
            var service = new PurchaseOrderItemService(_myRestaurantContext);

            //Act
            var dbItem = await service.GetPurchaseOrderItemAsync(d => d.Id == id);
            dbItem.PurchaseOrderId = 2;
            dbItem.ItemId = 12;
            dbItem.ItemUnitPrice = 450;
            dbItem.Quantity = 3;

            await service.UpdatePurchaseOrderItemAsync(dbItem);

            var result = await service.GetPurchaseOrderItemAsync(d => d.Id == id);

            //Assert
            var item = result.Should().BeAssignableTo<PurchaseOrderItem>().Subject;
            item.Id.Should().Be(id);
            item.Item.Name.Should().Be("Sausages");
            item.PurchaseOrder.OrderNumber.Should().Be("PO_20210130_8d8c512f7cd7920");
            item.ItemUnitPrice.Should().Be(450);
            item.Quantity.Should().Be(3);
        }

        [Fact]
        public async void DeletePurchaseOrderItemAsync_Successfully_Deleted()
        {
            //Arrange
            var id = 1;
            var service = new PurchaseOrderItemService(_myRestaurantContext);

            //Act
            var dbPurchaseOrderItem = await service.GetPurchaseOrderItemAsync(d => d.Id == id);

            await service.DeletePurchaseOrderItemAsync(dbPurchaseOrderItem);

            var result = await service.GetPurchaseOrderItemAsync(d => d.Id == id);

            //Assert
            result.Should().BeNull();
        }
    }
}
