namespace MyRestaurant.Business.Dtos.V1
{
    public class CurrentUserDto
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public IEnumerable<string> Roles { get; set; } = default!;
    }
}
