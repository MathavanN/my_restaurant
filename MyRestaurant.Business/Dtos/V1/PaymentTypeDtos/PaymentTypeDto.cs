namespace MyRestaurant.Business.Dtos.V1
{
    public class PaymentTypeDto
    {
        public string Name { get; set; } = default!;
        public int CreditPeriod { get; set; }
    }
}
