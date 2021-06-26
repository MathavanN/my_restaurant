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
    public class GoodsReceivedNoteRepositoryTest : IClassFixture<GoodsReceivedNoteRepositoryFixture>
    {
        private readonly GoodsReceivedNoteRepositoryFixture _fixture;
        public GoodsReceivedNoteRepositoryTest(GoodsReceivedNoteRepositoryFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GetGoodsReceivedNotesAsync_Returns_GetGoodsReceivedNoteDtos()
        {
            //Arrange
            _fixture.MockGoodsReceivedNoteService.Setup(x => x.GetGoodsReceivedNotesAsync())
                .ReturnsAsync(_fixture.GoodsReceivedNotes);

            var repository = new GoodsReceivedNoteRepository(AutoMapperSingleton.Mapper, _fixture.MockGoodsReceivedNoteService.Object,
                _fixture.MockUserAccessorService.Object, _fixture.MockPurchaseOrderService.Object, _fixture.MockPurchaseOrderItemService.Object,
                _fixture.MockGoodsReceivedNoteItemService.Object);

            //Act
            var result = await repository.GetGoodsReceivedNotesAsync();

            //Assert
            var grns = result.Should().BeAssignableTo<IEnumerable<GetGoodsReceivedNoteDto>>().Subject;
            grns.Should().HaveCount(4);
        }

        [Fact]
        public async Task GetGoodsReceivedNoteAsync_Returns_GetGoodsReceivedNoteDto()
        {
            //Arrange
            var id = 1;
            _fixture.MockGoodsReceivedNoteService.Setup(x => x.GetGoodsReceivedNoteAsync(It.IsAny<Expression<Func<GoodsReceivedNote, bool>>>()))
                .Returns<Expression<Func<GoodsReceivedNote, bool>>>(expression => Task.FromResult(_fixture.GoodsReceivedNotes.AsQueryable().FirstOrDefault(expression)));

            var repository = new GoodsReceivedNoteRepository(AutoMapperSingleton.Mapper, _fixture.MockGoodsReceivedNoteService.Object,
                _fixture.MockUserAccessorService.Object, _fixture.MockPurchaseOrderService.Object, _fixture.MockPurchaseOrderItemService.Object,
                _fixture.MockGoodsReceivedNoteItemService.Object);

            //Act
            var result = await repository.GetGoodsReceivedNoteAsync(id);

            //Assert
            result.Should().BeOfType(typeof(GetGoodsReceivedNoteDto));
            result.Id.Should().Be(id);
            result.PurchaseOrderNumber.Should().Be("PO_20210210_8d8c510caee6a4b");
            result.PaymentTypeName.Should().Be("Cash");
            result.ApprovalStatus.Should().Be("Pending");
        }

        [Fact]
        public async Task GetGoodsReceivedNoteAsync_Throws_NotFoundException()
        {
            //Arrange
            var id = 201;
            _fixture.MockGoodsReceivedNoteService.Setup(x => x.GetGoodsReceivedNoteAsync(It.IsAny<Expression<Func<GoodsReceivedNote, bool>>>()))
                .Returns<Expression<Func<GoodsReceivedNote, bool>>>(expression => Task.FromResult(_fixture.GoodsReceivedNotes.AsQueryable().FirstOrDefault(expression)));

            var repository = new GoodsReceivedNoteRepository(AutoMapperSingleton.Mapper, _fixture.MockGoodsReceivedNoteService.Object,
                _fixture.MockUserAccessorService.Object, _fixture.MockPurchaseOrderService.Object, _fixture.MockPurchaseOrderItemService.Object,
                _fixture.MockGoodsReceivedNoteItemService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.GetGoodsReceivedNoteAsync(id));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound);
            exception.ErrorMessage.Should().Be("Goods received note not found.");
            exception.ErrorType.Should().Be(HttpStatusCode.NotFound.ToString());
        }

        [Fact]
        public async Task CreateGoodsReceivedNoteAsync_Returns_New_GetGoodsReceivedNoteDto()
        {
            //Arrange
            _fixture.MockPurchaseOrderService.Setup(x => x.GetPurchaseOrderAsync(It.IsAny<Expression<Func<PurchaseOrder, bool>>>()))
                .Returns<Expression<Func<PurchaseOrder, bool>>>(expression => Task.FromResult(_fixture.PurchaseOrders.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockUserAccessorService.Setup(x => x.GetCurrentUser()).Returns(_fixture.CurrentUser);

            _fixture.MockPurchaseOrderItemService.Setup(x => x.GetPurchaseOrderItemsAsync(It.IsAny<Expression<Func<PurchaseOrderItem, bool>>>()))
                .Returns<Expression<Func<PurchaseOrderItem, bool>>>(async (expression) =>
                {
                    var orders = _fixture.PurchaseOrderItems.AsQueryable().Where(expression).ToList();
                    return await Task.FromResult(orders);
                });

            _fixture.MockGoodsReceivedNoteService.Setup(x => x.AddGoodsReceivedNoteAsync(It.IsAny<GoodsReceivedNote>()))
                .ReturnsAsync(_fixture.CreateNewGoodsReceivedNote);

            _fixture.MockGoodsReceivedNoteItemService.Setup(x => x.AddGoodsReceivedNoteItemAsync(It.IsAny<GoodsReceivedNoteItem>()));

            var repository = new GoodsReceivedNoteRepository(AutoMapperSingleton.Mapper, _fixture.MockGoodsReceivedNoteService.Object,
                _fixture.MockUserAccessorService.Object, _fixture.MockPurchaseOrderService.Object, _fixture.MockPurchaseOrderItemService.Object,
                _fixture.MockGoodsReceivedNoteItemService.Object);

            //Act
            var result = await repository.CreateGoodsReceivedNoteAsync(_fixture.CreateGoodsReceivedNote);

            //Assert
            result.Should().BeOfType(typeof(GetGoodsReceivedNoteDto));
            result.Id.Should().Be(5);
            result.PurchaseOrderNumber.Should().Be("PO_20210227_8d8c510caee6a4b");
            result.InvoiceNumber.Should().Be("INV_20210228_03");
            result.ApprovalStatus.Should().Be("Pending");
            result.Vat.Should().Be(0.7m);
            result.Discount.Should().Be(1.4m);
            result.Nbt.Should().Be(0.7m);
        }

        [Fact]
        public async Task CreateGoodsReceivedNoteAsync_Throws_PurchaseOrder_NotFoundException()
        {
            //Arrange
            _fixture.MockPurchaseOrderService.Setup(x => x.GetPurchaseOrderAsync(It.IsAny<Expression<Func<PurchaseOrder, bool>>>()))
                .Returns<Expression<Func<PurchaseOrder, bool>>>(expression => Task.FromResult(_fixture.PurchaseOrders.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockUserAccessorService.Setup(x => x.GetCurrentUser()).Returns(_fixture.CurrentUser);

            _fixture.MockPurchaseOrderItemService.Setup(x => x.GetPurchaseOrderItemsAsync(It.IsAny<Expression<Func<PurchaseOrderItem, bool>>>()))
                .Returns<Expression<Func<PurchaseOrderItem, bool>>>(async (expression) =>
                {
                    var orders = _fixture.PurchaseOrderItems.AsQueryable().Where(expression).ToList();
                    return await Task.FromResult(orders);
                });

            _fixture.MockGoodsReceivedNoteService.Setup(x => x.AddGoodsReceivedNoteAsync(It.IsAny<GoodsReceivedNote>()))
                .ReturnsAsync(_fixture.CreateNewGoodsReceivedNote);

            _fixture.MockGoodsReceivedNoteItemService.Setup(x => x.AddGoodsReceivedNoteItemAsync(It.IsAny<GoodsReceivedNoteItem>()));

            var repository = new GoodsReceivedNoteRepository(AutoMapperSingleton.Mapper, _fixture.MockGoodsReceivedNoteService.Object,
                _fixture.MockUserAccessorService.Object, _fixture.MockPurchaseOrderService.Object, _fixture.MockPurchaseOrderItemService.Object,
                _fixture.MockGoodsReceivedNoteItemService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.CreateGoodsReceivedNoteAsync(new CreateGoodsReceivedNoteDto
            {
                PurchaseOrderId = 40,
                InvoiceNumber = "INV_20210228_03",
                Vat = 0.7m,
                Discount = 1.4m,
                Nbt = 0.7m,
                PaymentTypeId = 1,
                ReceivedBy = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa3916b9"),
                ReceivedDate = DateTime.Now.AddDays(-1)
            }));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound);
            exception.ErrorMessage.Should().Be("Purchase order not found.");
            exception.ErrorType.Should().Be(HttpStatusCode.NotFound.ToString());
        }

        [Fact]
        public async Task CreateGoodsReceivedNoteAsync_Throws_PurchaseOrder_BadRequestException()
        {
            //Arrange
            _fixture.MockPurchaseOrderService.Setup(x => x.GetPurchaseOrderAsync(It.IsAny<Expression<Func<PurchaseOrder, bool>>>()))
                .Returns<Expression<Func<PurchaseOrder, bool>>>(expression => Task.FromResult(_fixture.PurchaseOrders.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockUserAccessorService.Setup(x => x.GetCurrentUser()).Returns(_fixture.CurrentUser);

            _fixture.MockPurchaseOrderItemService.Setup(x => x.GetPurchaseOrderItemsAsync(It.IsAny<Expression<Func<PurchaseOrderItem, bool>>>()))
                .Returns<Expression<Func<PurchaseOrderItem, bool>>>(async (expression) =>
                {
                    var orders = _fixture.PurchaseOrderItems.AsQueryable().Where(expression).ToList();
                    return await Task.FromResult(orders);
                });

            _fixture.MockGoodsReceivedNoteService.Setup(x => x.AddGoodsReceivedNoteAsync(It.IsAny<GoodsReceivedNote>()))
                .ReturnsAsync(_fixture.CreateNewGoodsReceivedNote);

            _fixture.MockGoodsReceivedNoteItemService.Setup(x => x.AddGoodsReceivedNoteItemAsync(It.IsAny<GoodsReceivedNoteItem>()));

            var repository = new GoodsReceivedNoteRepository(AutoMapperSingleton.Mapper, _fixture.MockGoodsReceivedNoteService.Object,
                _fixture.MockUserAccessorService.Object, _fixture.MockPurchaseOrderService.Object, _fixture.MockPurchaseOrderItemService.Object,
                _fixture.MockGoodsReceivedNoteItemService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.CreateGoodsReceivedNoteAsync(new CreateGoodsReceivedNoteDto
            {
                PurchaseOrderId = 4,
                InvoiceNumber = "INV_20210228_03",
                Vat = 0.7m,
                Discount = 1.4m,
                Nbt = 0.7m,
                PaymentTypeId = 1,
                ReceivedBy = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa3916b9"),
                ReceivedDate = DateTime.Now.AddDays(-1)
            }));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.BadRequest);
            exception.ErrorMessage.Should().Be("GRN can create for approved purchase order.");
            exception.ErrorType.Should().Be(HttpStatusCode.BadRequest.ToString());
        }

        [Fact]
        public async Task CreateGoodsReceivedNoteAsync_Throws_User_BadRequestException()
        {
            //Arrange
            _fixture.MockPurchaseOrderService.Setup(x => x.GetPurchaseOrderAsync(It.IsAny<Expression<Func<PurchaseOrder, bool>>>()))
                .Returns<Expression<Func<PurchaseOrder, bool>>>(expression => Task.FromResult(_fixture.PurchaseOrders.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockUserAccessorService.Setup(x => x.GetCurrentUser()).Returns(_fixture.NullCurrentUser);

            _fixture.MockPurchaseOrderItemService.Setup(x => x.GetPurchaseOrderItemsAsync(It.IsAny<Expression<Func<PurchaseOrderItem, bool>>>()))
                .Returns<Expression<Func<PurchaseOrderItem, bool>>>(async (expression) =>
                {
                    var orders = _fixture.PurchaseOrderItems.AsQueryable().Where(expression).ToList();
                    return await Task.FromResult(orders);
                });

            _fixture.MockGoodsReceivedNoteService.Setup(x => x.AddGoodsReceivedNoteAsync(It.IsAny<GoodsReceivedNote>()))
                .ReturnsAsync(_fixture.CreateNewGoodsReceivedNote);

            _fixture.MockGoodsReceivedNoteItemService.Setup(x => x.AddGoodsReceivedNoteItemAsync(It.IsAny<GoodsReceivedNoteItem>()));

            var repository = new GoodsReceivedNoteRepository(AutoMapperSingleton.Mapper, _fixture.MockGoodsReceivedNoteService.Object,
                _fixture.MockUserAccessorService.Object, _fixture.MockPurchaseOrderService.Object, _fixture.MockPurchaseOrderItemService.Object,
                _fixture.MockGoodsReceivedNoteItemService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.CreateGoodsReceivedNoteAsync(new CreateGoodsReceivedNoteDto
            {
                PurchaseOrderId = 3,
                InvoiceNumber = "INV_20210224_01",
                Vat = 0.7m,
                Discount = 1.4m,
                Nbt = 0.7m,
                PaymentTypeId = 1,
                ReceivedBy = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa3916b9"),
                ReceivedDate = DateTime.Now.AddDays(-5)
            }));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.BadRequest);
            exception.ErrorMessage.Should().Be("User details not found. Login again.");
            exception.ErrorType.Should().Be(HttpStatusCode.BadRequest.ToString());
        }

        [Fact]
        public async Task CreateGoodsReceivedNoteAsync_Throws_UserId_BadRequestException()
        {
            //Arrange
            _fixture.MockPurchaseOrderService.Setup(x => x.GetPurchaseOrderAsync(It.IsAny<Expression<Func<PurchaseOrder, bool>>>()))
                .Returns<Expression<Func<PurchaseOrder, bool>>>(expression => Task.FromResult(_fixture.PurchaseOrders.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockUserAccessorService.Setup(x => x.GetCurrentUser()).Returns(_fixture.EmptyUserIdCurrentUser);

            _fixture.MockPurchaseOrderItemService.Setup(x => x.GetPurchaseOrderItemsAsync(It.IsAny<Expression<Func<PurchaseOrderItem, bool>>>()))
                .Returns<Expression<Func<PurchaseOrderItem, bool>>>(async (expression) =>
                {
                    var orders = _fixture.PurchaseOrderItems.AsQueryable().Where(expression).ToList();
                    return await Task.FromResult(orders);
                });

            _fixture.MockGoodsReceivedNoteService.Setup(x => x.AddGoodsReceivedNoteAsync(It.IsAny<GoodsReceivedNote>()))
                .ReturnsAsync(_fixture.CreateNewGoodsReceivedNote);

            _fixture.MockGoodsReceivedNoteItemService.Setup(x => x.AddGoodsReceivedNoteItemAsync(It.IsAny<GoodsReceivedNoteItem>()));

            var repository = new GoodsReceivedNoteRepository(AutoMapperSingleton.Mapper, _fixture.MockGoodsReceivedNoteService.Object,
                _fixture.MockUserAccessorService.Object, _fixture.MockPurchaseOrderService.Object, _fixture.MockPurchaseOrderItemService.Object,
                _fixture.MockGoodsReceivedNoteItemService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.CreateGoodsReceivedNoteAsync(new CreateGoodsReceivedNoteDto
            {
                PurchaseOrderId = 3,
                InvoiceNumber = "INV_20210224_01",
                Vat = 0.7m,
                Discount = 1.4m,
                Nbt = 0.7m,
                PaymentTypeId = 1,
                ReceivedBy = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa3916b9"),
                ReceivedDate = DateTime.Now.AddDays(-5)
            }));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.BadRequest);
            exception.ErrorMessage.Should().Be("User details not found. Login again.");
            exception.ErrorType.Should().Be(HttpStatusCode.BadRequest.ToString());
        }

        [Fact]
        public async Task CreateGoodsReceivedNoteAsync_Throws_GRN_BadRequestException()
        {
            //Arrange
            _fixture.MockPurchaseOrderService.Setup(x => x.GetPurchaseOrderAsync(It.IsAny<Expression<Func<PurchaseOrder, bool>>>()))
                .Returns<Expression<Func<PurchaseOrder, bool>>>(expression => Task.FromResult(_fixture.PurchaseOrders.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockUserAccessorService.Setup(x => x.GetCurrentUser()).Returns(_fixture.CurrentUser);

            _fixture.MockPurchaseOrderItemService.Setup(x => x.GetPurchaseOrderItemsAsync(It.IsAny<Expression<Func<PurchaseOrderItem, bool>>>()))
                .Returns<Expression<Func<PurchaseOrderItem, bool>>>(async (expression) =>
                {
                    var orders = _fixture.PurchaseOrderItems.AsQueryable().Where(expression).ToList();
                    return await Task.FromResult(orders);
                });

            _fixture.MockGoodsReceivedNoteService.Setup(x => x.AddGoodsReceivedNoteAsync(It.IsAny<GoodsReceivedNote>()))
                .ReturnsAsync(_fixture.CreateNewGoodsReceivedNote);

            _fixture.MockGoodsReceivedNoteItemService.Setup(x => x.AddGoodsReceivedNoteItemAsync(It.IsAny<GoodsReceivedNoteItem>()));

            var repository = new GoodsReceivedNoteRepository(AutoMapperSingleton.Mapper, _fixture.MockGoodsReceivedNoteService.Object,
                _fixture.MockUserAccessorService.Object, _fixture.MockPurchaseOrderService.Object, _fixture.MockPurchaseOrderItemService.Object,
                _fixture.MockGoodsReceivedNoteItemService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.CreateGoodsReceivedNoteAsync(new CreateGoodsReceivedNoteDto
            {
                PurchaseOrderId = 6,
                InvoiceNumber = "INV_20210224_01",
                Vat = 0.7m,
                Discount = 1.4m,
                Nbt = 0.7m,
                PaymentTypeId = 1,
                ReceivedBy = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa3916b9"),
                ReceivedDate = DateTime.Now.AddDays(-5)
            }));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.BadRequest);
            exception.ErrorMessage.Should().Be("GRN already created for this purchase order.");
            exception.ErrorType.Should().Be(HttpStatusCode.BadRequest.ToString());
        }

        [Fact]
        public async Task UpdateGoodsReceivedNoteAsync_Returns_Updated_GetGoodsReceivedNoteDto()
        {
            //Arrange
            var id = 1;
            _fixture.MockGoodsReceivedNoteService.Setup(x => x.GetGoodsReceivedNoteAsync(It.IsAny<Expression<Func<GoodsReceivedNote, bool>>>()))
                .Returns<Expression<Func<GoodsReceivedNote, bool>>>(expression => Task.FromResult(_fixture.GoodsReceivedNotes.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockGoodsReceivedNoteService.Setup(x => x.UpdateGoodsReceivedNoteAsync(It.IsAny<GoodsReceivedNote>()));

            var repository = new GoodsReceivedNoteRepository(AutoMapperSingleton.Mapper, _fixture.MockGoodsReceivedNoteService.Object,
                _fixture.MockUserAccessorService.Object, _fixture.MockPurchaseOrderService.Object, _fixture.MockPurchaseOrderItemService.Object,
                _fixture.MockGoodsReceivedNoteItemService.Object);

            //Act
            var result = await repository.UpdateGoodsReceivedNoteAsync(id, _fixture.EditGoodsReceivedNoteDto);

            //Assert
            result.Should().BeOfType(typeof(GetGoodsReceivedNoteDto));
            result.Id.Should().Be(id);
            result.PurchaseOrderNumber.Should().Be("PO_20210210_8d8c510caee6a4b");
            result.InvoiceNumber.Should().Be("INV_20210132_01");
            result.ApprovalStatus.Should().Be("Pending");
            result.Vat.Should().Be(0.75m);
            result.Discount.Should().Be(10);
            result.Nbt.Should().Be(0.6m);
        }

        [Fact]
        public async Task UpdateGoodsReceivedNoteAsync_Throws_NotFoundException()
        {
            //Arrange
            var id = 201;
            _fixture.MockGoodsReceivedNoteService.Setup(x => x.GetGoodsReceivedNoteAsync(It.IsAny<Expression<Func<GoodsReceivedNote, bool>>>()))
                .Returns<Expression<Func<GoodsReceivedNote, bool>>>(expression => Task.FromResult(_fixture.GoodsReceivedNotes.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockGoodsReceivedNoteService.Setup(x => x.UpdateGoodsReceivedNoteAsync(It.IsAny<GoodsReceivedNote>()));

            var repository = new GoodsReceivedNoteRepository(AutoMapperSingleton.Mapper, _fixture.MockGoodsReceivedNoteService.Object,
                _fixture.MockUserAccessorService.Object, _fixture.MockPurchaseOrderService.Object, _fixture.MockPurchaseOrderItemService.Object,
                _fixture.MockGoodsReceivedNoteItemService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.UpdateGoodsReceivedNoteAsync(id, _fixture.EditGoodsReceivedNoteDto));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound);
            exception.ErrorMessage.Should().Be("Goods received note not found.");
            exception.ErrorType.Should().Be(HttpStatusCode.NotFound.ToString());
        }

        [Fact]
        public async Task ApprovalGoodsReceivedNoteAsync_Throws_User_BadRequestException()
        {
            //Arrange
            var id = 3;
            _fixture.MockGoodsReceivedNoteService.Setup(x => x.GetGoodsReceivedNoteAsync(It.IsAny<Expression<Func<GoodsReceivedNote, bool>>>()))
                .Returns<Expression<Func<GoodsReceivedNote, bool>>>(expression => Task.FromResult(_fixture.GoodsReceivedNotes.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockUserAccessorService.Setup(x => x.GetCurrentUser()).Returns(_fixture.NullCurrentUser);

            _fixture.MockGoodsReceivedNoteService.Setup(x => x.UpdateGoodsReceivedNoteAsync(It.IsAny<GoodsReceivedNote>()));

            var repository = new GoodsReceivedNoteRepository(AutoMapperSingleton.Mapper, _fixture.MockGoodsReceivedNoteService.Object,
                _fixture.MockUserAccessorService.Object, _fixture.MockPurchaseOrderService.Object, _fixture.MockPurchaseOrderItemService.Object,
                _fixture.MockGoodsReceivedNoteItemService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.ApprovalGoodsReceivedNoteAsync(id, _fixture.ApprovalGoodsReceivedNoteDto));

            //Assert
            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.BadRequest);
            exception.ErrorMessage.Should().Be("User details not found. Login again.");
            exception.ErrorType.Should().Be(HttpStatusCode.BadRequest.ToString());
        }

        [Fact]
        public async Task ApprovalGoodsReceivedNoteAsync_Throws_UserId_BadRequestException()
        {
            //Arrange
            var id = 3;
            _fixture.MockGoodsReceivedNoteService.Setup(x => x.GetGoodsReceivedNoteAsync(It.IsAny<Expression<Func<GoodsReceivedNote, bool>>>()))
                .Returns<Expression<Func<GoodsReceivedNote, bool>>>(expression => Task.FromResult(_fixture.GoodsReceivedNotes.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockUserAccessorService.Setup(x => x.GetCurrentUser()).Returns(_fixture.EmptyUserIdCurrentUser);

            _fixture.MockGoodsReceivedNoteService.Setup(x => x.UpdateGoodsReceivedNoteAsync(It.IsAny<GoodsReceivedNote>()));

            var repository = new GoodsReceivedNoteRepository(AutoMapperSingleton.Mapper, _fixture.MockGoodsReceivedNoteService.Object,
                _fixture.MockUserAccessorService.Object, _fixture.MockPurchaseOrderService.Object, _fixture.MockPurchaseOrderItemService.Object,
                _fixture.MockGoodsReceivedNoteItemService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.ApprovalGoodsReceivedNoteAsync(id, _fixture.ApprovalGoodsReceivedNoteDto));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.BadRequest);
            exception.ErrorMessage.Should().Be("User details not found. Login again.");
            exception.ErrorType.Should().Be(HttpStatusCode.BadRequest.ToString());
        }

        [Fact]
        public async Task ApprovalGoodsReceivedNoteAsync_Returns_Approval_GetGoodsReceivedNoteDto()
        {
            //Arrange
            var id = 3;
            _fixture.MockGoodsReceivedNoteService.Setup(x => x.GetGoodsReceivedNoteAsync(It.IsAny<Expression<Func<GoodsReceivedNote, bool>>>()))
                .Returns<Expression<Func<GoodsReceivedNote, bool>>>(expression => Task.FromResult(_fixture.GoodsReceivedNotes.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockUserAccessorService.Setup(x => x.GetCurrentUser()).Returns(_fixture.CurrentUser);

            _fixture.MockGoodsReceivedNoteService.Setup(x => x.UpdateGoodsReceivedNoteAsync(It.IsAny<GoodsReceivedNote>()));

            var repository = new GoodsReceivedNoteRepository(AutoMapperSingleton.Mapper, _fixture.MockGoodsReceivedNoteService.Object,
                _fixture.MockUserAccessorService.Object, _fixture.MockPurchaseOrderService.Object, _fixture.MockPurchaseOrderItemService.Object,
                _fixture.MockGoodsReceivedNoteItemService.Object);

            //Act
            var result = await repository.ApprovalGoodsReceivedNoteAsync(id, _fixture.ApprovalGoodsReceivedNoteDto);

            //Assert
            result.Should().BeOfType(typeof(GetGoodsReceivedNoteDto));
            result.Id.Should().Be(id);
            result.PurchaseOrderNumber.Should().Be("PO_20210224_8d8c510caee6a4b");
            result.InvoiceNumber.Should().Be("INV_20210224_01");
            result.ApprovalStatus.Should().Be("Approved");
            result.Vat.Should().Be(0.5m);
            result.Discount.Should().Be(0.5m);
            result.Nbt.Should().Be(0.5m);
        }

        [Fact]
        public async Task DeleteGoodsReceivedNoteAsync_Returns_NoResult()
        {
            //Arrange
            var id = 2;
            _fixture.MockGoodsReceivedNoteService.Setup(x => x.GetGoodsReceivedNoteAsync(It.IsAny<Expression<Func<GoodsReceivedNote, bool>>>()))
                .Returns<Expression<Func<GoodsReceivedNote, bool>>>(expression => Task.FromResult(_fixture.GoodsReceivedNotes.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockGoodsReceivedNoteService.Setup(x => x.DeleteGoodsReceivedNoteAsync(It.IsAny<GoodsReceivedNote>()));

            var repository = new GoodsReceivedNoteRepository(AutoMapperSingleton.Mapper, _fixture.MockGoodsReceivedNoteService.Object,
                _fixture.MockUserAccessorService.Object, _fixture.MockPurchaseOrderService.Object, _fixture.MockPurchaseOrderItemService.Object,
                _fixture.MockGoodsReceivedNoteItemService.Object);

            //Act
            await repository.DeleteGoodsReceivedNoteAsync(id);

            // Assert
            _fixture.MockGoodsReceivedNoteService.Verify(x => x.DeleteGoodsReceivedNoteAsync(It.IsAny<GoodsReceivedNote>()), Times.Once);
        }

        [Fact]
        public async Task DeleteGoodsReceivedNoteAsync_Throws_NotFoundException()
        {
            //Arrange
            var id = 201;
            _fixture.MockGoodsReceivedNoteService.Setup(x => x.GetGoodsReceivedNoteAsync(It.IsAny<Expression<Func<GoodsReceivedNote, bool>>>()))
                .Returns<Expression<Func<GoodsReceivedNote, bool>>>(expression => Task.FromResult(_fixture.GoodsReceivedNotes.AsQueryable().FirstOrDefault(expression)));

            _fixture.MockGoodsReceivedNoteService.Setup(x => x.DeleteGoodsReceivedNoteAsync(It.IsAny<GoodsReceivedNote>()));

            var repository = new GoodsReceivedNoteRepository(AutoMapperSingleton.Mapper, _fixture.MockGoodsReceivedNoteService.Object,
                _fixture.MockUserAccessorService.Object, _fixture.MockPurchaseOrderService.Object, _fixture.MockPurchaseOrderItemService.Object,
                _fixture.MockGoodsReceivedNoteItemService.Object);

            //Act
            var exception = await Assert.ThrowsAsync<RestException>(() => repository.DeleteGoodsReceivedNoteAsync(id));

            //Assert
            exception.ErrorCode.Should().Be(HttpStatusCode.NotFound);
            exception.ErrorMessage.Should().Be("Goods received note not found.");
            exception.ErrorType.Should().Be(HttpStatusCode.NotFound.ToString());
        }
    }
}
