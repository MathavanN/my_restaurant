using Moq;
using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Models;
using MyRestaurant.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyRestaurant.Business.Tests.Repositories.Fixtures
{
    public class PurchaseOrderRepositoryFixture : IDisposable
    {
        public Mock<IPurchaseOrderService> MockPurchaseOrderService { get; private set; }
        public Mock<IUserAccessorService> MockUserAccessorService { get; private set; }
        public IEnumerable<PurchaseOrder> PurchaseOrders { get; private set; }
        public IEnumerable<Supplier> Suppliers { get; private set; }
        public CreatePurchaseOrderDto CreatePurchaseOrderDto { get; private set; }
        public CurrentUser CurrentUser { get; private set; }
        public CurrentUser NullCurrentUser { get; private set; }
        public CurrentUser EmptyUserIdCurrentUser { get; private set; }
        public PurchaseOrder CreatedNewPurchaseOrder { get; private set; }
        public EditPurchaseOrderDto EditPurchaseOrderDto { get; private set; }
        public PurchaseOrder UpdatedPurchaseOrder { get; private set; }
        public ApprovalPurchaseOrderDto ApprovalPurchaseOrderDto { get; private set; }
        public PurchaseOrder ApprovedPurchaseOrder { get; private set; }
        public PurchaseOrderRepositoryFixture()
        {
            MockPurchaseOrderService = new Mock<IPurchaseOrderService>();

            MockUserAccessorService = new Mock<IUserAccessorService>();

            Suppliers = new List<Supplier>
            {
                new Supplier
                {
                    Id = 1,
                    Name = "ABC Pvt Ltd",
                    Address1 = "American Mission School Road",
                    Address2 = "Madduvil South",
                    City = "Chavakachcheri",
                    Country = "Sri Lanka",
                    Telephone1 = "0765554345",
                    Telephone2 = "0766554567",
                    Fax = "",
                    Email = "goldendining2010@gmail.com",
                    ContactPerson = "James"
                },
                new Supplier
                {
                    Id = 2,
                    Name = "VBT Pvt Ltd",
                    Address1 = "VBT Road",
                    Address2 = "VBTt",
                    City = "Jaffna",
                    Country = "Sri Lanka",
                    Telephone1 = "0777113644",
                    Telephone2 = "",
                    Fax = "",
                    Email = "test@test.com",
                    ContactPerson = "James"
                }
            };

            PurchaseOrders = new List<PurchaseOrder>
            {
                new PurchaseOrder {
                    Id = 1,
                    OrderNumber = "PO_20210130_8d8c510caee6a4b",
                    SupplierId = 1,
                    Supplier = Suppliers.FirstOrDefault(d => d.Id == 1),
                    RequestedBy = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa3916b9"),
                    RequestedDate = DateTime.Now.AddDays(-10),
                    ApprovalStatus = Status.Approved,
                    ApprovedBy = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa3916b9"),
                    ApprovedDate = DateTime.Now,
                    Description = "Test",
                    ApprovalReason = "Items are required"
                },
                new PurchaseOrder {
                    Id = 2,
                    OrderNumber = "PO_20210130_8d8c512f7cd7920",
                    SupplierId = 2,
                    Supplier = Suppliers.FirstOrDefault(d => d.Id == 2),
                    RequestedBy = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa3916b9"),
                    RequestedDate = DateTime.Now.AddDays(-5),
                    ApprovalStatus = Status.Pending,
                    ApprovedBy = Guid.Empty,
                    ApprovedDate = default,
                    Description = "",
                    ApprovalReason = ""
                },
                new PurchaseOrder
                {
                    Id = 3,
                    OrderNumber = "PO_20210206_8d8caa8b86ce209",
                    SupplierId = 1,
                    Supplier = Suppliers.FirstOrDefault(d => d.Id == 1),
                    RequestedBy = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa3916b9"),
                    RequestedDate = DateTime.Now.AddDays(-2),
                    ApprovalStatus = Status.Rejected,
                    ApprovedBy = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa3916b9"),
                    ApprovedDate = DateTime.Now.AddDays(-1),
                    ApprovalReason = "Test for reject",
                    Description = "Test Description"
                },
                new PurchaseOrder
                {
                    Id = 4,
                    OrderNumber = "PO_20210227_8d8caa8b86ce209",
                    SupplierId = 1,
                    Supplier = Suppliers.FirstOrDefault(d => d.Id == 1),
                    RequestedBy = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa3916b9"),
                    RequestedUser = new User
                    {
                        Id = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa3916b9"),
                        FirstName = "Golden",
                        LastName = "Dining",
                    },
                    RequestedDate = DateTime.Now.AddDays(-2),
                    ApprovalStatus = Status.Approved,
                    ApprovedBy = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa3916b9"),
                    ApprovedUser = new User
                    {
                        Id = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa3916b9"),
                        FirstName = "Golden",
                        LastName = "Dining",
                    },
                    ApprovedDate = DateTime.Now.AddDays(-1),
                    ApprovalReason = "Items required",
                    Description = "Test Description",
                    GoodsReceivedNotes = new HashSet<GoodsReceivedNote>()
                }
            };

            CreatePurchaseOrderDto = new CreatePurchaseOrderDto
            {
                SupplierId = 2,
                Description = "Test create new PO"
            };

            CreatedNewPurchaseOrder = new PurchaseOrder
            {
                Id = 5,
                OrderNumber = "PO_20210227_8d8c512f7cd7920",
                SupplierId = 2,
                Supplier = Suppliers.FirstOrDefault(d => d.Id == 2),
                RequestedBy = Guid.Parse("33a91077-ef90-40a1-be42-27354f598c20"),
                RequestedDate = DateTime.Now,
                ApprovalStatus = Status.Pending,
                ApprovedBy = Guid.Empty,
                ApprovedDate = default,
                Description = "",
                ApprovalReason = ""
            };

            CurrentUser = new CurrentUser
            {
                Email = "abc@gmail.com",
                FirstName = "Victor",
                LastName = "Kamal",
                Roles = new List<string> { "Admin", "Report", "Normal" },
                UserId = Guid.Parse("33a91077-ef90-40a1-be42-27354f598c20")
            };

            NullCurrentUser = null;

            EmptyUserIdCurrentUser = new CurrentUser
            {
                UserId = Guid.Empty,
                Email = "abc@gmail.com",
                Roles = new List<string> { "SuperAdmin", "Admin", "Report", "Normal" },
                FirstName = "Golden",
                LastName = "Dining",
            };

            EditPurchaseOrderDto = new EditPurchaseOrderDto
            {
                SupplierId = 1,
                Description = "Supplier Changed"
            };

            UpdatedPurchaseOrder = PurchaseOrders.FirstOrDefault(d => d.Id == 2);
            UpdatedPurchaseOrder.SupplierId = EditPurchaseOrderDto.SupplierId;
            UpdatedPurchaseOrder.Description = EditPurchaseOrderDto.Description;
            UpdatedPurchaseOrder.Supplier = Suppliers.FirstOrDefault(d => d.Id == EditPurchaseOrderDto.SupplierId);

            ApprovalPurchaseOrderDto = new ApprovalPurchaseOrderDto
            {
                ApprovalReason = "Items are not required",
                ApprovalStatus = "Rejected"
            };

            ApprovedPurchaseOrder = PurchaseOrders.FirstOrDefault(d => d.Id == 2);
            ApprovedPurchaseOrder.ApprovalStatus = Status.Rejected;
            ApprovedPurchaseOrder.ApprovedBy = CurrentUser.UserId;
            ApprovedPurchaseOrder.ApprovalReason = ApprovalPurchaseOrderDto.ApprovalReason;
            ApprovedPurchaseOrder.ApprovedDate = DateTime.Now;
        }

        public void Dispose()
        {
            MockPurchaseOrderService = null;
        }
    }
}
