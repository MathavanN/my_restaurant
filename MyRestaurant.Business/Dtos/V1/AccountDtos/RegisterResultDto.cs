namespace MyRestaurant.Business.Dtos.V1
{
    public class RegisterResultDto
    {
        public string Status { get; set; }
        public string Message { get; set; }
    }

    public class TokenResultDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
