namespace MyRestaurant.Business.Dtos.V1
{
    public class RegisterNormalDto
    {
        public string? Email { get; set; }
        public string Password { get; set; } = default!;
        public string ConfirmPassword { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public List<string>? Roles { get; set; }
    }

    public enum NormalUserRoles
    {
        Report,
        Normal
    }
}
