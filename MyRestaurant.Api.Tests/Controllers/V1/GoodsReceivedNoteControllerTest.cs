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
    public class GoodsReceivedNoteControllerTest : IClassFixture<GoodsReceivedNoteControllerFixture>
    {
        private readonly GoodsReceivedNoteControllerFixture _fixture;
        public GoodsReceivedNoteControllerTest(GoodsReceivedNoteControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async void GetGoodsReceivedNotes_ReturnsOkObjectResult()
        {
            //Arrange
            _fixture.MockGoodsReceivedNoteRepository.Setup(x => x.GetGoodsReceivedNotesAsync())
                .ReturnsAsync(_fixture.GoodsReceivedNotes);

            var controller = new GoodsReceivedNoteController(_fixture.MockGoodsReceivedNoteRepository.Object);

            //Act
            var result = await controller.GetGoodsReceivedNotes();

            //Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var grn = okResult.Value.Should().BeAssignableTo<IEnumerable<GetGoodsReceivedNoteDto>>().Subject;

            okResult.StatusCode.Should().Be(200);
            grn.Should().HaveCount(2);
        }

        [Fact]
        public async void GetGoodsReceivedNote_ReturnsOkObjectResult()
        {
            //Arrange
            var id = 2;
            _fixture.MockGoodsReceivedNoteRepository.Setup(x => x.GetGoodsReceivedNoteAsync(id))
                .ReturnsAsync(_fixture.GoodsReceivedNotes.Single(d => d.Id == id));

            var controller = new GoodsReceivedNoteController(_fixture.MockGoodsReceivedNoteRepository.Object);

            //Act
            var result = await controller.GetGoodsReceivedNote(id);

            //Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var grn = okResult.Value.Should().BeAssignableTo<GetGoodsReceivedNoteDto>().Subject;

            okResult.StatusCode.Should().Be(200);
            grn.Id.Should().Be(id);
            grn.PurchaseOrderNumber.Should().Be("PO_20210206_8d8c510caee6a4b");
            grn.InvoiceNumber.Should().Be("INV_20210206_02");
            grn.ApprovalStatus.Should().Be("Approved");
        }

        [Fact]
        public async void CreateGoodsReceivedNote_ReturnCreatedAtRouteResult()
        {
            //Arrange
            _fixture.MockGoodsReceivedNoteRepository.Setup(x => x.CreateGoodsReceivedNoteAsync(_fixture.ValidCreateGoodsReceivedNoteDto))
                .ReturnsAsync(_fixture.CreateGoodsReceivedNoteDtoResult);

            var controller = new GoodsReceivedNoteController(_fixture.MockGoodsReceivedNoteRepository.Object);

            //Act
            var result = await controller.CreateGoodsReceivedNote(_fixture.ValidCreateGoodsReceivedNoteDto, _fixture.ApiVersion);

            //Assert
            var objectResult = result.Should().BeOfType<CreatedAtRouteResult>().Subject;
            objectResult.StatusCode.Should().Be(201);
            objectResult.RouteValues["id"].Should().Be(3);
            objectResult.RouteValues["version"].Should().Be($"{_fixture.ApiVersion}");

            var grn = objectResult.Value.Should().BeAssignableTo<GetGoodsReceivedNoteDto>().Subject;
            grn.Id.Should().Be(3);
            grn.PurchaseOrderNumber.Should().Be("PO_20210216_8d8c510caee6a4b");
            grn.InvoiceNumber.Should().Be("INV_20210216_03");
            grn.ApprovalStatus.Should().Be("Pending");
        }

        [Fact]
        public async void UpdateGoodsReceivedNote_ReturnsOkObjectResult()
        {
            //Arrange
            var id = 1;
            _fixture.MockGoodsReceivedNoteRepository.Setup(x => x.UpdateGoodsReceivedNoteAsync(id, _fixture.ValidEditGoodsReceivedNoteDto))
                .ReturnsAsync(_fixture.EditGoodsReceivedNoteDtoResult);

            var controller = new GoodsReceivedNoteController(_fixture.MockGoodsReceivedNoteRepository.Object);

            //Act
            var result = await controller.UpdateGoodsReceivedNote(id, _fixture.ValidEditGoodsReceivedNoteDto);

            //Assert
            var objectResult = result.Should().BeOfType<OkObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);

            var grn = objectResult.Value.Should().BeAssignableTo<GetGoodsReceivedNoteDto>().Subject;
            grn.Id.Should().Be(1);
            grn.PurchaseOrderNumber.Should().Be("PO_20210130_8d8c510caee6a4b");
            grn.InvoiceNumber.Should().Be("INV_20210132_01");
            grn.ApprovalStatus.Should().Be("Pending");
            grn.Nbt.Should().Be(0.6m);
            grn.Vat.Should().Be(0.75m);
            grn.Discount.Should().Be(10);
        }

        [Fact]
        public async void ApproveGoodsReceivedNote_ReturnsOkObjectResult()
        {
            //Arrange
            var id = 1;
            _fixture.MockGoodsReceivedNoteRepository.Setup(x => x.ApprovalGoodsReceivedNoteAsync(id, _fixture.ValidApprovalGoodsReceivedNoteDto))
                .ReturnsAsync(_fixture.ApprovalGoodsReceivedNoteDtoResult);

            var controller = new GoodsReceivedNoteController(_fixture.MockGoodsReceivedNoteRepository.Object);

            //Act
            var result = await controller.ApproveGoodsReceivedNote(id, _fixture.ValidApprovalGoodsReceivedNoteDto);

            //Assert
            var objectResult = result.Should().BeOfType<OkObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);

            var grn = objectResult.Value.Should().BeAssignableTo<GetGoodsReceivedNoteDto>().Subject;
            grn.Id.Should().Be(1);
            grn.PurchaseOrderNumber.Should().Be("PO_20210130_8d8c510caee6a4b");
            grn.InvoiceNumber.Should().Be("INV_20210132_01");
            grn.ApprovalStatus.Should().Be("Rejected");
            grn.Nbt.Should().Be(0.5m);
            grn.Vat.Should().Be(0.5m);
            grn.Discount.Should().Be(0.5m);
        }

        [Fact]
        public async void DeleteGoodsReceivedNote_ReturnNoContentResult()
        {
            //Arrange
            var id = 2;
            _fixture.MockGoodsReceivedNoteRepository.Setup(x => x.DeleteGoodsReceivedNoteAsync(id));

            var controller = new GoodsReceivedNoteController(_fixture.MockGoodsReceivedNoteRepository.Object);

            //Act
            var result = await controller.DeleteGoodsReceivedNote(id);

            //Assert
            var objectResult = result.Should().BeOfType<NoContentResult>().Subject;
            objectResult.StatusCode.Should().Be(204);
        }
    }
}
