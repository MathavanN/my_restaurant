using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MyRestaurant.Api.Controllers.V1;
using MyRestaurant.Api.Tests.Controllers.V1.Fixtures;
using MyRestaurant.Business.Dtos.V1;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MyRestaurant.Api.Tests.Controllers.V1
{
    public class PurchaseOrderControllerTest : IClassFixture<PurchaseOrderControllerFixture>
    {
        private readonly PurchaseOrderControllerFixture _fixture;

        public PurchaseOrderControllerTest(PurchaseOrderControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async void GetPurchaseOrders_Returns_OkObjectResult()
        {
            //Arrange
            _fixture.MockPurchaseOrderRepository.Setup(x => x.GetPurchaseOrdersAsync())
                .ReturnsAsync(_fixture.PurchaseOrders);

            var controller = new PurchaseOrderController(_fixture.MockPurchaseOrderRepository.Object);

            //Act
            var result = await controller.GetPurchaseOrders();

            //Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var orders = okResult.Value.Should().BeAssignableTo<IEnumerable<GetPurchaseOrderDto>>().Subject;

            okResult.StatusCode.Should().Be(200);
            orders.Should().HaveCount(3);
        }

        [Fact]
        public async void GetPurchaseOrdersForGRN_Returns_OkObjectResult()
        {
            //Arrange
            _fixture.MockPurchaseOrderRepository.Setup(x => x.GetPurchaseOrdersAllowToCreateGRN())
                .ReturnsAsync(_fixture.PurchaseOrders.Where(d => d.ApprovalStatus == "Approved"));

            var controller = new PurchaseOrderController(_fixture.MockPurchaseOrderRepository.Object);

            //Act
            var result = await controller.GetPurchaseOrdersForGRN();

            //Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var orders = okResult.Value.Should().BeAssignableTo<IEnumerable<GetPurchaseOrderDto>>().Subject;

            okResult.StatusCode.Should().Be(200);
            orders.Should().HaveCount(1);
        }

        [Fact]
        public async void GetPurchaseOrder_Returns_OkObjectResult()
        {
            //Arrange
            var id = 2;
            _fixture.MockPurchaseOrderRepository.Setup(x => x.GetPurchaseOrderAsync(id))
                .ReturnsAsync(_fixture.PurchaseOrders.Single(d => d.Id == id));

            var controller = new PurchaseOrderController(_fixture.MockPurchaseOrderRepository.Object);

            //Act
            var result = await controller.GetPurchaseOrder(id);

            //Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var item = okResult.Value.Should().BeAssignableTo<GetPurchaseOrderDto>().Subject;

            okResult.StatusCode.Should().Be(200);
            item.Id.Should().Be(id);
            item.OrderNumber.Should().Be("PO_20210130_8d8c512f7cd7920");
            item.SupplierName.Should().Be("VBT Pvt Ltd");
            item.ApprovalStatus.Should().Be("Pending");
        }

        [Fact]
        public async void CreatePurchaseOrder_Returns_CreatedAtRouteResult()
        {
            //Arrange
            _fixture.MockPurchaseOrderRepository.Setup(x => x.CreatePurchaseOrderAsync(_fixture.ValidCreatePurchaseOrderDto))
                .ReturnsAsync(_fixture.CreatePurchaseOrderDtoResult);

            var controller = new PurchaseOrderController(_fixture.MockPurchaseOrderRepository.Object);

            //Act
            var result = await controller.CreatePurchaseOrder(_fixture.ValidCreatePurchaseOrderDto, _fixture.ApiVersion);

            //Assert
            var objectResult = result.Should().BeOfType<CreatedAtRouteResult>().Subject;
            objectResult.StatusCode.Should().Be(201);
            objectResult.RouteValues["id"].Should().Be(4);
            objectResult.RouteValues["version"].Should().Be($"{_fixture.ApiVersion}");

            var item = objectResult.Value.Should().BeAssignableTo<GetPurchaseOrderDto>().Subject;
            item.Id.Should().Be(4);
            item.OrderNumber.Should().Be("PO_20210216_8d8c512f7cd7920");
            item.SupplierName.Should().Be("ABC Pvt Ldt");
            item.ApprovalStatus.Should().Be("Pending");
        }

        [Fact]
        public async void UpdatePurchaseOrder_Returns_OkObjectResult()
        {
            //Arrange
            var id = 2;
            _fixture.MockPurchaseOrderRepository.Setup(x => x.UpdatePurchaseOrderAsync(id, _fixture.ValidEditPurchaseOrderDto))
                .ReturnsAsync(_fixture.EditPurchaseOrderDtoResult);

            var controller = new PurchaseOrderController(_fixture.MockPurchaseOrderRepository.Object);

            //Act
            var result = await controller.UpdatePurchaseOrder(id, _fixture.ValidEditPurchaseOrderDto);

            //Assert
            var objectResult = result.Should().BeOfType<OkObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);

            var item = objectResult.Value.Should().BeAssignableTo<GetPurchaseOrderDto>().Subject;
            item.Id.Should().Be(id);
            item.OrderNumber.Should().Be("PO_20210130_8d8c512f7cd7920");
            item.SupplierName.Should().Be("ABC Pvt Ldt");
            item.ApprovalStatus.Should().Be("Pending");
        }

        [Fact]
        public async void ApprovePurchaseOrder_Returns_OkObjectResult()
        {
            //Arrange
            var id = 2;
            _fixture.MockPurchaseOrderRepository.Setup(x => x.ApprovalPurchaseOrderAsync(id, _fixture.ValidApprovalPurchaseOrderDto))
                .ReturnsAsync(_fixture.ApprovalPurchaseOrderDtoResult);

            var controller = new PurchaseOrderController(_fixture.MockPurchaseOrderRepository.Object);

            //Act
            var result = await controller.ApprovePurchaseOrder(id, _fixture.ValidApprovalPurchaseOrderDto);

            //Assert
            var objectResult = result.Should().BeOfType<OkObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);

            var item = objectResult.Value.Should().BeAssignableTo<GetPurchaseOrderDto>().Subject;
            item.Id.Should().Be(id);
            item.OrderNumber.Should().Be("PO_20210130_8d8c512f7cd7920");
            item.SupplierName.Should().Be("ABC Pvt Ldt");
            item.ApprovalStatus.Should().Be("Approved");
        }

        [Fact]
        public async void DeletePurchaseOrder_Returns_NoContentResult()
        {
            //Arrange
            var id = 2;
            _fixture.MockPurchaseOrderRepository.Setup(x => x.DeletePurchaseOrderAsync(id));

            var controller = new PurchaseOrderController(_fixture.MockPurchaseOrderRepository.Object);

            //Act
            var result = await controller.DeletePurchaseOrder(id);

            //Assert
            var objectResult = result.Should().BeOfType<NoContentResult>().Subject;
            objectResult.StatusCode.Should().Be(204);
        }
    }
}
