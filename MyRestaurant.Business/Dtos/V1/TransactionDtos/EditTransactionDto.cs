using System;

namespace MyRestaurant.Business.Dtos.V1
{
    public class EditTransactionDto
    {
        public int TransactionTypeId { get; set; }
        public int PaymentTypeId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string Cashflow { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
