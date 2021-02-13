using MyRestaurant.Api.Validators.V1;
using MyRestaurant.Business.Dtos.V1;
using System;

namespace MyRestaurant.Api.Tests.Validators.V1.Fixtures
{
    public class EditGoodsReceivedNoteItemDtoValidatorFixture : IDisposable
    {
        public EditGoodsReceivedNoteItemDto Model { get; set; }
        public EditGoodsReceivedNoteItemDtoValidator Validator { get; private set; }

        public EditGoodsReceivedNoteItemDtoValidatorFixture()
        {
            Validator = new EditGoodsReceivedNoteItemDtoValidator();

            Model = new EditGoodsReceivedNoteItemDto
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
