using Microsoft.AspNetCore.Mvc;
using Moq;
using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Repositories.Contracts;
using System;
using System.Collections.Generic;

namespace MyRestaurant.Api.Tests.Controllers.V1.Fixtures
{
    public class GoodsReceivedNoteItemControllerFixure : IDisposable
    {
        private bool _disposed;
        public ApiVersion ApiVersion { get; private set; }
        public Mock<IGoodsReceivedNoteItemRepository> MockGoodsReceivedNoteItemRepository { get; private set; }
        public IEnumerable<GetGoodsReceivedNoteItemDto> GoodsReceivedNoteItems { get; private set; }
        public CreateGoodsReceivedNoteItemDto ValidCreateGoodsReceivedNoteItemDto { get; private set; }
        public GetGoodsReceivedNoteItemDto CreateGoodsReceivedNoteItemDtoResult { get; private set; }
        public EditGoodsReceivedNoteItemDto ValidEditGoodsReceivedNoteItemDto { get; private set; }
        public GetGoodsReceivedNoteItemDto EditGoodsReceivedNoteItemDtoResult { get; private set; }

        public GoodsReceivedNoteItemControllerFixure()
        {
            ApiVersion = new ApiVersion(1, 0);

            MockGoodsReceivedNoteItemRepository = new Mock<IGoodsReceivedNoteItemRepository>();

            GoodsReceivedNoteItems = new List<GetGoodsReceivedNoteItemDto>
            {
                new GetGoodsReceivedNoteItemDto {
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
                new GetGoodsReceivedNoteItemDto {
                    Id = 2,
                    GoodsReceivedNoteId = 101,
                    ItemTypeId = 5,
                    ItemTypeName = "Grocery",
                    ItemId = 20025,
                    ItemName = "Chilli Powder",
                    ItemUnit = 250,
                    UnitOfMeasureCode = "g",
                    ItemUnitPrice = 540,
                    Quantity = 5,
                    Nbt = 0.1m,
                    Vat = 0.1m,
                    Discount = 0.1m
                },
                new GetGoodsReceivedNoteItemDto
                {
                    Id = 3,
                    GoodsReceivedNoteId = 202,
                    ItemTypeId = 5,
                    ItemTypeName = "Grocery",
                    ItemId = 20026,
                    ItemName = "Salt",
                    ItemUnit = 5,
                    UnitOfMeasureCode = "kg",
                    ItemUnitPrice = 30,
                    Quantity = 10,
                    Nbt = 0.1m,
                    Vat = 0.1m,
                    Discount = 0.1m
                },
                new GetGoodsReceivedNoteItemDto
                {
                    Id = 4,
                    GoodsReceivedNoteId = 202,
                    ItemTypeId = 5,
                    ItemTypeName = "Grocery",
                    ItemId = 20024,
                    ItemName = "Sugar",
                    ItemUnit = 5,
                    UnitOfMeasureCode = "kg",
                    ItemUnitPrice = 260,
                    Quantity = 6,
                    Nbt = 0.1m,
                    Vat = 0.1m,
                    Discount = 0.1m
                }
            };

            ValidCreateGoodsReceivedNoteItemDto = new CreateGoodsReceivedNoteItemDto
            {
                GoodsReceivedNoteId = 202,
                ItemId = 20023,
                ItemUnitPrice = 350,
                Quantity = 5,
                Nbt = 0.1m,
                Vat = 0.1m,
                Discount = 0.1m
            };

            CreateGoodsReceivedNoteItemDtoResult = new GetGoodsReceivedNoteItemDto
            {
                Id = 5,
                GoodsReceivedNoteId = 202,
                ItemTypeId = 5,
                ItemTypeName = "Grocery",
                ItemId = 20023,
                ItemName = "Rice",
                ItemUnit = 10,
                UnitOfMeasureCode = "kg",
                ItemUnitPrice = 350,
                Quantity = 5,
                Nbt = 0.1m,
                Vat = 0.1m,
                Discount = 0.1m
            };

            ValidEditGoodsReceivedNoteItemDto = new EditGoodsReceivedNoteItemDto
            {
                GoodsReceivedNoteId = 101,
                ItemId = 20025,
                ItemUnitPrice = 650,
                Quantity = 7,
                Nbt = 0.1m,
                Vat = 0.1m,
                Discount = 0.1m
            };

            EditGoodsReceivedNoteItemDtoResult = new GetGoodsReceivedNoteItemDto
            {
                Id = 2,
                GoodsReceivedNoteId = 101,
                ItemTypeId = 5,
                ItemTypeName = "Grocery",
                ItemId = 20025,
                ItemName = "Chilli Powder",
                ItemUnit = 250,
                UnitOfMeasureCode = "g",
                ItemUnitPrice = 650,
                Quantity = 7,
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
                    MockGoodsReceivedNoteItemRepository = null;
                }

                _disposed = true;
            }
        }
    }
}
