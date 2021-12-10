using Microsoft.AspNetCore.Mvc;
using Moq;
using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Repositories.Contracts;
using System;
using System.Collections.Generic;

namespace MyRestaurant.Api.Tests.Controllers.V1.Fixtures
{
    public class PurchaseOrderControllerFixture : IDisposable
    {
        private bool _disposed;
        public ApiVersion ApiVersion { get; private set; }
        public Mock<IPurchaseOrderRepository> MockPurchaseOrderRepository { get; private set; }
        public IEnumerable<GetPurchaseOrderDto> PurchaseOrders { get; private set; }
        public CreatePurchaseOrderDto ValidCreatePurchaseOrderDto { get; private set; }
        public GetPurchaseOrderDto CreatePurchaseOrderDtoResult { get; private set; }
        public EditPurchaseOrderDto ValidEditPurchaseOrderDto { get; private set; }
        public GetPurchaseOrderDto EditPurchaseOrderDtoResult { get; private set; }
        public ApprovalPurchaseOrderDto ValidApprovalPurchaseOrderDto { get; private set; }
        public GetPurchaseOrderDto ApprovalPurchaseOrderDtoResult { get; private set; }

        public PurchaseOrderControllerFixture()
        {
            ApiVersion = new ApiVersion(1, 0);

            MockPurchaseOrderRepository = new Mock<IPurchaseOrderRepository>();

            PurchaseOrders = new List<GetPurchaseOrderDto>
            {
                new GetPurchaseOrderDto {
                    Id = 1,
                    OrderNumber = "PO_20210130_8d8c510caee6a4b",
                    SupplierId = 1,
                    SupplierName = "ABC Pvt Ldt",
                    RequestedUserId = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa3916b9"),
                    RequestedUserName = "Golden Dining",
                    RequestedDate = DateTime.Now.AddDays(-10),
                    ApprovalStatus = "Approved",
                    ApprovedUserId = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa3916b9"),
                    ApprovedUserName = "Golden Dining",
                    ApprovedDate = DateTime.Now,
                    Description = "Test"
                },
                new GetPurchaseOrderDto {
                    Id = 2,
                    OrderNumber = "PO_20210130_8d8c512f7cd7920",
                    SupplierId = 2,
                    SupplierName = "VBT Pvt Ltd",
                    RequestedUserId = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa3916b9"),
                    RequestedUserName = "Golden Dining",
                    RequestedDate = DateTime.Now.AddDays(-5),
                    ApprovalStatus = "Pending",
                    ApprovedUserId = Guid.Empty,
                    ApprovedUserName = null,
                    ApprovedDate = default,
                    Description = ""
                },
                new GetPurchaseOrderDto
                {
                    Id = 3,
                    OrderNumber = "PO_20210206_8d8caa8b86ce209",
                    SupplierId = 1,
                    SupplierName = "ABC Pvt Ldt",
                    RequestedUserId = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa3916b9"),
                    RequestedUserName = "Golden Dining",
                    RequestedDate = DateTime.Now.AddDays(-2),
                    ApprovalStatus = "Rejected",
                    ApprovedUserId = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa3916b9"),
                    ApprovedUserName = "Golden Dining",
                    ApprovedDate = DateTime.Now.AddDays(-1),
                    Description = "Test for reject"
                }
            };

            ValidCreatePurchaseOrderDto = new CreatePurchaseOrderDto
            {
                SupplierId = 1,
                Description = "Test create new PO"
            };

            CreatePurchaseOrderDtoResult = new GetPurchaseOrderDto
            {
                Id = 4,
                OrderNumber = "PO_20210216_8d8c512f7cd7920",
                SupplierId = 1,
                SupplierName = "ABC Pvt Ldt",
                RequestedUserId = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa3916b9"),
                RequestedUserName = "Golden Dining",
                RequestedDate = DateTime.Now,
                ApprovalStatus = "Pending",
                ApprovedUserId = Guid.Empty,
                ApprovedUserName = null,
                ApprovedDate = default,
                Description = ""
            };

            ValidEditPurchaseOrderDto = new EditPurchaseOrderDto
            {
                SupplierId = 1,
                Description = "Changed supplier"
            };

            EditPurchaseOrderDtoResult = new GetPurchaseOrderDto
            {
                Id = 2,
                OrderNumber = "PO_20210130_8d8c512f7cd7920",
                SupplierId = 1,
                SupplierName = "ABC Pvt Ldt",
                RequestedUserId = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa3916b9"),
                RequestedUserName = "Golden Dining",
                RequestedDate = DateTime.Now.AddDays(-5),
                ApprovalStatus = "Pending",
                ApprovedUserId = Guid.Empty,
                ApprovedUserName = null,
                ApprovedDate = default,
                Description = "Changed supplier"
            };

            ValidApprovalPurchaseOrderDto = new ApprovalPurchaseOrderDto
            {
                ApprovalStatus = "Approved",
                ApprovalReason = "All the items are required."
            };

            ApprovalPurchaseOrderDtoResult = new GetPurchaseOrderDto
            {
                Id = 2,
                OrderNumber = "PO_20210130_8d8c512f7cd7920",
                SupplierId = 1,
                SupplierName = "ABC Pvt Ldt",
                RequestedUserId = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa3916b9"),
                RequestedUserName = "Golden Dining",
                RequestedDate = DateTime.Now.AddDays(-5),
                ApprovalStatus = "Approved",
                ApprovedUserId = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa3916b9"),
                ApprovedUserName = "Golden Dining",
                ApprovedDate = DateTime.Now,
                Description = "Changed supplier"
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
                    MockPurchaseOrderRepository = null;
                }

                _disposed = true;
            }
        }
    }
}
