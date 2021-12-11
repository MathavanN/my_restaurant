namespace MyRestaurant.Models
{
    public class TransactionType : MyRestaurantObject
    {
        public TransactionType()
        {
            Transactions = new HashSet<Transaction>();
            Type = default!;
        }

        public int Id { get; set; }
        public string Type { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
