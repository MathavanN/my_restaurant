using Microsoft.AspNetCore.Mvc;
using Moq;
using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Repositories.Contracts;
using System;
using System.Collections.Generic;

namespace MyRestaurant.Api.Tests.Controllers.V1.Fixtures
{
    public class GoodsReceivedNoteControllerFixture : IDisposable
    {
        private bool _disposed;
        public ApiVersion ApiVersion { get; private set; }
        public Mock<IGoodsReceivedNoteRepository> MockGoodsReceivedNoteRepository { get; private set; }
        public IEnumerable<GetGoodsReceivedNoteDto> GoodsReceivedNotes { get; private set; }
        public CreateGoodsReceivedNoteDto ValidCreateGoodsReceivedNoteDto { get; private set; }
        public GetGoodsReceivedNoteDto CreateGoodsReceivedNoteDtoResult { get; private set; }
        public EditGoodsReceivedNoteDto ValidEditGoodsReceivedNoteDto { get; private set; }
        public GetGoodsReceivedNoteDto EditGoodsReceivedNoteDtoResult { get; private set; }
        public ApprovalGoodsReceivedNoteDto ValidApprovalGoodsReceivedNoteDto { get; private set; }
        public GetGoodsReceivedNoteDto ApprovalGoodsReceivedNoteDtoResult { get; private set; }

        public GoodsReceivedNoteControllerFixture()
        {
            ApiVersion = new ApiVersion(1, 0);

            MockGoodsReceivedNoteRepository = new Mock<IGoodsReceivedNoteRepository>();

            GoodsReceivedNotes = new List<GetGoodsReceivedNoteDto>
            {
                new GetGoodsReceivedNoteDto {
                    Id = 1,
                    PurchaseOrderId = 1,
                    PurchaseOrderNumber = "PO_20210130_8d8c510caee6a4b",
                    InvoiceNumber = "INV_20210132_01",
                    PaymentTypeId = 1,
                    PaymentTypeName = "Cash",
                    Nbt = 0.5m,
                    Vat = 0.5m,
                    Discount = 0.5m,
                    ReceivedBy = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa3916b9"),
                    ReceivedUserName = "Golden Dining",
                    ReceivedDate = DateTime.Now.AddDays(-5),
                    ApprovalStatus = "Pending",
                    ApprovedBy = Guid.Empty,
                    ApprovedUserName = null,
                    ApprovedDate = default,
                    CreatedDate = DateTime.Now
                },
                new GetGoodsReceivedNoteDto {
                    Id = 2,
                    PurchaseOrderId = 2,
                    PurchaseOrderNumber = "PO_20210206_8d8c510caee6a4b",
                    InvoiceNumber = "INV_20210206_02",
                    PaymentTypeId = 1,
                    PaymentTypeName = "Cash",
                    Nbt = 0,
                    Vat = 0,
                    Discount = 0,
                    ReceivedBy = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa3916b9"),
                    ReceivedUserName = "Golden Dining",
                    ReceivedDate = DateTime.Now.AddDays(-10),
                    ApprovalStatus = "Approved",
                    ApprovedBy = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa3916b9"),
                    ApprovedUserName = "Golden Dining",
                    ApprovedDate = DateTime.Now.AddDays(-8),
                    CreatedDate = DateTime.Now.AddDays(-9)
                }
            };

            ValidCreateGoodsReceivedNoteDto = new CreateGoodsReceivedNoteDto
            {
                PurchaseOrderId = 3,
                InvoiceNumber = "INV_20210216_03",
                Vat = 0.7m,
                Discount = 1.4m,
                Nbt = 0.7m,
                PaymentTypeId = 1,
                ReceivedBy = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa3916b9"),
                ReceivedDate = DateTime.Now.AddDays(-1)
            };

            CreateGoodsReceivedNoteDtoResult = new GetGoodsReceivedNoteDto
            {
                Id = 3,
                PurchaseOrderId = 3,
                PurchaseOrderNumber = "PO_20210216_8d8c510caee6a4b",
                InvoiceNumber = "INV_20210216_03",
                Vat = 0.7m,
                Discount = 1.4m,
                Nbt = 0.7m,
                PaymentTypeId = 1,
                PaymentTypeName = "Cash",
                ReceivedBy = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa3916b9"),
                ReceivedDate = DateTime.Now.AddDays(-1),
                ReceivedUserName = "Golden Dining",
                ApprovalStatus = "Pending",
                ApprovedBy = Guid.Empty,
                ApprovedUserName = null,
                ApprovedDate = default,
                CreatedDate = DateTime.Now
            };

            ValidEditGoodsReceivedNoteDto = new EditGoodsReceivedNoteDto
            {
                PurchaseOrderId = 2,
                PaymentTypeId = 1,
                InvoiceNumber = "INV_20210206_02",
                Nbt = 0.6m,
                Vat = 0.75m,
                Discount = 10,
                ReceivedBy = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa3916b9"),
                ReceivedDate = DateTime.Now.AddDays(-10),
            };

            EditGoodsReceivedNoteDtoResult = new GetGoodsReceivedNoteDto
            {
                Id = 1,
                PurchaseOrderId = 2,
                PurchaseOrderNumber = "PO_20210130_8d8c510caee6a4b",
                InvoiceNumber = "INV_20210132_01",
                PaymentTypeId = 1,
                PaymentTypeName = "Cash",
                Nbt = 0.6m,
                Vat = 0.75m,
                Discount = 10,
                ReceivedBy = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa3916b9"),
                ReceivedUserName = "Golden Dining",
                ReceivedDate = DateTime.Now.AddDays(-5),
                ApprovalStatus = "Pending",
                ApprovedBy = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa3916b9"),
                ApprovedUserName = "Golden Dining",
                ApprovedDate = DateTime.Now.AddDays(-3),
                CreatedDate = DateTime.Now.AddDays(-4)
            };

            ValidApprovalGoodsReceivedNoteDto = new ApprovalGoodsReceivedNoteDto
            {
                ApprovalStatus = "Rejected",
                ApprovalReason = "Already GRN created."
            };

            ApprovalGoodsReceivedNoteDtoResult = new GetGoodsReceivedNoteDto
            {
                Id = 1,
                PurchaseOrderId = 1,
                PurchaseOrderNumber = "PO_20210130_8d8c510caee6a4b",
                InvoiceNumber = "INV_20210132_01",
                PaymentTypeId = 1,
                PaymentTypeName = "Cash",
                Nbt = 0.5m,
                Vat = 0.5m,
                Discount = 0.5m,
                ReceivedBy = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa3916b9"),
                ReceivedUserName = "Golden Dining",
                ReceivedDate = DateTime.Now.AddDays(-5),
                ApprovalStatus = "Rejected",
                ApprovedBy = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa3916b9"),
                ApprovedUserName = "Golden Dining",
                ApprovedDate = DateTime.Now,
                CreatedDate = DateTime.Now
            };
        }
        
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    MockGoodsReceivedNoteRepository = null;
                }

                _disposed = true;
            }
        }
    }
}
