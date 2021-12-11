namespace MyRestaurant.Services
{
    public class CurrentUser
    {
        public CurrentUser()
        {
            FirstName = default!;
            LastName = default!;
            Email = default!;
            Roles = default!;
        }
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
