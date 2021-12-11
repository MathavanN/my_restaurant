namespace MyRestaurant.Business.Dtos.V1
{
    public class GetUserDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public List<string> Roles { get; set; } = default!;
    }
}
