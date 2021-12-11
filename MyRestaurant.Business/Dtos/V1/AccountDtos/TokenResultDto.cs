namespace MyRestaurant.Business.Dtos.V1
{
    public class TokenResultDto
    {
        public string AccessToken { get; set; } = default!;
        public string RefreshToken { get; set; } = default!;
    }
}
