namespace MyRestaurant.Business.Dtos.V1
{
    public class CreateRestaurantInfoDto
    {
        public string Name { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string City { get; set; } = default!;
        public string Country { get; set; } = default!;
        public string LandLine { get; set; } = default!;
        public string Mobile { get; set; } = default!;
        public string Email { get; set; } = default!;
    }
}
