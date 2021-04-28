using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Validators.V1;
using System;

namespace MyRestaurant.Business.Tests.Validators.V1.Fixtures
{
    public class CreateGoodsReceivedNoteDtoValidatorFixture : IDisposable
    {
        public CreateGoodsReceivedNoteDto Model { get; set; }
        public CreateGoodsReceivedNoteDtoValidator Validator { get; private set; }

        public CreateGoodsReceivedNoteDtoValidatorFixture()
        {
            Validator = new CreateGoodsReceivedNoteDtoValidator();

            Model = new CreateGoodsReceivedNoteDto
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
