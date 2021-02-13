using MyRestaurant.Api.Validators.V1;
using MyRestaurant.Business.Dtos.V1;
using System;

namespace MyRestaurant.Api.Tests.Validators.V1.Fixtures
{
    public class CreateGoodsReceivedNoteItemDtoValidatorFixture : IDisposable
    {
        public CreateGoodsReceivedNoteItemDto Model { get; set; }
        public CreateGoodsReceivedNoteItemDtoValidator Validator { get; private set; }

        public CreateGoodsReceivedNoteItemDtoValidatorFixture()
        {
            Validator = new CreateGoodsReceivedNoteItemDtoValidator();

            Model = new CreateGoodsReceivedNoteItemDto
            {
                GoodsReceivedNoteId = 1,
                ItemId = 1,
                ItemUnitPrice = 1450.50m,
                Quantity = 2,
                Nbt = 0.3m,
                Vat = 0.5m,
                Discount = 1.2m,
            };
        }
        public void Dispose()
        {
            Model = null;
            Validator = null;
        }
    }
}
