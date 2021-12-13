using Moq;
using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Models;
using MyRestaurant.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyRestaurant.Business.Tests.Repositories.Fixtures
{
    public class GoodsReceivedNoteRepositoryFixture : IDisposable
    {
        private bool _disposed;
        public Mock<IGoodsReceivedNoteService> MockGoodsReceivedNoteService { get; private set; }
        public Mock<IPurchaseOrderService> MockPurchaseOrderService { get; private set; }
        public Mock<IPurchaseOrderItemService> MockPurchaseOrderItemService { get; private set; }
        public Mock<IGoodsReceivedNoteItemService> MockGoodsReceivedNoteItemService { get; private set; }
        public Mock<IUserAccessorService> MockUserAccessorService { get; private set; }
        public IEnumerable<GoodsReceivedNote> GoodsReceivedNotes { get; private set; }
        public IEnumerable<PurchaseOrder> PurchaseOrders { get; private set; }
        public CreateGoodsReceivedNoteDto CreateGoodsReceivedNote { get; private set; }
        public GoodsReceivedNote CreateNewGoodsReceivedNote { get; private set; }
        public CurrentUser CurrentUser { get; private set; }
        public CurrentUser? NullCurrentUser { get; private set; }
        public CurrentUser EmptyUserIdCurrentUser { get; private set; }
        public IEnumerable<PurchaseOrderItem> PurchaseOrderItems { get; private set; }
        public EditGoodsReceivedNoteDto EditGoodsReceivedNoteDto { get; private set; }
        public ApprovalGoodsReceivedNoteDto ApprovalGoodsReceivedNoteDto { get; private set; }
        public GoodsReceivedNoteRepositoryFixture()
        {
            MockGoodsReceivedNoteService = new Mock<IGoodsReceivedNoteService>();
            MockPurchaseOrderService = new Mock<IPurchaseOrderService>();
            MockPurchaseOrderItemService = new Mock<IPurchaseOrderItemService>();
            MockGoodsReceivedNoteItemService = new Mock<IGoodsReceivedNoteItemService>();
            MockUserAccessorService = new Mock<IUserAccessorService>();

            PurchaseOrders = new List<PurchaseOrder>
            {
                new PurchaseOrder
                {
                    Id = 1,
                    OrderNumber = "PO_20210210_8d8c510caee6a4b",
                    SupplierId = 1,
                    Supplier = new Supplier
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
                },
                new PurchaseOrder
                {
                    Id = 2,
                    OrderNumber = "PO_20210130_8d8c510caee6a4b"
                },
                new PurchaseOrder
                {
                    Id = 3,
                    OrderNumber = "PO_20210227_8d8c510caee6a4b",
                    SupplierId = 1,
                    Supplier = new Supplier
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
                },
                new PurchaseOrder
                {
                    Id = 4,
                    OrderNumber = "PO_20210215_8d8c510caee6a4b",
                    ApprovalStatus = Status.Pending
                },
                new PurchaseOrder
                {
                    Id = 5,
                    OrderNumber = "PO_20210224_8d8c510caee6a4b",
                    SupplierId = 1,
                    Supplier = new Supplier
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
                    RequestedBy = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa3916b9"),
                    RequestedUser = new User
                    {
                        Id = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa3916b9"),
                        FirstName = "Golden",
                        LastName = "Dining",
                    },
                    RequestedDate = DateTime.Now.AddDays(-1),
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
                },
                new PurchaseOrder
                {
                    Id = 6,
                    OrderNumber = "PO_20210223_8d8c510caee6a4b",
                    SupplierId = 1,
                    Supplier = new Supplier
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
                    RequestedBy = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa3916b9"),
                    RequestedUser = new User
                    {
                        Id = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa3916b9"),
                        FirstName = "Golden",
                        LastName = "Dining",
                    },
                    RequestedDate = DateTime.Now.AddDays(-3),
                    ApprovalStatus = Status.Approved,
                    ApprovedBy = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa3916b9"),
                    ApprovedUser = new User
                    {
                        Id = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa3916b9"),
                        FirstName = "Golden",
                        LastName = "Dining",
                    },
                    ApprovedDate = DateTime.Now.AddDays(-2),
                    ApprovalReason = "Items required",
                    Description = "Test Description"
                }
            };

            var paymentType = new PaymentType { Id = 1, Name = "Cash", CreditPeriod = 0 };

            var unitOfMeasure1 = new UnitOfMeasure { Id = 1, Code = "kg", Description = "" };
            var unitOfMeasure2 = new UnitOfMeasure { Id = 2, Code = "g", Description = "" };
            var unitOfMeasure3 = new UnitOfMeasure { Id = 3, Code = "l", Description = "" };
            var unitOfMeasure4 = new UnitOfMeasure { Id = 4, Code = "ml", Description = "" };
            var unitOfMeasure5 = new UnitOfMeasure { Id = 5, Code = "none", Description = "" };

            var stockType1 = new StockType { Id = 1, Type = "Grocery" };
            var stockType2 = new StockType { Id = 2, Type = "Beverage" };
            var stockType3 = new StockType { Id = 3, Type = "Stationery" };

            var stockItems = new List<StockItem>
            {
                new StockItem { Id = 20025, TypeId = 1, Type = stockType1, Name = "Rice", ItemUnit = 10, UnitOfMeasureId = 1, UnitOfMeasure = unitOfMeasure1 },
                new StockItem { Id = 20026, TypeId = 1, Type = stockType1, Name = "Chilli Powder", ItemUnit = 250, UnitOfMeasureId = 2, UnitOfMeasure = unitOfMeasure2 },
                new StockItem { Id = 20050, TypeId = 2, Type = stockType2, Name = "Water", ItemUnit = 1, UnitOfMeasureId = 3, UnitOfMeasure = unitOfMeasure3 },
                new StockItem { Id = 20024, TypeId = 3, Type = stockType3, Name = "Blue Pen", ItemUnit = 1, UnitOfMeasureId = 5, UnitOfMeasure = unitOfMeasure5 },
                new StockItem { Id = 20023, TypeId = 5, Type = stockType1, Name = "Rice", ItemUnit = 10, UnitOfMeasureId = 1, UnitOfMeasure = unitOfMeasure1 },
            };

            PurchaseOrderItems = new List<PurchaseOrderItem>
            {
                new PurchaseOrderItem {
                    Id = 1,
                    PurchaseOrderId = 1,
                    Item = stockItems.First(d => d.Id == 20025),
                    ItemId = 20025,
                    ItemUnitPrice = 540,
                    Quantity = 5
                },
                new PurchaseOrderItem {
                    Id = 2,
                    PurchaseOrderId = 2,
                    Item = stockItems.First(d => d.Id == 20026),
                    ItemId = 20026,
                    ItemUnitPrice = 30,
                    Quantity = 10
                },
                new PurchaseOrderItem {
                    Id = 3,
                    PurchaseOrderId = 3,
                    Item = stockItems.First(d => d.Id == 20050),
                    ItemId = 20050,
                    ItemUnitPrice = 50,
                    Quantity = 5
                },
                new PurchaseOrderItem {
                    Id = 4,
                    PurchaseOrderId = 3,
                    Item = stockItems.First(d => d.Id == 20024),
                    ItemId = 20024,
                    ItemUnitPrice = 260,
                    Quantity = 6
                }
            };

            GoodsReceivedNotes = new List<GoodsReceivedNote>
            {
                new GoodsReceivedNote {
                    Id = 1,
                    PurchaseOrderId = 1,
                    PurchaseOrder = PurchaseOrders.First(d => d.Id == 1),
                    InvoiceNumber = "INV_20210132_01",
                    PaymentTypeId = 1,
                    PaymentType = paymentType,
                    Nbt = 0.5m,
                    Vat = 0.5m,
                    Discount = 0.5m,
                    ReceivedBy = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa3916b9"),
                    ReceivedDate = DateTime.Now.AddDays(-5),
                    ReceivedUser = new User
                    {
                        Id = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa3916b9"),
                        FirstName = "Golden",
                        LastName = "Dining",
                    },
                    ApprovedBy = Guid.Empty,
                    ApprovalStatus = Status.Pending,
                    ApprovedDate = default,
                    ApprovalReason = "",
                    CreatedBy = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa3916b9"),
                    CreatedDate = DateTime.Now
                },
                new GoodsReceivedNote {
                    Id = 2,
                    PurchaseOrderId = 2,
                    PurchaseOrder = PurchaseOrders.First(d => d.Id == 2),
                    InvoiceNumber = "INV_20210206_02",
                    PaymentTypeId = 1,
                    PaymentType = paymentType,
                    Nbt = 0,
                    Vat = 0,
                    Discount = 0,
                    ReceivedBy = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa3916b9"),
                    ReceivedDate = DateTime.Now.AddDays(-10),
                    ReceivedUser = new User
                    {
                        Id = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa3916b9"),
                        FirstName = "Golden",
                        LastName = "Dining",
                    },
                    ApprovalStatus = Status.Approved,
                    ApprovedBy = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa3916b9"),
                    ApprovedUser = new User
                    {
                        Id = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa3916b9"),
                        FirstName = "Golden",
                        LastName = "Dining",
                    },
                    ApprovedDate = DateTime.Now.AddDays(-8),
                    CreatedBy = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa3916b9"),
                    CreatedDate = DateTime.Now.AddDays(-9)
                },
                new GoodsReceivedNote {
                    Id = 3,
                    PurchaseOrderId = 5,
                    PurchaseOrder = PurchaseOrders.First(d => d.Id == 5),
                    InvoiceNumber = "INV_20210224_01",
                    PaymentTypeId = 1,
                    PaymentType = paymentType,
                    Nbt = 0.5m,
                    Vat = 0.5m,
                    Discount = 0.5m,
                    ReceivedBy = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa3916b9"),
                    ReceivedDate = DateTime.Now.AddDays(-5),
                    ReceivedUser = new User
                    {
                        Id = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa3916b9"),
                        FirstName = "Golden",
                        LastName = "Dining",
                    },
                    ApprovedBy = Guid.Empty,
                    ApprovalStatus = Status.Pending,
                    ApprovedDate = default,
                    ApprovalReason = "",
                    CreatedBy = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa3916b9"),
                    CreatedDate = DateTime.Now
                },
                new GoodsReceivedNote {
                    Id = 4,
                    PurchaseOrderId = 6,
                    PurchaseOrder = PurchaseOrders.First(d => d.Id == 6),
                    InvoiceNumber = "INV_20210224_01",
                    PaymentTypeId = 1,
                    PaymentType = paymentType,
                    Vat = 0.7m,
                    Discount = 1.4m,
                    Nbt = 0.7m,
                    ReceivedBy = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa3916b9"),
                    ReceivedDate = DateTime.Now.AddMinutes(-60),
                    ReceivedUser = new User
                    {
                        Id = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa3916b9"),
                        FirstName = "Golden",
                        LastName = "Dining",
                    },
                    ApprovalStatus = Status.Approved,
                    ApprovedBy = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa3916b9"),
                    ApprovedDate = DateTime.Now.AddMinutes(-30),
                    ApprovedUser = new User
                    {
                        Id = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa3916b9"),
                        FirstName = "Golden",
                        LastName = "Dining",
                    },
                    ApprovalReason = "Item are received",
                    CreatedBy = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa3916b9"),
                    CreatedDate = DateTime.Now.AddMinutes(-60)
                }
            };

            CreateGoodsReceivedNote = new CreateGoodsReceivedNoteDto
            {
                PurchaseOrderId = 3,
                InvoiceNumber = "INV_20210228_03",
                Vat = 0.7m,
                Discount = 1.4m,
                Nbt = 0.7m,
                PaymentTypeId = 1,
                ReceivedBy = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa3916b9"),
                ReceivedDate = DateTime.Now.AddDays(-1)
            };

            CurrentUser = new CurrentUser
            {
                Email = "abc@gmail.com",
                Roles = new List<string> { "SuperAdmin", "Admin", "Report", "Normal" },
                UserId = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa3916b9"),
                FirstName = "Golden",
                LastName = "Dining",
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

            CreateNewGoodsReceivedNote = new GoodsReceivedNote
            {
                Id = 5,
                PurchaseOrderId = 3,
                PurchaseOrder = PurchaseOrders.First(d => d.Id == 3),
                InvoiceNumber = "INV_20210228_03",
                PaymentTypeId = 1,
                PaymentType = paymentType,
                Vat = 0.7m,
                Discount = 1.4m,
                Nbt = 0.7m,
                ReceivedBy = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa3916b9"),
                ReceivedDate = DateTime.Now.AddDays(-1),
                ReceivedUser = new User
                {
                    Id = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa3916b9"),
                    FirstName = "Golden",
                    LastName = "Dining",
                },
                ApprovedBy = Guid.Empty,
                ApprovalStatus = Status.Pending,
                ApprovedDate = default,
                ApprovalReason = "",
                CreatedBy = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa3916b9"),
                CreatedDate = DateTime.Now
            };

            EditGoodsReceivedNoteDto = new EditGoodsReceivedNoteDto
            {
                PurchaseOrderId = 1,
                PaymentTypeId = 1,
                InvoiceNumber = "INV_20210132_01",
                Nbt = 0.6m,
                Vat = 0.75m,
                Discount = 10,
                ReceivedBy = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa3916b9"),
                ReceivedDate = DateTime.Now.AddDays(-10),
            };

            ApprovalGoodsReceivedNoteDto = new ApprovalGoodsReceivedNoteDto
            {
                ApprovalReason = "All items are received",
                ApprovalStatus = "Approved"
            };

            PurchaseOrders.First(d => d.Id == 6).GoodsReceivedNotes.Add(GoodsReceivedNotes.First(d => d.Id == 4));
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
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
                    MockGoodsReceivedNoteService = null;
                    MockPurchaseOrderService = null;
                    MockPurchaseOrderItemService = null;
                    MockGoodsReceivedNoteItemService = null;
                    MockUserAccessorService = null;
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
                }

                _disposed = true;
            }
        }
    }
}
