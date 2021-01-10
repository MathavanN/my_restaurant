using System.Collections.Generic;

namespace MyRestaurant.Models
{
    public class Supplier : MyRestaurantObject
    {
        public Supplier()
        {
            PurchaseOrders = new HashSet<PurchaseOrder>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Telephone1 { get; set; }
        public string Telephone2 { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string ContactPerson { get; set; }

        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
    }
}
