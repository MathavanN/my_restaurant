using Microsoft.AspNetCore.Mvc;
using Moq;
using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Repositories.Contracts;
using System;
using System.Collections.Generic;

namespace MyRestaurant.Api.Tests.Controllers.V1.Fixtures
{
    public class GoodsReceivedNoteFreeItemControllerFixture : IDisposable
    {
        private bool _disposed;
        public ApiVersion ApiVersion { get; private set; }
        public Mock<IGoodsReceivedNoteFreeItemRepository> MockGoodsReceivedNoteFreeItemRepository { get; private set; }
        public IEnumerable<GetGoodsReceivedNoteFreeItemDto> GoodsReceivedNoteFreeItems { get; private set; }
        public CreateGoodsReceivedNoteFreeItemDto ValidCreateGoodsReceivedNoteFreeItemDto { get; private set; }
        public GetGoodsReceivedNoteFreeItemDto CreateGoodsReceivedNoteFreeItemDtoResult { get; private set; }
        public EditGoodsReceivedNoteFreeItemDto ValidEditGoodsReceivedNoteFreeItemDto { get; private set; }
        public GetGoodsReceivedNoteFreeItemDto EditGoodsReceivedNoteFreeItemDtoResult { get; private set; }

        public GoodsReceivedNoteFreeItemControllerFixture()
        {
            ApiVersion = new ApiVersion(1, 0);

            MockGoodsReceivedNoteFreeItemRepository = new Mock<IGoodsReceivedNoteFreeItemRepository>();

            GoodsReceivedNoteFreeItems = new List<GetGoodsReceivedNoteFreeItemDto>
            {
                new GetGoodsReceivedNoteFreeItemDto {
                    Id = 1,
                    GoodsReceivedNoteId = 101,
                    ItemTypeId = 6,
                    ItemTypeName = "Bevarage",
                    ItemId = 20052,
                    ItemName = "Coca cola",
                    ItemUnit = 1.5m,
                    UnitOfMeasureCode = "l",
                    ItemUnitPrice = 250,
                    Quantity = 1,
                    Nbt = 0.1m,
                    Vat = 0.1m,
                    Discount = 0.1m
                },
                new GetGoodsReceivedNoteFreeItemDto
                {
                    Id = 2,
                    GoodsReceivedNoteId = 202,
                    ItemTypeId = 5,
                    ItemTypeName = "Grocery",
                    ItemId = 20026,
                    ItemName = "Salt",
                    ItemUnit = 5,
                    UnitOfMeasureCode = "kg",
                    ItemUnitPrice = 30,
                    Quantity = 1,
                    Nbt = 0.1m,
                    Vat = 0.1m,
                    Discount = 0.1m
                }
            };

            ValidCreateGoodsReceivedNoteFreeItemDto = new CreateGoodsReceivedNoteFreeItemDto
            {
                GoodsReceivedNoteId = 202,
                ItemId = 20023,
                ItemUnitPrice = 350,
                Quantity = 5,
                Nbt = 0.1m,
                Vat = 0.1m,
                Discount = 0.1m
            };

            CreateGoodsReceivedNoteFreeItemDtoResult = new GetGoodsReceivedNoteFreeItemDto
            {
                Id = 3,
                GoodsReceivedNoteId = 202,
                ItemTypeId = 5,
                ItemTypeName = "Grocery",
                ItemId = 20023,
                ItemName = "Rice",
                ItemUnit = 10,
                UnitOfMeasureCode = "kg",
                ItemUnitPrice = 350,
                Quantity = 1,
                Nbt = 0.1m,
                Vat = 0.1m,
                Discount = 0.1m
            };

            ValidEditGoodsReceivedNoteFreeItemDto = new EditGoodsReceivedNoteFreeItemDto
            {
                GoodsReceivedNoteId = 101,
                ItemId = 20052,
                ItemUnitPrice = 150,
                Quantity = 2
            };

            EditGoodsReceivedNoteFreeItemDtoResult = new GetGoodsReceivedNoteFreeItemDto
            {
                Id = 2,
                GoodsReceivedNoteId = 101,
                ItemTypeId = 5,
                ItemTypeName = "Bevarage",
                ItemId = 20052,
                ItemName = "Coca cola",
                ItemUnit = 1.5m,
                UnitOfMeasureCode = "l",
                ItemUnitPrice = 150,
                Quantity = 2,
                Nbt = 0.1m,
                Vat = 0.1m,
                Discount = 0.1m
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
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
                    MockGoodsReceivedNoteFreeItemRepository = null;
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
                }

                _disposed = true;
            }
        }
    }
}
