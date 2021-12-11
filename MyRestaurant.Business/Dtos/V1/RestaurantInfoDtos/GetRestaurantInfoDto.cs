namespace MyRestaurant.Business.Dtos.V1
{
    public class GetRestaurantInfoDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string LandLine { get; set; } = default!;
        public string Mobile { get; set; } = default!;
        public string Email { get; set; } = default!;
    }
}
