using Microsoft.AspNetCore.Mvc;
using Moq;
using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Repositories.Contracts;
using System;
using System.Collections.Generic;

namespace MyRestaurant.Api.Tests.Controllers.V1.Fixtures
{
    public class PurchaseOrderItemControllerFixture : IDisposable
    {
        public ApiVersion ApiVersion { get; private set; }
        public Mock<IPurchaseOrderItemRepository> MockPurchaseOrderItemRepository { get; private set; }
        public IEnumerable<GetPurchaseOrderItemDto> PurchaseOrderItems { get; private set; }
        public CreatePurchaseOrderItemDto ValidCreatePurchaseOrderItemDto { get; private set; }
        public GetPurchaseOrderItemDto CreatePurchaseOrderItemDtoResult { get; private set; }
        public EditPurchaseOrderItemDto ValidEditPurchaseOrderItemDto { get; private set; }
        public GetPurchaseOrderItemDto EditPurchaseOrderItemDtoResult { get; private set; }

        public PurchaseOrderItemControllerFixture()
        {
            ApiVersion = new ApiVersion(1, 0);

            MockPurchaseOrderItemRepository = new Mock<IPurchaseOrderItemRepository>();

            PurchaseOrderItems = new List<GetPurchaseOrderItemDto>
            {
                new GetPurchaseOrderItemDto {
                    Id = 1,
                    PurchaseOrderId = 101,
                    ItemTypeId = 5,
                    ItemTypeName = "Grocery",
                    ItemId = 20025,
                    ItemName = "Chilli Powder",
                    ItemUnit = 250,
                    UnitOfMeasureCode = "g",
                    ItemUnitPrice = 540,
                    Quantity = 5
                },
                new GetPurchaseOrderItemDto {
                    Id = 2,
                    PurchaseOrderId = 202,
                    ItemTypeId = 5,
                    ItemTypeName = "Grocery",
                    ItemId = 20026,
                    ItemName = "Salt",
                    ItemUnit = 5,
                    UnitOfMeasureCode = "kg",
                    ItemUnitPrice = 30,
                    Quantity = 10
                },
                new GetPurchaseOrderItemDto {
                    Id = 3,
                    PurchaseOrderId = 101,
                    ItemTypeId = 6,
                    ItemTypeName = "Bevarage",
                    ItemId = 20050,
                    ItemName = "Coca cola",
                    ItemUnit = 300,
                    UnitOfMeasureCode = "ml",
                    ItemUnitPrice = 50,
                    Quantity = 5
                },
                new GetPurchaseOrderItemDto {
                    Id = 4,
                    PurchaseOrderId = 202,
                    ItemTypeId = 5,
                    ItemTypeName = "Grocery",
                    ItemId = 20024,
                    ItemName = "Sugar",
                    ItemUnit = 5,
                    UnitOfMeasureCode = "kg",
                    ItemUnitPrice = 260,
                    Quantity = 6
                }
            };

            ValidCreatePurchaseOrderItemDto = new CreatePurchaseOrderItemDto
            {
                PurchaseOrderId = 202,
                ItemId = 20023,
                ItemUnitPrice = 350,
                Quantity = 5
            };

            CreatePurchaseOrderItemDtoResult = new GetPurchaseOrderItemDto
            {
                Id = 5,
                PurchaseOrderId = 202,
                ItemTypeId = 5,
                ItemTypeName = "Grocery",
                ItemId = 20023,
                ItemName = "Rice",
                ItemUnit = 10,
                UnitOfMeasureCode = "kg",
                ItemUnitPrice = 350,
                Quantity = 5
            };

            ValidEditPurchaseOrderItemDto = new EditPurchaseOrderItemDto
            {
                PurchaseOrderId = 101,
                ItemId = 20025,
                ItemUnitPrice = 650,
                Quantity = 7
            };

            EditPurchaseOrderItemDtoResult = new GetPurchaseOrderItemDto
            {
                Id = 1,
                PurchaseOrderId = 101,
                ItemTypeId = 5,
                ItemTypeName = "Grocery",
                ItemId = 20025,
                ItemName = "Chilli Powder",
                ItemUnit = 250,
                UnitOfMeasureCode = "g",
                ItemUnitPrice = 650,
                Quantity = 7
            };
        }
        public void Dispose()
        {
            MockPurchaseOrderItemRepository = null;
        }
    }
}
