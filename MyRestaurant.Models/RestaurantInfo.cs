namespace MyRestaurant.Models
{
    public class RestaurantInfo : MyRestaurantObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string LandLine { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
    }
}
