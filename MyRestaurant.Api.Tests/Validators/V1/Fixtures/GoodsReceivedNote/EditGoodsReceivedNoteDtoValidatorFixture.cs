using MyRestaurant.Api.Validators.V1;
using MyRestaurant.Business.Dtos.V1;
using System;

namespace MyRestaurant.Api.Tests.Validators.V1.Fixtures
{
    public class EditGoodsReceivedNoteDtoValidatorFixture : IDisposable
    {
        public EditGoodsReceivedNoteDto Model { get; set; }
        public EditGoodsReceivedNoteDtoValidator Validator { get; private set; }

        public EditGoodsReceivedNoteDtoValidatorFixture()
        {
            Validator = new EditGoodsReceivedNoteDtoValidator();

            Model = new EditGoodsReceivedNoteDto
            {
                PurchaseOrderId = 1,
                InvoiceNumber = "INV_2021_02_13",
                PaymentTypeId = 1,
                Nbt = 0.3m,
                Vat = 0.5m,
                Discount = 1.2m,
                ReceivedBy = Guid.NewGuid(),
                ReceivedDate = DateTime.Now
            };
        }
        public void Dispose()
        {
            Model = null;
            Validator = null;
        }
    }
}
