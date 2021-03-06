using FluentAssertions;
using MyRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MyRestaurant.Services.Tests
{
    public class PurchaseOrderServiceTest : MyRestaurantContextTestBase
    {
        public PurchaseOrderServiceTest()
        {
            PurchaseOrderInitializer.Initialize(_myRestaurantContext);
        }

        [Fact]
        public async void GetPurchaseOrdersAsync_Returns_PurchaseOrders()
        {
            //Arrange
            var service = new PurchaseOrderService(_myRestaurantContext);

            //Act
            var result = await service.GetPurchaseOrdersAsync();

            //Assert
            var orders = result.Should().BeAssignableTo<IEnumerable<PurchaseOrder>>().Subject;
            orders.Should().HaveCount(6);
        }

        [Fact]
        public async void GetPurchaseOrderAsync_Returns_PurchaseOrder()
        {
            //Arrange
            var id = 1;
            var service = new PurchaseOrderService(_myRestaurantContext);

            //Act
            var result = await service.GetPurchaseOrderAsync(d => d.Id == id);

            //Assert
            var purchaseOrder = result.Should().BeAssignableTo<PurchaseOrder>().Subject;
            purchaseOrder.Id.Should().Be(id);
            purchaseOrder.OrderNumber.Should().Be("PO_20210130_8d8c510caee6a4b");
            purchaseOrder.Supplier.Name.Should().Be("ABC Pvt Ltd");
        }

        [Fact]
        public async void GetPurchaseOrderAsync_Returns_Null()
        {
            //Arrange
            var id = 10001;
            var service = new PurchaseOrderService(_myRestaurantContext);

            //Act
            var result = await service.GetPurchaseOrderAsync(d => d.Id == id);

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async void AddPurchaseOrderAsync_Returns_New_PurchaseOrder()
        {
            //Arrange
            var requestedUserId = _myRestaurantContext.Users.ToList().FirstOrDefault(d => d.FirstName == "Normal").Id;
            var service = new PurchaseOrderService(_myRestaurantContext);

            //Act
            var result = await service.AddPurchaseOrderAsync(new PurchaseOrder
            {
                OrderNumber = "PO_20210227_8d8c512f7cd7920",
                SupplierId = 2,
                RequestedBy = requestedUserId,
                RequestedDate = DateTime.Now,
                ApprovalStatus = Status.Pending,
                ApprovedBy = Guid.Empty,
                ApprovedDate = default,
                Description = "",
                ApprovalReason = ""
            });

            //Assert
            var order = result.Should().BeAssignableTo<PurchaseOrder>().Subject;
            order.OrderNumber.Should().Be("PO_20210227_8d8c512f7cd7920");
            order.RequestedUser.FirstName.Should().Be("Normal");

            //Act
            var orders = await service.GetPurchaseOrdersAsync();

            //Assert
            orders.Should().HaveCount(7);
        }

        [Fact]
        public async void UpdatePurchaseOrderAsync_Successfully_Updated()
        {
            //Arrange
            var id = 2;
            var service = new PurchaseOrderService(_myRestaurantContext);

            //Act
            var dbPurchaseOrder = await service.GetPurchaseOrderAsync(d => d.Id == id);
            dbPurchaseOrder.SupplierId = 1;
            dbPurchaseOrder.Description = "Supplier Changed";

            await service.UpdatePurchaseOrderAsync(dbPurchaseOrder);

            var result = await service.GetPurchaseOrderAsync(d => d.Id == id);

            //Assert
            var order = result.Should().BeAssignableTo<PurchaseOrder>().Subject;
            order.Id.Should().Be(id);
            order.Supplier.Name.Should().Be("ABC Pvt Ltd");
            order.Description.Should().Be("Supplier Changed");
        }

        [Fact]
        public async void DeletePurchaseOrderAsync_Successfully_Deleted()
        {
            //Arrange
            var id = 1;
            var service = new PurchaseOrderService(_myRestaurantContext);

            //Act
            var dbPurchaseOrder = await service.GetPurchaseOrderAsync(d => d.Id == id);

            await service.DeletePurchaseOrderAsync(dbPurchaseOrder);

            var result = await service.GetPurchaseOrderAsync(d => d.Id == id);

            //Assert
            result.Should().BeNull();
        }
    }
}
