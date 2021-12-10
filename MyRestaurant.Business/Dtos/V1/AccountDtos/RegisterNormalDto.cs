namespace MyRestaurant.Business.Dtos.V1
{
    public class RegisterNormalDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<string> Roles { get; set; }
    }

    public enum NormalUserRoles
    {
        Report,
        Normal
    }
}
