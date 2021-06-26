using FluentAssertions;
using Moq;
using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Errors;
using MyRestaurant.Business.Repositories;
using MyRestaurant.Business.Tests.Repositories.Fixtures;
using MyRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace MyRestaurant.Business.Tests.Repositories
{
    public class PurchaseOrderRepositoryTest : IClassFixture<PurchaseOrderRepositoryFixture>
    {
        private readonly PurchaseOrderRepositoryFixture _fixture;
        public PurchaseOrderRepositoryTest(PurchaseOrderRepositoryFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GetPurchaseOrdersAllowToCreateGRN_Returns_GetPurchaseOrderDtos()
        {
            //Arrange
            _fixture.MockPurchaseOrderService.Setup(x => x.GetPurchaseOrdersAsync(It.IsAny<Expression<Func<PurchaseOrder, bool>>>()))
                .Returns<Expression<Func<PurchaseOrder, bool>>>(async (expression) =>
                {
                    var orders = _fixture.PurchaseOrders.AsQueryable().Where(expression).ToList();
                    return await Task.FromResult(orders);
                });

            var repository = new PurchaseOrderRepository(AutoMapperSingleton.Mapper, _fixture.MockPurchaseOrderService.Object, _fixture.MockUserAccessorService.Object);

            //Act
            var result = await repository.GetPurchaseOrdersAllowToCreateGRN();

            //Assert
            var orders = result.Should().BeAssignableTo<IEnumerable<GetPurchaseOrderDto>>().Subject;
            orders.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetPurchaseOrdersAsync_Returns_GetPurchaseOrderDtos()
        {
            //Arrange
            _fixture.MockPurchaseOrderService.Setup(x => x.GetPurchaseOrdersAsync(null))
                .ReturnsAsync(_fixture.PurchaseOrders);

            var repository = new PurchaseOrderRepository(AutoMapperSingleton.Mapper, _fixture.MockPurchaseOrderService.Object, _fixture.MockUserAccessorService.Object);

            //Act
            var result = await repository.GetPurchaseOrdersAsync();

            //Assert
            var orders = result.Should().BeAssignableTo<IEnumerable<GetPurchaseOrderDto>>().Subject;
            orders.Should().HaveCount(4);
        }

        [Fact]
        public async Task GetPurchaseOrderAsync_Returns_GetPurchaseOrderDto()
        {
            //Arrange
            var id = 1;
            _fixture.MockPurchaseOrderService.Setup(x => x.GetPurchaseOrderAsync(It.IsAny<Expression<Func<PurchaseOrder, bool>>>()))
                .Returns<Expression<Func<PurchaseOrder, bool>>>(expression => Task.FromResult(_fixture.PurchaseOrders.AsQueryable().FirstOrDefault(expression)));

            var repository = new PurchaseOrderRepository(AutoMapperSingleton.Mapper, _fixture.MockPurchaseOrderService.Object, _fixture.MockUserAccessorService.Object);

            //Act
            var result = await repository.GetPurchaseOrderAsync(id);

            //Assert
            result.Should().BeOfType(typeof(GetPurchaseOrderDto));
            result.Id.Should().Be(id);
            result.OrderNumber.Should().Be("PO_20210130_8d8c510caee6a4b");
            result.SupplierName.Should().Be("ABC Pvt Ltd");
        }

        [Fact]
        public async Task GetPurchaseOrderAsync_Throws_NotFoundException()
        {
            //Arrange
            var id = 201;
            _fixture.MockPurchaseOrderService.Setup(x => x.GetPurchaseOrderAsync(It.IsAny<Expression<Func<PurchaseOrder, bool>>>()))
                .Returns<Expression<Func<PurchaseOrder, bool>>>(expression => Task.FromResult(_fixture.PurchaseOrders.AsQueryable().FirstOrDefault(expression)));

            var repository = new PurchaseOrderRepository(AutoMapperSingleton.Mapper, _fixture.MockPurchaseOrderService.Object, _fixture.MockUserAccessorService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.GetPurchaseOrderAsync(id));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound);
            exception.ErrorMessage.Should().Be("Purchase order not found.");
            exception.ErrorType.Should().Be(HttpStatusCode.NotFound.ToString());
        }

        [Fact]
        public async Task CreatePurchaseOrderAsync_Returns_New_GetPurchaseOrderDto()
        {
            //Arrange
            _fixture.MockPurchaseOrderService.Setup(x => x.AddPurchaseOrderAsync(It.IsAny<PurchaseOrder>()))
                .ReturnsAsync(_fixture.CreatedNewPurchaseOrder);

            _fixture.MockUserAccessorService.Setup(x => x.GetCurrentUser()).Returns(_fixture.CurrentUser);

            var repository = new PurchaseOrderRepository(AutoMapperSingleton.Mapper, _fixture.MockPurchaseOrderService.Object, _fixture.MockUserAccessorService.Object);

            //Act
            var result = await repository.CreatePurchaseOrderAsync(_fixture.CreatePurchaseOrderDto);

            //Assert
            result.Should().BeOfType(typeof(GetPurchaseOrderDto));
            result.Id.Should().Be(5);
            result.OrderNumber.Should().Be("PO_20210227_8d8c512f7cd7920");
            result.SupplierName.Should().Be("VBT Pvt Ltd");
        }

        [Fact]
        public async Task UpdatePurchaseOrderAsync_Returns_Updated_GetPurchaseOrderDto()
        {
            //Arrange
            var id = 2;
            _fixture.MockPurchaseOrderService.Setup(x => x.GetPurchaseOrderAsync(It.IsAny<Expression<Func<PurchaseOrder, bool>>>()))
                .Returns<Expression<Func<PurchaseOrder, bool>>>(expression => Task.FromResult(_fixture.PurchaseOrders.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockPurchaseOrderService.Setup(x => x.UpdatePurchaseOrderAsync(It.IsAny<PurchaseOrder>()))
                .Returns(Task.FromResult(_fixture.UpdatedPurchaseOrder));

            var repository = new PurchaseOrderRepository(AutoMapperSingleton.Mapper, _fixture.MockPurchaseOrderService.Object, _fixture.MockUserAccessorService.Object);

            //Act
            var result = await repository.UpdatePurchaseOrderAsync(id, _fixture.EditPurchaseOrderDto);

            //Assert
            result.Should().BeOfType(typeof(GetPurchaseOrderDto));
            result.Id.Should().Be(id);
            result.OrderNumber.Should().Be("PO_20210130_8d8c512f7cd7920");
            result.SupplierName.Should().Be("ABC Pvt Ltd");
        }

        [Fact]
        public async Task UpdateUnitOfMeasureAsync_Throws_NotFoundException()
        {
            //Arrange
            var id = 201;
            _fixture.MockPurchaseOrderService.Setup(x => x.GetPurchaseOrderAsync(It.IsAny<Expression<Func<PurchaseOrder, bool>>>()))
                .Returns<Expression<Func<PurchaseOrder, bool>>>(expression => Task.FromResult(_fixture.PurchaseOrders.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockPurchaseOrderService.Setup(x => x.UpdatePurchaseOrderAsync(It.IsAny<PurchaseOrder>()));

            var repository = new PurchaseOrderRepository(AutoMapperSingleton.Mapper, _fixture.MockPurchaseOrderService.Object, _fixture.MockUserAccessorService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.UpdatePurchaseOrderAsync(id, _fixture.EditPurchaseOrderDto));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound);
            exception.ErrorMessage.Should().Be("Purchase order not found.");
            exception.ErrorType.Should().Be(HttpStatusCode.NotFound.ToString());
        }

        [Fact]
        public async Task ApprovalPurchaseOrderAsync_Throws_NotFoundException()
        {
            //Arrange
            var id = 201;
            _fixture.MockPurchaseOrderService.Setup(x => x.GetPurchaseOrderAsync(It.IsAny<Expression<Func<PurchaseOrder, bool>>>()))
                .Returns<Expression<Func<PurchaseOrder, bool>>>(expression => Task.FromResult(_fixture.PurchaseOrders.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockPurchaseOrderService.Setup(x => x.UpdatePurchaseOrderAsync(It.IsAny<PurchaseOrder>()));

            var repository = new PurchaseOrderRepository(AutoMapperSingleton.Mapper, _fixture.MockPurchaseOrderService.Object, _fixture.MockUserAccessorService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.ApprovalPurchaseOrderAsync(id, _fixture.ApprovalPurchaseOrderDto));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound);
            exception.ErrorMessage.Should().Be("Purchase order not found.");
            exception.ErrorType.Should().Be(HttpStatusCode.NotFound.ToString());
        }

        [Fact]
        public async Task ApprovalPurchaseOrderAsync_Throws_User_BadRequestException()
        {
            //Arrange
            var id = 2;
            _fixture.MockPurchaseOrderService.Setup(x => x.GetPurchaseOrderAsync(It.IsAny<Expression<Func<PurchaseOrder, bool>>>()))
                .Returns<Expression<Func<PurchaseOrder, bool>>>(expression => Task.FromResult(_fixture.PurchaseOrders.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockPurchaseOrderService.Setup(x => x.UpdatePurchaseOrderAsync(It.IsAny<PurchaseOrder>()))
                .Returns(Task.FromResult(_fixture.ApprovedPurchaseOrder));

            _fixture.MockUserAccessorService.Setup(x => x.GetCurrentUser()).Returns(_fixture.NullCurrentUser);

            var repository = new PurchaseOrderRepository(AutoMapperSingleton.Mapper, _fixture.MockPurchaseOrderService.Object, _fixture.MockUserAccessorService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.ApprovalPurchaseOrderAsync(id, _fixture.ApprovalPurchaseOrderDto));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.BadRequest);
            exception.ErrorMessage.Should().Be("User details not found. Login again.");
            exception.ErrorType.Should().Be(HttpStatusCode.BadRequest.ToString());
        }

        [Fact]
        public async Task ApprovalPurchaseOrderAsync_Throws_UserId_BadRequestException()
        {
            //Arrange
            var id = 2;
            _fixture.MockPurchaseOrderService.Setup(x => x.GetPurchaseOrderAsync(It.IsAny<Expression<Func<PurchaseOrder, bool>>>()))
                .Returns<Expression<Func<PurchaseOrder, bool>>>(expression => Task.FromResult(_fixture.PurchaseOrders.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockPurchaseOrderService.Setup(x => x.UpdatePurchaseOrderAsync(It.IsAny<PurchaseOrder>()))
                .Returns(Task.FromResult(_fixture.ApprovedPurchaseOrder));

            _fixture.MockUserAccessorService.Setup(x => x.GetCurrentUser()).Returns(_fixture.EmptyUserIdCurrentUser);

            var repository = new PurchaseOrderRepository(AutoMapperSingleton.Mapper, _fixture.MockPurchaseOrderService.Object, _fixture.MockUserAccessorService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.ApprovalPurchaseOrderAsync(id, _fixture.ApprovalPurchaseOrderDto));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.BadRequest);
            exception.ErrorMessage.Should().Be("User details not found. Login again.");
            exception.ErrorType.Should().Be(HttpStatusCode.BadRequest.ToString());
        }

        [Fact]
        public async Task ApprovalPurchaseOrderAsync_Returns_Approval_GetPurchaseOrderDto()
        {
            //Arrange
            var id = 2;
            _fixture.MockPurchaseOrderService.Setup(x => x.GetPurchaseOrderAsync(It.IsAny<Expression<Func<PurchaseOrder, bool>>>()))
                .Returns<Expression<Func<PurchaseOrder, bool>>>(expression => Task.FromResult(_fixture.PurchaseOrders.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockPurchaseOrderService.Setup(x => x.UpdatePurchaseOrderAsync(It.IsAny<PurchaseOrder>()))
                .Returns(Task.FromResult(_fixture.ApprovedPurchaseOrder));

            _fixture.MockUserAccessorService.Setup(x => x.GetCurrentUser()).Returns(_fixture.CurrentUser);

            var repository = new PurchaseOrderRepository(AutoMapperSingleton.Mapper, _fixture.MockPurchaseOrderService.Object, _fixture.MockUserAccessorService.Object);

            //Act
            var result = await repository.ApprovalPurchaseOrderAsync(id, _fixture.ApprovalPurchaseOrderDto);

            //Assert
            result.Should().BeOfType(typeof(GetPurchaseOrderDto));
            result.Id.Should().Be(id);
            result.OrderNumber.Should().Be("PO_20210130_8d8c512f7cd7920");
            result.ApprovalStatus.Should().Be("Rejected");
        }

        [Fact]
        public async Task DeletePurchaseOrderAsync_Returns_NoResult()
        {
            //Arrange
            var id = 2;
            _fixture.MockPurchaseOrderService.Setup(x => x.GetPurchaseOrderAsync(It.IsAny<Expression<Func<PurchaseOrder, bool>>>()))
                .Returns<Expression<Func<PurchaseOrder, bool>>>(expression => Task.FromResult(_fixture.PurchaseOrders.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockPurchaseOrderService.Setup(x => x.DeletePurchaseOrderAsync(It.IsAny<PurchaseOrder>()));

            var repository = new PurchaseOrderRepository(AutoMapperSingleton.Mapper, _fixture.MockPurchaseOrderService.Object, _fixture.MockUserAccessorService.Object);

            //Act
            await repository.DeletePurchaseOrderAsync(id);

            // Assert
            _fixture.MockPurchaseOrderService.Verify(x => x.DeletePurchaseOrderAsync(It.IsAny<PurchaseOrder>()), Times.Once);
        }

        [Fact]
        public async Task DeletePurchaseOrderAsync_Throws_NotFoundException()
        {
            //Arrange
            var id = 201;
            _fixture.MockPurchaseOrderService.Setup(x => x.GetPurchaseOrderAsync(It.IsAny<Expression<Func<PurchaseOrder, bool>>>()))
                .Returns<Expression<Func<PurchaseOrder, bool>>>(expression => Task.FromResult(_fixture.PurchaseOrders.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockPurchaseOrderService.Setup(x => x.DeletePurchaseOrderAsync(It.IsAny<PurchaseOrder>()));

            var repository = new PurchaseOrderRepository(AutoMapperSingleton.Mapper, _fixture.MockPurchaseOrderService.Object, _fixture.MockUserAccessorService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.DeletePurchaseOrderAsync(id));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound);
            exception.ErrorMessage.Should().Be("Purchase order not found.");
            exception.ErrorType.Should().Be(HttpStatusCode.NotFound.ToString());
        }
    }
}
