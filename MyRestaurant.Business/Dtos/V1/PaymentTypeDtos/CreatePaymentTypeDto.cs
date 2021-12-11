namespace MyRestaurant.Business.Dtos.V1
{
    public class CreatePaymentTypeDto
    {
        public string Name { get; set; } = default!;
        public int CreditPeriod { get; set; }
    }
}
