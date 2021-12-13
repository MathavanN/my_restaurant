using FluentAssertions;
using MyRestaurant.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        public async Task GetGoodsReceivedNoteItemsAsync_Returns_GoodsReceivedNoteItems()
        {
            //Arrange
            var service = new GoodsReceivedNoteItemService(_myRestaurantContext);

            //Act
            var result = await service.GetGoodsReceivedNoteItemsAsync(d => d.GoodsReceivedNoteId == 1);

            //Assert
            result.Should().BeAssignableTo<IEnumerable<GoodsReceivedNoteItem>>();
            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetGoodsReceivedNoteItemAsync_Returns_GoodsReceivedNoteItem()
        {
            //Arrange
            var id = 1;
            var service = new GoodsReceivedNoteItemService(_myRestaurantContext);

            //Act
            var result = await service.GetGoodsReceivedNoteItemAsync(d => d.Id == id);

            //Assert
            result.Should().BeAssignableTo<GoodsReceivedNoteItem>();
            result!.GoodsReceivedNote.Should().BeAssignableTo<GoodsReceivedNote>();
            result.Id.Should().Be(id);
            result.Item.Name.Should().Be("Rice");
            result.Discount.Should().Be(0.1m);
        }

        [Fact]
        public async Task GetGoodsReceivedNoteItemAsync_Returns_Null()
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
        public async Task AddGoodsReceivedNoteItemAsync_Returns_New_GoodsReceivedNoteItem()
        {
            //Arrange
            var goodsReceivedNoteId = 2;
            var service = new GoodsReceivedNoteItemService(_myRestaurantContext);

            //Act
            var result = await service.AddGoodsReceivedNoteItemAsync(new GoodsReceivedNoteItem
            {
                GoodsReceivedNoteId = goodsReceivedNoteId,
                ItemId = 8,
                ItemUnitPrice = 350,
                Quantity = 5
            });

            //Assert
            result.Should().BeAssignableTo<GoodsReceivedNoteItem>();
            result.Item.Name.Should().Be("Chips");
            result.ItemUnitPrice.Should().Be(350);
            result.Nbt.Should().Be(0);

            //Act
            var items = await service.GetGoodsReceivedNoteItemsAsync(d => d.GoodsReceivedNoteId == goodsReceivedNoteId);

            //Assert
            items.Should().HaveCount(3);
        }

        [Fact]
        public async Task UpdateGoodsReceivedNoteItemAsync_Successfully_Updated()
        {
            //Arrange
            var id = 3;
            var service = new GoodsReceivedNoteItemService(_myRestaurantContext);

            //Act
            var dbItem = await service.GetGoodsReceivedNoteItemAsync(d => d.Id == id);
            dbItem!.ItemUnitPrice = 50;
            dbItem.Quantity = 30;
            dbItem.Nbt = 0.5m;

            await service.UpdateGoodsReceivedNoteItemAsync(dbItem);

            var result = await service.GetGoodsReceivedNoteItemAsync(d => d.Id == id);

            //Assert
            result.Should().BeAssignableTo<GoodsReceivedNoteItem>();
            result!.Id.Should().Be(id);
            result.ItemUnitPrice.Should().Be(50);
            result.Quantity.Should().Be(30);
            result.Nbt.Should().Be(0.5m);
        }

        [Fact]
        public async Task DeleteGoodsReceivedNoteItemAsync_Successfully_Deleted()
        {
            //Arrange
            var id = 1;
            var service = new GoodsReceivedNoteItemService(_myRestaurantContext);

            //Act
            var dbGRNItem = await service.GetGoodsReceivedNoteItemAsync(d => d.Id == id);

            await service.DeleteGoodsReceivedNoteItemAsync(dbGRNItem!);

            var result = await service.GetGoodsReceivedNoteItemAsync(d => d.Id == id);

            //Assert
            result.Should().BeNull();
        }
    }
}
