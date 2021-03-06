using FluentAssertions;
using MyRestaurant.Models;
using System.Collections.Generic;
using Xunit;

namespace MyRestaurant.Services.Tests
{
    public class GoodsReceivedNoteItemServiceTest : MyRestaurantContextTestBase
    {
        public GoodsReceivedNoteItemServiceTest()
        {
            GoodsReceivedNoteItemInitializer.Initialize(_myRestaurantContext);
        }

        [Fact]
        public async void GetGoodsReceivedNoteItemsAsync_Returns_GoodsReceivedNoteItems()
        {
            //Arrange
            var service = new GoodsReceivedNoteItemService(_myRestaurantContext);

            //Act
            var result = await service.GetGoodsReceivedNoteItemsAsync(d => d.GoodsReceivedNoteId == 1);

            //Assert
            var items = result.Should().BeAssignableTo<IEnumerable<GoodsReceivedNoteItem>>().Subject;
            items.Should().HaveCount(2);
        }

        [Fact]
        public async void GetGoodsReceivedNoteItemAsync_Returns_GoodsReceivedNoteItem()
        {
            //Arrange
            var id = 1;
            var service = new GoodsReceivedNoteItemService(_myRestaurantContext);

            //Act
            var result = await service.GetGoodsReceivedNoteItemAsync(d => d.Id == id);

            //Assert
            var item = result.Should().BeAssignableTo<GoodsReceivedNoteItem>().Subject;
            item.Id.Should().Be(id);
            item.Item.Name.Should().Be("Rice");
            item.Discount.Should().Be(0.1m);
        }

        [Fact]
        public async void GetGoodsReceivedNoteItemAsync_Returns_Null()
        {
            //Arrange
            var id = 10;
            var service = new GoodsReceivedNoteItemService(_myRestaurantContext);

            //Act
            var result = await service.GetGoodsReceivedNoteItemAsync(d => d.Id == id);

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async void AddGoodsReceivedNoteItemAsync_Returns_New_GoodsReceivedNoteItem()
        {
            //Arrange
            var goodsReceivedNoteId = 2;
            var service = new GoodsReceivedNoteItemService(_myRestaurantContext);

            //Act
            var result = await service.AddGoodsReceivedNoteItemAsync(new GoodsReceivedNoteItem {
                GoodsReceivedNoteId = goodsReceivedNoteId,
                ItemId = 8,
                ItemUnitPrice = 350,
                Quantity = 5
            });

            //Assert
            var item = result.Should().BeAssignableTo<GoodsReceivedNoteItem>().Subject;
            item.Item.Name.Should().Be("Chips");
            item.ItemUnitPrice.Should().Be(350);
            item.Nbt.Should().Be(0);

            //Act
            var items = await service.GetGoodsReceivedNoteItemsAsync(d => d.GoodsReceivedNoteId == goodsReceivedNoteId);

            //Assert
            items.Should().HaveCount(3);
        }

        [Fact]
        public async void UpdateGoodsReceivedNoteItemAsync_Successfully_Updated()
        {
            //Arrange
            var id = 3;
            var service = new GoodsReceivedNoteItemService(_myRestaurantContext);

            //Act
            var dbItem = await service.GetGoodsReceivedNoteItemAsync(d => d.Id == id);
            dbItem.ItemUnitPrice = 50;
            dbItem.Quantity = 30;
            dbItem.Nbt = 0.5m;

            await service.UpdateGoodsReceivedNoteItemAsync(dbItem);

            var result = await service.GetGoodsReceivedNoteItemAsync(d => d.Id == id);

            //Assert
            var item = result.Should().BeAssignableTo<GoodsReceivedNoteItem>().Subject;
            item.Id.Should().Be(id);
            item.ItemUnitPrice.Should().Be(50);
            item.Quantity.Should().Be(30);
            item.Nbt.Should().Be(0.5m);
        }

        [Fact]
        public async void DeleteGoodsReceivedNoteItemAsync_Successfully_Deleted()
        {
            //Arrange
            var id = 1;
            var service = new GoodsReceivedNoteItemService(_myRestaurantContext);

            //Act
            var dbGRNItem = await service.GetGoodsReceivedNoteItemAsync(d => d.Id == id);

            await service.DeleteGoodsReceivedNoteItemAsync(dbGRNItem);

            var result = await service.GetGoodsReceivedNoteItemAsync(d => d.Id == id);

            //Assert
            result.Should().BeNull();
        }
    }
}
