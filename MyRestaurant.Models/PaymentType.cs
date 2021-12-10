namespace MyRestaurant.Models
{
    public class PaymentType : MyRestaurantObject
    {
        public PaymentType()
        {
            GoodsReceivedNotes = new HashSet<GoodsReceivedNote>();
            Transactions = new HashSet<Transaction>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int CreditPeriod { get; set; }

        public virtual ICollection<GoodsReceivedNote> GoodsReceivedNotes { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
