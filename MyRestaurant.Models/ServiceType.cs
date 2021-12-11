namespace MyRestaurant.Models
{
    public class ServiceType : MyRestaurantObject
    {
        public ServiceType()
        {
            Type = default!;
        }

        public int Id { get; set; }
        public string Type { get; set; }
    }
}
