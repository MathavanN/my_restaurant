namespace MyRestaurant.Services
{
    public class JwtSettings
    {
        public string AccessTokenSecret { get; set; } = default!;
        public string Issuer { get; set; } = default!;
        public string Audience { get; set; } = default!;
        public double AccessTokenExpirationInMinutes { get; set; }
        public string RefreshTokenSecret { get; set; } = default!;
        public double RefreshTokenExpirationInMinutes { get; set; }
    }
}
