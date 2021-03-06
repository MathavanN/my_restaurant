using FluentAssertions;
using MyRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MyRestaurant.Services.Tests
{
    public class GoodsReceivedNoteServiceTest : MyRestaurantContextTestBase
    {
        public GoodsReceivedNoteServiceTest()
        {
            GoodsReceivedNoteInitializer.Initialize(_myRestaurantContext);
        }

        [Fact]
        public async void GetGoodsReceivedNotesAsync_Returns_GoodsReceivedNotes()
        {
            //Arrange
            var service = new GoodsReceivedNoteService(_myRestaurantContext);

            //Act
            var result = await service.GetGoodsReceivedNotesAsync();

            //Assert
            var grns = result.Should().BeAssignableTo<IEnumerable<GoodsReceivedNote>>().Subject;
            grns.Should().HaveCount(3);
        }

        [Fact]
        public async void GetGoodsReceivedNoteAsync_Returns_GoodsReceivedNote()
        {
            //Arrange
            var id = 1;
            var service = new GoodsReceivedNoteService(_myRestaurantContext);

            //Act
            var result = await service.GetGoodsReceivedNoteAsync(d => d.Id == id);

            //Assert
            var grn = result.Should().BeAssignableTo<GoodsReceivedNote>().Subject;
            grn.Id.Should().Be(id);
            grn.InvoiceNumber.Should().Be("INV_20210132_01");
            grn.PaymentType.CreditPeriod.Should().Be(0);
        }

        [Fact]
        public async void GetGoodsReceivedNoteAsync_Returns_Null()
        {
            //Arrange
            var id = 10001;
            var service = new GoodsReceivedNoteService(_myRestaurantContext);

            //Act
            var result = await service.GetGoodsReceivedNoteAsync(d => d.Id == id);

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async void AddGoodsReceivedNoteAsync_Returns_New_GoodsReceivedNote()
        {
            //Arrange
            var service = new GoodsReceivedNoteService(_myRestaurantContext);

            //Act
            var result = await service.AddGoodsReceivedNoteAsync(new GoodsReceivedNote
            {
                PurchaseOrderId = 5,
                InvoiceNumber = "INV_20210224_01",
                PaymentTypeId = 1,
                Nbt = 0.5m,
                Vat = 0.5m,
                Discount = 0.5m,
                ReceivedBy = _myRestaurantContext.Users.First(d => d.FirstName == "Report").Id,
                ReceivedDate = DateTime.Now.AddDays(-5),
                ApprovedBy = Guid.Empty,
                ApprovalStatus = Status.Pending,
                ApprovedDate = default,
                ApprovalReason = "",
                CreatedBy = _myRestaurantContext.Users.First(d => d.FirstName == "Admin").Id,
                CreatedDate = DateTime.Now
            });

            //Assert
            var grn = result.Should().BeAssignableTo<GoodsReceivedNote>().Subject;
            grn.InvoiceNumber.Should().Be("INV_20210224_01");
            grn.ApprovalStatus.Should().Be(Status.Pending);
        }

        [Fact]
        public async void UpdateGoodsReceivedNoteAsync_Successfully_Updated()
        {
            //Arrange
            var id = 2;
            var service = new GoodsReceivedNoteService(_myRestaurantContext);

            //Act
            var dbGRN = await service.GetGoodsReceivedNoteAsync(d => d.Id == id);
            dbGRN.Nbt = 0.6m;
            dbGRN.Vat = 0.75m;
            dbGRN.Discount = 10;

            await service.UpdateGoodsReceivedNoteAsync(dbGRN);

            var result = await service.GetGoodsReceivedNoteAsync(d => d.Id == id);

            //Assert
            var grn = result.Should().BeAssignableTo<GoodsReceivedNote>().Subject;
            grn.Id.Should().Be(id);
            grn.Nbt.Should().Be(0.6m);
            grn.Vat.Should().Be(0.75m);
            grn.Discount.Should().Be(10);
        }

        [Fact]
        public async void DeleteGoodsReceivedNoteAsync_Successfully_Deleted()
        {
            //Arrange
            var id = 1;
            var service = new GoodsReceivedNoteService(_myRestaurantContext);

            //Act
            var dbGRN = await service.GetGoodsReceivedNoteAsync(d => d.Id == id);

            await service.DeleteGoodsReceivedNoteAsync(dbGRN);

            var result = await service.GetGoodsReceivedNoteAsync(d => d.Id == id);

            //Assert
            result.Should().BeNull();
        }
    }
}