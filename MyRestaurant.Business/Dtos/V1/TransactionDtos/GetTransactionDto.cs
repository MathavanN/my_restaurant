namespace MyRestaurant.Business.Dtos.V1
{
    public class GetTransactionDto
    {
        public long Id { get; set; }
        public int TransactionTypeId { get; set; }
        public string TransactionType { get; set; } = default!;
        public int PaymentTypeId { get; set; }
        public string PaymentType { get; set; } = default!;
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; } = default!;
        public string Cashflow { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
    }
}
