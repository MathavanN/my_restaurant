namespace MyRestaurant.Business.Dtos.V1
{
    public class GetTransactionDto : TransactionDto
    {
        public long Id { get; set; }
        public string TransactionType { get; set; } = default!;
        public string PaymentType { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
    }
}
