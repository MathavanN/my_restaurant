using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MyRestaurant.Api.Controllers.V1;
using MyRestaurant.Api.Tests.Controllers.V1.Fixtures;
using MyRestaurant.Business.Dtos.V1;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task GetPurchaseOrders_Returns_OkObjectResult()
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
        public async Task GetPurchaseOrdersForGRN_Returns_OkObjectResult()
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
        public async Task GetPurchaseOrder_Returns_OkObjectResult()
        {
            //Arrange
            var id = 2;
            _fixture.MockPurchaseOrderRepository.Setup(x => x.GetPurchaseOrderAsync(It.IsAny<long>()))
                .ReturnsAsync(_fixture.PurchaseOrders.Single(d => d.Id == id));

            var controller = new PurchaseOrderController(_fixture.MockPurchaseOrderRepository.Object);

            //Act
            var result = await controller.GetPurchaseOrder(id);

            //Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var order = okResult.Value.Should().BeAssignableTo<GetPurchaseOrderDto>().Subject;

            okResult.StatusCode.Should().Be(200);
            order.Id.Should().Be(id);
            order.OrderNumber.Should().Be("PO_20210130_8d8c512f7cd7920");
            order.SupplierName.Should().Be("VBT Pvt Ltd");
            order.ApprovalStatus.Should().Be("Pending");
        }

        [Fact]
        public async Task CreatePurchaseOrder_Returns_CreatedAtRouteResult()
        {
            //Arrange
            _fixture.MockPurchaseOrderRepository.Setup(x => x.CreatePurchaseOrderAsync(It.IsAny<CreatePurchaseOrderDto>()))
                .ReturnsAsync(_fixture.CreatePurchaseOrderDtoResult);

            var controller = new PurchaseOrderController(_fixture.MockPurchaseOrderRepository.Object);

            //Act
            var result = await controller.CreatePurchaseOrder(_fixture.ValidCreatePurchaseOrderDto, _fixture.ApiVersion);

            //Assert
            var objectResult = result.Should().BeOfType<CreatedAtRouteResult>().Subject;
            objectResult.StatusCode.Should().Be(201);
            objectResult.RouteValues["id"].Should().Be(4);
            objectResult.RouteValues["version"].Should().Be($"{_fixture.ApiVersion}");

            var order = objectResult.Value.Should().BeAssignableTo<GetPurchaseOrderDto>().Subject;
            order.Id.Should().Be(4);
            order.OrderNumber.Should().Be("PO_20210216_8d8c512f7cd7920");
            order.SupplierName.Should().Be("ABC Pvt Ldt");
            order.ApprovalStatus.Should().Be("Pending");
        }

        [Fact]
        public async Task UpdatePurchaseOrder_Returns_OkObjectResult()
        {
            //Arrange
            var id = 2;
            _fixture.MockPurchaseOrderRepository.Setup(x => x.UpdatePurchaseOrderAsync(It.IsAny<long>(), It.IsAny<EditPurchaseOrderDto>()))
                .ReturnsAsync(_fixture.EditPurchaseOrderDtoResult);

            var controller = new PurchaseOrderController(_fixture.MockPurchaseOrderRepository.Object);

            //Act
            var result = await controller.UpdatePurchaseOrder(id, _fixture.ValidEditPurchaseOrderDto);

            //Assert
            var objectResult = result.Should().BeOfType<OkObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);

            var order = objectResult.Value.Should().BeAssignableTo<GetPurchaseOrderDto>().Subject;
            order.Id.Should().Be(id);
            order.OrderNumber.Should().Be("PO_20210130_8d8c512f7cd7920");
            order.SupplierName.Should().Be("ABC Pvt Ldt");
            order.ApprovalStatus.Should().Be("Pending");
        }

        [Fact]
        public async Task ApprovePurchaseOrder_Returns_OkObjectResult()
        {
            //Arrange
            var id = 2;
            _fixture.MockPurchaseOrderRepository.Setup(x => x.ApprovalPurchaseOrderAsync(It.IsAny<long>(), It.IsAny<ApprovalPurchaseOrderDto>()))
                .ReturnsAsync(_fixture.ApprovalPurchaseOrderDtoResult);

            var controller = new PurchaseOrderController(_fixture.MockPurchaseOrderRepository.Object);

            //Act
            var result = await controller.ApprovePurchaseOrder(id, _fixture.ValidApprovalPurchaseOrderDto);

            //Assert
            var objectResult = result.Should().BeOfType<OkObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);

            var order = objectResult.Value.Should().BeAssignableTo<GetPurchaseOrderDto>().Subject;
            order.Id.Should().Be(id);
            order.OrderNumber.Should().Be("PO_20210130_8d8c512f7cd7920");
            order.SupplierName.Should().Be("ABC Pvt Ldt");
            order.ApprovalStatus.Should().Be("Approved");
        }

        [Fact]
        public async Task DeletePurchaseOrder_Returns_NoContentResult()
        {
            //Arrange
            var id = 2;
            _fixture.MockPurchaseOrderRepository.Setup(x => x.DeletePurchaseOrderAsync(It.IsAny<long>()));

            var controller = new PurchaseOrderController(_fixture.MockPurchaseOrderRepository.Object);

            //Act
            var result = await controller.DeletePurchaseOrder(id);

            //Assert
            var objectResult = result.Should().BeOfType<NoContentResult>().Subject;
            objectResult.StatusCode.Should().Be(204);
        }
    }
}
