namespace MyRestaurant.Business.Dtos.V1
{
    public class GetPaymentTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public int CreditPeriod { get; set; }
    }
}
