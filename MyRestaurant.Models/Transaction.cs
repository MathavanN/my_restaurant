namespace MyRestaurant.Models
{
    public class Transaction : MyRestaurantObject
    {
        public long Id { get; set; }
        public int TransactionTypeId { get; set; }
        public int PaymentTypeId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public Cashflow Cashflow { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual PaymentType PaymentType { get; set; }
        public virtual TransactionType TransactionType { get; set; }
    }

    public enum Cashflow
    {
        Income = 0,
        Expense = 1,
    }
}
