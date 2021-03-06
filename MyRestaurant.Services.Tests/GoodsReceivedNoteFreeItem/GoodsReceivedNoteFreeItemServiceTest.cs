using FluentAssertions;
using MyRestaurant.Models;
using System.Collections.Generic;
using Xunit;

namespace MyRestaurant.Services.Tests
{
    public class GoodsReceivedNoteFreeItemServiceTest : MyRestaurantContextTestBase
    {
        public GoodsReceivedNoteFreeItemServiceTest()
        {
            GoodsReceivedNoteFreeItemInitializer.Initialize(_myRestaurantContext);
        }

        [Fact]
        public async void GetGoodsReceivedNoteFreeItemsAsync_Returns_GoodsReceivedNoteFreeItems()
        {
            //Arrange
            var service = new GoodsReceivedNoteFreeItemService(_myRestaurantContext);

            //Act
            var result = await service.GetGoodsReceivedNoteFreeItemsAsync(d => d.GoodsReceivedNoteId == 1);

            //Assert
            var items = result.Should().BeAssignableTo<IEnumerable<GoodsReceivedNoteFreeItem>>().Subject;
            items.Should().HaveCount(1);
        }

        [Fact]
        public async void GetGoodsReceivedNoteFreeItemAsync_Returns_GoodsReceivedNoteFreeItem()
        {
            //Arrange
            var id = 1;
            var service = new GoodsReceivedNoteFreeItemService(_myRestaurantContext);

            //Act
            var result = await service.GetGoodsReceivedNoteFreeItemAsync(d => d.Id == id);

            //Assert
            var item = result.Should().BeAssignableTo<GoodsReceivedNoteFreeItem>().Subject;
            item.Id.Should().Be(id);
            item.Item.Name.Should().Be("Chips");
            item.Discount.Should().Be(0.1m);
        }

        [Fact]
        public async void GetGoodsReceivedNoteFreeItemAsync_Returns_Null()
        {
            //Arrange
            var id = 10001;
            var service = new GoodsReceivedNoteFreeItemService(_myRestaurantContext);

            //Act
            var result = await service.GetGoodsReceivedNoteFreeItemAsync(d => d.Id == id);

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async void AddGoodsReceivedNoteFreeItemAsync_Returns_New_GoodsReceivedNoteFreeItem()
        {
            //Arrange
            var goodsReceivedNoteId = 2;
            var service = new GoodsReceivedNoteFreeItemService(_myRestaurantContext);

            //Act
            var result = await service.AddGoodsReceivedNoteFreeItemAsync(new GoodsReceivedNoteFreeItem
            {
                GoodsReceivedNoteId = goodsReceivedNoteId,
                ItemId = 27,
                ItemUnitPrice = 350,
                Quantity = 2,
                Nbt = 0.1m,
                Vat = 0.1m,
                Discount = 0.1m
            });

            //Assert
            var item = result.Should().BeAssignableTo<GoodsReceivedNoteFreeItem>().Subject;
            item.Item.Name.Should().Be("Honey");
            item.Discount.Should().Be(0.1m);
            item.Quantity.Should().Be(2);

            //Act
            var items = await service.GetGoodsReceivedNoteFreeItemsAsync(d => d.GoodsReceivedNoteId == goodsReceivedNoteId);

            //Assert
            items.Should().HaveCount(2);
        }

        [Fact]
        public async void UpdateGoodsReceivedNoteFreeItemAsync_Successfully_Updated()
        {
            //Arrange
            var goodsReceivedNoteId = 1;
            var service = new GoodsReceivedNoteFreeItemService(_myRestaurantContext);

            //Act
            var dbItem = await service.GetGoodsReceivedNoteFreeItemAsync(d => d.GoodsReceivedNoteId == goodsReceivedNoteId);
            dbItem.ItemId = 8;
            dbItem.ItemUnitPrice = 350;
            dbItem.Quantity = 2;

            await service.UpdateGoodsReceivedNoteFreeItemAsync(dbItem);

            var result = await service.GetGoodsReceivedNoteFreeItemAsync(d => d.GoodsReceivedNoteId == goodsReceivedNoteId);

            //Assert
            var item = result.Should().BeAssignableTo<GoodsReceivedNoteFreeItem>().Subject;
            item.Item.Name.Should().Be("Chips");
            item.Discount.Should().Be(0.1m);
            item.Quantity.Should().Be(2);
            item.ItemUnitPrice.Should().Be(350);
        }

        [Fact]
        public async void DeleteGoodsReceivedNoteFreeItemAsync_Successfully_Deleted()
        {
            //Arrange
            var id = 1;
            var service = new GoodsReceivedNoteFreeItemService(_myRestaurantContext);

            //Act
            var dbGRNFreeItem = await service.GetGoodsReceivedNoteFreeItemAsync(d => d.Id == id);

            await service.DeleteGoodsReceivedNoteFreeItemAsync(dbGRNFreeItem);

            var result = await service.GetGoodsReceivedNoteFreeItemAsync(d => d.Id == id);

            //Assert
            result.Should().BeNull();
        }
    }
}
